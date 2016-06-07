// @brief Read a file and output the number of times each word appears
// @author Miguel Bermudez
// @version 2016-06-07

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Word_Counter
{
    public partial class wordCounter : Form
    {
        //List used behind the scenes
        private List<WordsCounted> backList;
        private static readonly string FREQ_SYMBOL = "▼";
        private static readonly string FREQ_SYMBOL_ALT = "Θ" ;

        /// <summary>
        /// Initializes form
        /// </summary>
        public wordCounter()
        {
            InitializeComponent();
            //Reset the form to default state
            ResetForm();
            //Sets default directory to some sample files
            Directory.SetCurrentDirectory(@"..\..\SampleFiles\");
        }

        /// <summary>
        /// Lets user select a file.
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void selectFile_Click(object sender, EventArgs e)
        {
            string filePath;    //Holds path to file user will select

            //Sets the dialog box to display the default directory first
            //This was declared in the wordCounter constructor
            oFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            //If the user selects a file
            if (oFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Change the cursor to a wait icon
                Cursor.Current = Cursors.WaitCursor;

                //Sets the label above the outputBox to the name of file by
                //splitting the file path by \ and taking the last part of
                //the result
                string[] fileNameArray = oFileDialog.FileName.Split('\\');
                fileName.Text = fileNameArray[fileNameArray.Length - 1];

                //Gets the path for the file
                filePath = oFileDialog.FileName;

                //If the user has checked they want to count by characters
                if (characterCount.Checked)
                {
                    CharRead(filePath);
                }
                else
                {
                    //If not we read by words
                    StandardRead(filePath);
                }

                //Update statistics
                UpdateStats();

                //Enable clearing the form
                clearFile.Visible = true;
                //Enable saving file
                saveStatistics.Visible = true;
                //Disable selecting a new file
                selectFile.Visible = false;

                //Sort by frequency button is now visible
                sortFreqButton.Visible = true;

                //If we are not counting characters
                if (!characterCount.Checked)
                {
                    //Show longest word is now visible
                    longestWordsShow.Visible = true;
                }

                //Enable searching
                //NOTE: Changing the text updates the outputBox
                //Line commented out as it is unnecessary 
                //(and slows down results)

                /*UpdateListBox();*/
                searchBox.Text = "";
                searchBox.Enabled = true;

                //Sets the cursor back to default. Done!
                Cursor.Current = Cursors.Default;
            }
            else
            {
                //User did not select a file
                fileName.Text = "No file selected";
            }
        }

        /// <summary>
        /// Reads the file by character
        /// </summary>
        /// <param name="iFile">input file</param>
        private void CharRead(string iFile)
        {
            //Open the text selected into the StreamReader
            StreamReader inputFile = File.OpenText(oFileDialog.FileName);
            char tempChar;  //Holds the characters as they are read

            //While there is still more to read
            while (!inputFile.EndOfStream)
            {
                //Read the next character
                tempChar = (char)inputFile.Read();

                //If the user doesn't want case sensitive results
                if (!caseSensitive.Checked)
                {
                    //Turn all characters into lowercase
                    tempChar = char.ToLower(tempChar);
                }

                //If the character isn't a whitespace character
                if (!char.IsWhiteSpace(tempChar))
                {
                    //Updates list with read character
                    UpdateList(tempChar.ToString());
                }
            }
        }

        /// <summary>
        /// Reads words in a file
        /// </summary>
        /// <param name="iFile">Not used</param>
        private void StandardRead(string filePath)
        {
            StringBuilder tempString = new StringBuilder();  //Holds each word read
            string incompleteWord = ""; //Holds incomplete words

            //Gets all words from file
            string allFile = File.ReadAllText(filePath);

            //Split all the words by whitespace
            string[] wordArray = allFile.Split();

            //for the length of created array of words
            for (int i = 0; i < wordArray.Length; i++, tempString.Clear())
            {
                //If there was an incomplete word before this one
                if (incompleteWord != "")
                {
                    //Add the word to the temp string minus the '-'
                    for (int j = 0; i < incompleteWord.Length - 1; ++j)
                    {
                        tempString.Append(incompleteWord[j]);
                    }

                    //Clear incomplete word
                    incompleteWord = "";
                }

                //Strips punctuation
                tempString.Append(StripPunctuation(wordArray[i]));

                //If the temporary word is not null or a whitespace char
                if (!string.IsNullOrWhiteSpace(tempString.ToString()))
                {
                    //If the last letter of the string is not a '-'
                    if (tempString[tempString.Length - 1] != '-')
                    {
                        //If the user doesn't care about upper or lower case
                        if (!caseSensitive.Checked)
                        {
                            //Updates list with all words lowercase
                            UpdateList(tempString.ToString().ToLower());
                        } else
                        {
                            //Updates list
                            UpdateList(tempString.ToString());
                        }
                    }
                    else
                    {
                        //The last letter is a '-' so word must be incomplete
                        incompleteWord = tempString.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Strips punctuation off of string passed and returns new string
        /// </summary>
        /// <param name="str">String to affect</param>
        /// <returns>Fixed string</returns>
        private string StripPunctuation(string str)
        {
            StringBuilder tempStr = new StringBuilder();

            //For each letter in the word
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c) || c == '-')
                {
                    //Append letter if not punctuation or hyphen
                    tempStr.Append(c);
                }
            }

            //Return string
            return tempStr.ToString();
        }

        /// <summary>
        /// Inserts the word passed into list
        /// </summary>
        /// <param name="w">Word to be inserted</param>
        private void UpdateList(string w)
        {
            int index = 0;
            bool wordFound = false; //Holds whether word was found or not

            //Check that list has at least one item
            if (backList.Count > 0)
            {
                //Get whether word is in list and where it is (or should be)
                index = GetIndex(w, ref wordFound);
            }

            //If not found we must add it at index
            if (!wordFound)
            {
                //Insert a new object at sorted position
                backList.Insert(index, new WordsCounted(w));
            }
            else
            {
                //If found we must increment count
                backList[index].incrementNum();
            }
        }

        /// <summary>
        /// Updates statistics box
        /// </summary>
        private void UpdateStats()
        {
            string getCommonWord = "None found";    //Defaults to not found
            int numOfCommonWord = 0;    //Holds number of times word is found
            int getNumOfWords = 0;      //Number of total words
            int getNumOfLetters = 0;    //Number of total letters

            //Go through all the words
            for (int i = 0, sizeOfList = backList.Count; i < sizeOfList; i++)
            {
                //If the current object has a higher count than current highest count
                if (backList[i].getNum() > numOfCommonWord &&
                    backList[i].getWord() != "")
                {
                    //It is our new most common word
                    getCommonWord = (backList[i]).getWord();
                    //Updates the highest word
                    numOfCommonWord = backList[i].getNum();
                }

                //Add the number of times each word shows up
                getNumOfWords += backList[i].getNum();

                //Add up the length of each word (letters in word)
                getNumOfLetters +=
                    (backList[i].getWord().Length * backList[i].getNum());
            }

            //Outputs results to labels
            //Shows most common word and its count
            mostCommonWord.Text = getCommonWord + " | " +
                numOfCommonWord.ToString();
            //Shows total number of words
            numOfWords.Text = getNumOfWords.ToString();
            //Size of list is the number of unique words
            numOfUniqueWords.Text = backList.Count.ToString();

            //Won't get letters if we are not counting by words
            if (!characterCount.Checked)
            {
                //Outputs number of letters
                numOfLetters.Text = getNumOfLetters.ToString();
                //Gets the number of letters and divides them by number
                //of words to get average letters
                avgLettersPerWord.Text = string.Format("{0:0.00}",
                    ((double)getNumOfLetters / getNumOfWords));

            }
        }

        /// <summary>
        /// Updates the outputBox using data from list
        /// </summary>
        /// <param name="leftBounds">Starting left index</param>
        /// <param name="rightBounds">Ending right index</param>
        private void UpdateListBox(int leftBounds = 0, int rightBounds = -1) 
        {
            //Clear the list before working with it
            outputBox.Items.Clear();

            //If rightBounds is left to default then we make it the entire list
            if (rightBounds < leftBounds)
            {
                //Gets total number of items
                rightBounds = backList.Count;
            }

            //Suspends updating outputBox until finished
            outputBox.SuspendLayout();

            //From the left bounds to the right bounds
            for (int i = leftBounds; i < rightBounds; ++i)
            {
                //Add item to outputBox
                outputBox.Items.Add(backList[i]);
            }

            //Resume updating layout
            outputBox.ResumeLayout();
        }

        /// <summary>
        /// Lets user saves the stats for the file
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void saveStatistics_Click(object sender, EventArgs e)
        {
            //Output file object
            StreamWriter outFile;

            //If user selects file
            if (sFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Create the text file
                outFile = File.CreateText(sFileDialog.FileName);

                //Top of file. Shows file name and a bit of credits
                outFile.WriteLine("Statistics for file : " + fileName.Text);
                outFile.WriteLine("Gathered using Word Counter program.");
                outFile.WriteLine("\n");

                //Write out the label for most common word and its value
                outFile.WriteLine(mostCommonWordLabel.Text + " " +
                    mostCommonWord.Text);
                //Write out the number of words in file and its result
                outFile.WriteLine(numOfWordsLabel.Text + " " +
                    numOfWords.Text);
                //Write out the number of unique words in file and its result
                outFile.WriteLine(numOfUniqueWordsLabel.Text + " " +
                    numOfUniqueWords.Text);

                //If we are searching by words not characters
                if (!characterCount.Checked)
                {
                    //Write out the number of letters and its result
                    outFile.WriteLine(numOfLettersLabel.Text + " " +
                        numOfLetters.Text);
                    //Write out the average letters per word and its result
                    outFile.WriteLine(avgLettersPerWordLabel.Text + " " +
                        avgLettersPerWord.Text);
                }

                //Space out lines
                outFile.WriteLine("");

                //If the user checked that he wants the list along
                //with the statistics
                if (saveList.Checked)
                {
                    //If the searchBox isn't empty
                    if (searchBox.Text != "")
                    {
                        //If list is not being sorted by frequency
                        if (sortFreqButton.Text != FREQ_SYMBOL_ALT)
                        {
                            //Our list is filtered by a search
                            outFile.WriteLine("List of only words starting with: "
                                + searchBox.Text);
                        }
                        else
                        {
                            //Else we are sorting by frequency
                            outFile.WriteLine("List sort by frequency");
                        }
                    } else
                    {
                        outFile.WriteLine("List sorted alphabetically");
                    }

                    //Extra blank line
                    outFile.WriteLine("");

                    for (int i = 0, count = outputBox.Items.Count; i < count; i++)
                    {
                        //Write out the contents of the outputBox list
                        //line by line into the file
                        outFile.WriteLine(((WordsCounted)outputBox.Items[i]).ToString());
                    }
                }

                //Close output file
                outFile.Close();
            }
        }

        /// <summary>
        /// Clears form by calling ResetForm
        /// </summary>
        /// <param name="sender">Not Used</param>
        /// <param name="e">Not Used</param>
        private void clearFile_Click(object sender, EventArgs e)
        {
            //Reset form
            ResetForm();
        }

        /// <summary>
        /// Clears the form to a new state
        /// </summary>
        private void ResetForm()
        {
            //Resets the list by calling Clear function
            outputBox.Items.Clear();
            //Create new list for the background list
            backList = new List<WordsCounted>();

            //Resets all the labels
            mostCommonWord.Text = "";
            numOfWords.Text = "";
            numOfUniqueWords.Text = "";
            numOfLetters.Text = "";
            avgLettersPerWord.Text = "";
            fileName.Text = "[filename]";
            sortFreqButton.Visible = false;
            longestWordsShow.Visible = false;
            clearFile.Visible = false;
            selectFile.Visible = true;
            saveStatistics.Visible = false;

            //Disable searchBox
            searchBox.Text = "Select a file before searching";
            searchBox.Enabled = false;
        }

        /// <summary>
        /// Initializes the label fields on load to empty
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void wordCounter_Load(object sender, EventArgs e)
        {
            //Sets the labels on startup to blank
            mostCommonWord.Text = "";
            numOfWords.Text = "";
            numOfUniqueWords.Text = "";
            numOfLetters.Text = "";
            avgLettersPerWord.Text = "";
            fileName.Text = "[filename]";
        }

        /// <summary>
        /// Changes the label fields when counting by letters
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void extraChars_CheckedChanged(object sender, EventArgs e)
        {
            if (characterCount.Checked)
            {
                //When we count by letters we must switch some labels
                mostCommonWordLabel.Text = "Most common letter:";
                numOfWordsLabel.Text = "Letters/Symbols:";
                numOfUniqueWordsLabel.Text = "Unique letters/symbols:";
                numOfLetters.Visible = false;
                numOfLettersLabel.Visible = false;
                avgLettersPerWord.Visible = false;
                avgLettersPerWordLabel.Visible = false;
                longestWordLabel.Visible = false;

                //Resets form as major parameters have changed
                ResetForm();
            }
            else
            {
                //Sets the labels on startup to blank
                mostCommonWordLabel.Text = "Most common word:";
                numOfWordsLabel.Text = "Number of words:";
                numOfUniqueWordsLabel.Text = "Number of unique words:";
                numOfLetters.Visible = true;
                numOfLettersLabel.Visible = true;
                avgLettersPerWord.Visible = true;
                avgLettersPerWordLabel.Visible = true;
                longestWordLabel.Visible = true;

                //Resets form as major parameters have changed
                ResetForm();
            }
        }

        /// <summary>
        /// Clears form by calling ResetForm
        /// </summary>
        /// <param name="sender">Not Used</param>
        /// <param name="e">Not Used</param>
        private void caseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            ResetForm();
        }

        /// <summary>
        /// Sorts the output to be sorted by frequency
        /// </summary>
        private void SortByFrequency()
        {
            //Parallel array of bools
            bool[] usedWords = new bool[backList.Count];
            int largestNum;     //Current largest num
            int curLargestIndex;//Index of largest num

            //Initialize all to false
            for (int i = 0; i < backList.Count; ++i)
            {
                usedWords[i] = false;
            }

            //For the amount of items in the list
            for (int i = 0; i < backList.Count; ++i)
            {
                //Starting at largest number being 0
                largestNum = 0;
                curLargestIndex = 0;

                //For the entire list
                for (int j = 0; j < backList.Count; ++j)
                {
                    //If the bool array still has this word as not used
                    //and the number of it is larger than the current
                    if (!usedWords[j] && largestNum < backList[j].getNum())
                    {
                        //New largest number and index
                        curLargestIndex = j;
                        largestNum = backList[j].getNum();
                    }
                }

                //Change the bool in the parallel array to used
                usedWords[curLargestIndex] = true;
                //Add the word to the outputBox
                outputBox.Items.Add(backList[curLargestIndex]);
            }
        }

        /// <summary>
        /// Sort the array by frequency
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void sortFreqButton_Click(object sender, EventArgs e)
        {
            //Erase previous list displayed
            outputBox.Items.Clear();
            //Show this might take a while
            Cursor.Current = Cursors.WaitCursor;

            //If the button indicated sort by frequency
            if (sortFreqButton.Text == FREQ_SYMBOL )
            {
                //Disable the search bar
                searchBox.Text = "Disabled while displaying by frequency";
                searchBox.Enabled = false;
                //We sort by frequency
                SortByFrequency();
                //Change the button for the user to sort back
                sortFreqButton.Text = FREQ_SYMBOL_ALT;
            }
            else
            {
                //Enable the search bar
                searchBox.Text = "";
                searchBox.Enabled = true;
                //We do the regular sort again
                UpdateListBox();
                //Change the button back
                sortFreqButton.Text = FREQ_SYMBOL;
            }

            //Return to regular cursor
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Shows the longest word(s) in the list
        /// </summary>
        /// <param name="sender">Not Used</param>
        /// <param name="e">Not Used</param>
        private void longestWordsShow_Click(object sender, EventArgs e)
        {
            int getLongestWord = 0;     //Longest word size
            List<string> longestWords = new List<string>();    //Current longest word

            //Will hold output
            StringBuilder tempString = new StringBuilder();

            //Shows that the program is running
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < backList.Count; ++i)
            {
                //Compare the length of the word
                if (getLongestWord <= backList[i].getWord().Length)
                {
                    //If the new word is not only equal but GREATER
                    if (getLongestWord < backList[i].getWord().Length)
                    {
                        //This is the new size to aim for
                        getLongestWord = backList[i].getWord().Length;
                        longestWords.Clear();
                    }

                    //Add the current word as it is part of longest
                    longestWords.Add(backList[i].getWord());
                }
            }

            //Append the length of longest word
            tempString.Append("Length of longest word(s): ");
            tempString.Append(getLongestWord);
            tempString.Append("\n");

            //Append the words
            for (int i = 0; i < longestWords.Count; ++i)
            {
                tempString.Append("\n");
                tempString.Append(longestWords[i]);
            }

            //Sets the cursor back to default. Done!
            Cursor.Current = Cursors.Default;

            //Show a dialog box with the results
            MessageBox.Show(tempString.ToString());
        }

        /// <summary>
        /// Binary search for the index of a string in a list
        /// </summary>
        /// <param name="target">Word to search</param>
        /// <param name="wordFound">Results of search</param>
        /// <param name="lBounds">Left limit</param>
        /// <param name="rBounds">Right Limit</param>
        /// <returns>Index of where word is (or should be)</returns>
        private int GetIndex(string target, ref bool wordFound, int lBounds = 0, int rBounds = -1 )
        {
            int middleObj = 0;          //Holds what the middle object is
            int comparisonResults;      //Holds the results of comparison between strings

            //Set the start state of the wordFound bool to false
            wordFound = false;

            //If the number wasn't smaller on the right (or default)
            if (rBounds < lBounds)
            {
                //Holds the number of items in outputBox
                rBounds = backList.Count - 1;
            }

            //While right bounds is still larger and the word hasn't been found
            while (lBounds <= rBounds && !wordFound)
            {

                //Middle object is the right bounds plus the left bounds (total size)
                //divided by 2
                middleObj = (rBounds + lBounds) / 2;

                //Get the comparison between the two strings
                comparisonResults =
                    target.CompareTo(backList[middleObj].getWord());

                //If the word to be update is found
                if (comparisonResults < 0)
                {
                    //String was smaller alphabetically so we must move right bounds
                    //towards center - 1
                    rBounds = middleObj - 1;
                }
                else if (comparisonResults > 0)
                {
                    //String was higher alphabetically so we must move right bounds
                    //towards center + 1
                    lBounds = ++middleObj;
                }
                else
                {
                    //Word has been found
                    wordFound = true;
                }
            }

            //Return where the middle object is pointing
            return middleObj;
        }

        /// <summary>
        /// Allows user to search dynamically
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            //Passed bool not used
            bool discard = false;

            //Get the first index of the word that is being searched for
            int searchLeft = GetIndex(searchBox.Text, ref discard);
            //Get the last index of the word that is being serached for
            //Note: Use zz to simulate a large next word that is 
            //(unlikely) to exists
            int searchRight = GetIndex(searchBox.Text + "zz", ref discard);

            //Update the outputBox with the new values
            UpdateListBox(searchLeft, searchRight);
        }
    }
}
