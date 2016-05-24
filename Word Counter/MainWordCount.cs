// @brief Read a file and output the number of times each word appears
// @author Miguel Bermudez
// @version 2016-05-23

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
        private List<wordsCounted> backList;

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
        /// Lets user select a file. Also beginning method of processing
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void selectFile_Click(object sender, EventArgs e)
        {
            //Path to file
            string filePath;

            //Starting path for file dialog
            oFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            //Defaults the file name to warandpace
            oFileDialog.FileName = "warandpeace.txt";

            //If the user selects a file and not cancel
            if (oFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Shows that the program is running
                Cursor.Current = Cursors.WaitCursor;

                //Sets the label above the outputBox to the name of file by
                //first taking the whole name, then splitting it by the \
                //and taking the string after the last \ (the file name)
                string[] fileNameArray = oFileDialog.FileName.Split('\\');
                fileName.Text = fileNameArray[fileNameArray.Length - 1];

                //Gets filename path
                filePath = oFileDialog.FileName;

                //The user wants character counter
                if (extraChars.Checked)
                {
                    CharRead(filePath);
                }
                else
                {
                    //Else just word counter
                    StandardRead(filePath);
                }

                //Update stat box with the filled list
                UpdateStats();

                //Can't select new file until we clear
                clearFile.Visible = true;
                selectFile.Visible = false;

                //Sort by frequency button is now visible
                sortFreqButton.Visible = true;

                //If we are not checking by char
                if (!extraChars.Checked)
                {
                    //Show longest word is now visible
                    longestWordsShow.Visible = true;
                }
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
        /// Counts characters that appear in file
        /// </summary>
        /// <param name="iFile">input file</param>
        private void CharRead(string iFile)
        {
            //Open the text selected into the StreamReader
            StreamReader inputFile = File.OpenText(oFileDialog.FileName);
            //Character read
            char tempChar;

            //While we are still reading
            while (!inputFile.EndOfStream)
            {
                //Read a char
                tempChar = (char)inputFile.Read();

                if (!caseSensitive.Checked)
                {
                    //If the user wants case sensitive results
                    tempChar = char.ToLower(tempChar);
                }

                if (!char.IsWhiteSpace(tempChar))
                {
                    //Updates list
                    UpdateList(tempChar.ToString());
                }
            }
        }

        /// <summary>
        /// Reads words in a file seperated by whitespace
        /// </summary>
        /// <param name="iFile">Not used</param>
        private void StandardRead(string filePath)
        {
            //Gets all words from file
            string allFile = File.ReadAllText(filePath);

            string[] wordArray = allFile.Split();
            string tempString;

            //Holds the size of array from splitting file
            int sizeOfArray = wordArray.Length;

            //for the length of created split array
            for (int i = 0; i < sizeOfArray; i++)
            {
                //Strips punctuation
                tempString = StripPunctuation(wordArray[i]);

                //If not whitespace
                if (!string.IsNullOrWhiteSpace(tempString))
                {
                    //If user wants case sesitivity
                    if (!caseSensitive.Checked)
                    {
                        //Changes the word to lowercase
                        tempString = tempString.ToLower();
                    }

                    //Updates list
                    UpdateList(tempString);
                }
            }
        }

        /// <summary>
        /// Strips punctuation off of string passed and returns new string
        /// </summary>
        /// <param name="str">String to remove punctuations from</param>
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
        /// Puts the word passed into a sorted list
        /// </summary>
        /// <param name="w">Word to be inserted</param>
        private void UpdateList(string w)
        {
            //Holds the number of items in outputBox
            int rightBounds = backList.Count - 1;
            int leftBounds = 0;     //Left bounds starts at 0
            int middleObj = 0;      //Holds what the middle object is
            int comparisonResults;  //Holds the results of comparison between strings
            bool wordFound = false; //Holds whether word was found or not

            //If list is empty then we should just add word
            if (backList.Count > 0)
            {
                //While we are still within range and the words hasn't been found
                while (leftBounds <= rightBounds && !wordFound)
                {

                    //Middle object is the right bounds plus the left bounds (total size)
                    //divided by 2
                    middleObj = (rightBounds + leftBounds) / 2;

                    //Get the comparison between the two strings
                    comparisonResults =
                        w.CompareTo(backList[middleObj].getWord());

                    //If the word to be update is found
                    if (comparisonResults < 0)
                    {
                        //String was smaller alphabetically so we must move right bounds
                        //towards center - 1
                        rightBounds = middleObj - 1;
                    }
                    else if (comparisonResults > 0)
                    {
                        //String was higher alphabetically so we must move right bounds
                        //towards center + 1
                        leftBounds = ++middleObj;
                    }
                    else
                    {
                        //Increments object
                        backList[middleObj].incrementNum();
                        //Word has been found
                        wordFound = true;
                    }
                }
            }

            //If not found
            if (!wordFound)
            {
                //Create new object to insert
                wordsCounted tempObj = new wordsCounted(w);

                //Insert at sorted position
                backList.Insert(middleObj, tempObj);
            }
        }

        /// <summary>
        /// Updates statistics box
        /// </summary>
        private void UpdateStats()
        {
            string getCommonWord = "None found";    //Starts with not found
            int numOfCommonWord = 0;    //Holds number of times word is found
            int getNumOfWords = 0;      //Number of words total
            int getNumOfLetters = 0;    //Number of letters total

            //For the amount of items on the list
            for (int i = 0, sizeOfList = backList.Count; i < sizeOfList; i++)
            {

                //If the object has a higher count than current highest count
                if (backList[i].getNum() > numOfCommonWord &&
                    backList[i].getWord() != "")
                {
                    //It is our new most common word
                    getCommonWord = (backList[i]).getWord();
                    //And updates the count
                    numOfCommonWord = backList[i].getNum();
                }

                //Add the number of times each word shows up
                getNumOfWords += backList[i].getNum();

                //Add up the length of each word (letters in word)
                getNumOfLetters +=
                    backList[i].getWord().Length;
            }

            //Outputs results to labels
            //Outputs common word and its count
            mostCommonWord.Text = getCommonWord + " | " +
                numOfCommonWord.ToString();
            //Outputs number of words
            numOfWords.Text = getNumOfWords.ToString();
            //Size of list is the number of unique words
            numOfUniqueWords.Text = backList.Count.ToString();

            //Only applies if we are doing by words
            if (!extraChars.Checked)
            {
                //Outputs number of letters
                numOfLetters.Text = getNumOfLetters.ToString();
                //Gets the number of letters and divides them by number
                //of words to get average letters
                avgLettersPerWord.Text = string.Format("{0:0.00}",
                    ((double)getNumOfLetters / getNumOfWords));

            }

            //Update the listBox to show final counts for items
            UpdateListBox();
        }

        /// <summary>
        /// Updates output box with final list
        /// </summary>
        private void UpdateListBox()
        {
            //Gets number of items
            int count = backList.Count;
            //Suspends updating outputBox until finished
            outputBox.SuspendLayout();

            //For the number of items
            for (int i = 0; i < count; ++i)
            {
                //Input item
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

            //Defaults save file name to SaveFile.txt
            sFileDialog.FileName = "SaveFile.txt";

            //If user selects file
            if (sFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Create the text file
                outFile = File.CreateText(sFileDialog.FileName);

                //Top of file. Shows file name and a bit of credits
                outFile.WriteLine("Statistics for file: " + fileName.Text);
                outFile.WriteLine("Gathered using Word Counter program.");
                outFile.WriteLine("\n");

                //Write out the label for most common word and its
                //value
                outFile.WriteLine(mostCommonWordLabel.Text + " " +
                    mostCommonWord.Text);
                //Write out the number of words in file and its result
                outFile.WriteLine(numOfWordsLabel.Text + " " +
                    numOfWords.Text);
                //Write out the number of unique words in file and its result
                outFile.WriteLine(numOfUniqueWordsLabel.Text + " " +
                    numOfUniqueWords.Text);

                //Only appliest if we regular word search not char search
                if (!extraChars.Checked)
                {
                    //Write out the number of letters and its result
                    outFile.WriteLine(numOfLettersLabel.Text + " " +
                        numOfLetters.Text);
                    //Write out the average letters per word and its result
                    outFile.WriteLine(avgLettersPerWordLabel.Text + " " +
                        avgLettersPerWord.Text);
                }

                //Space out lines
                outFile.WriteLine("\n");

                //If the user checked that he wants the list along
                //with the statistics
                if (saveList.Checked)
                {
                    for (int i = 0, count = outputBox.Items.Count; i < count; i++)
                    {
                        //Write out the contents of the outputBox list
                        //line by line into the file
                        outFile.WriteLine(((wordsCounted)outputBox.Items[i]).ToString());
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
            backList = new List<wordsCounted>();

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
            if (extraChars.Checked)
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

                //Resets starts as parameters have changed
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

                //Resets starts as parameters have changed
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
            //Get the count once to not have to recalculate
            int lengthOfList = backList.Count;
            //Parallel array of bools
            bool[] usedWords = new bool[lengthOfList];
            int largestNum;     //Current largest num
            int curLargestIndex;//Index of largest num

            //Initialize to all false
            for (int i = 0; i < lengthOfList; ++i)
            {
                usedWords[i] = false;
            }

            //For the amount of items in the list
            for (int i = 0; i < lengthOfList; ++i)
            {
                //Starting at largest number being 0
                largestNum = 0;
                curLargestIndex = 0;

                //For the entire list
                for (int j = 0; j < lengthOfList; ++j)
                {
                    //If the bool array still has this word as not used
                    //and the number of it is largest than the current
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
            if (sortFreqButton.Text == "▼")
            {
                //We sort by frequency
                SortByFrequency();
                //Change the button for the user to sort back
                sortFreqButton.Text = "Θ";
            }
            else
            {
                //We do the regular sort again
                UpdateListBox();
                //Change the button back
                sortFreqButton.Text = "▼";
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

            MessageBox.Show(tempString.ToString());
        }
    }
}
