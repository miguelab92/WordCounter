// @brief Read a file and output the number of times each word appears
// @author Miguel Bermudez
// @version 2016-02-26

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Counter
{
    public partial class wordCounter : Form
    {
        /// <summary>
        /// Initializes form
        /// </summary>
        public wordCounter()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Lets user select a file. Also beginning method of processing
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void selectFile_Click(object sender, EventArgs e)
        {
            //Input file
            StreamReader inputFile;
            //List of wordsCounted. Holds results
            List<wordsCounted> wordList = new List<wordsCounted>();
            //Sets the outputBox's source to the created list
            outputBox.DataSource = wordList;

            //Starting path for file dialog
            oFileDialog.InitialDirectory = @"C:\Users\Soulin\Documents\
                                Programs\cSharp\Test Project\Word Counter";

            //If the user selects a file and not cancel
            if (oFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Shows that the program is running
                Cursor.Current = Cursors.WaitCursor;
                //Open the text selected into the StreamReader
                inputFile = File.OpenText(oFileDialog.FileName);

                //If the user wants all chars
                if (extraChars.Checked)
                {
                    allCharRead(inputFile, wordList);
                }
                else
                {
                    //Else just words
                    standardRead(inputFile, wordList);
                }

                //Close file
                inputFile.Close();
                //Update stat box with wordList
                updateStatistics(wordList);
                //Sets the cursor back to default. Done!
                Cursor.Current = Cursors.Default;
            }
            else
            {
                //User did not select a file
                MessageBox.Show("Please select a file.");
            }
        }

        /// <summary>
        /// Reads character by character including whitespace and
        /// punctuation
        /// </summary>
        /// <param name="iFile">Not used</param>
        /// <param name="wordList">Not used</param>
        private void allCharRead(StreamReader iFile, List<wordsCounted> wordList)
        {
            //Current word being built
            string wordRead;
            //Character read
            char tempChar;
            //

            //While not at the end of the stream
            while (!iFile.EndOfStream)
            {
                //Read a char
                tempChar = (char)iFile.Read();

                //If the char that was added is part of a word (not
                //white space or punctuation)
                if (Char.IsLetterOrDigit(tempChar))
                {
                    //Saves the char as the start of a word
                    wordRead = tempChar.ToString();

                    //While its still a word
                    while (Char.IsLetterOrDigit(tempChar))
                    {
                        //Consumes the next char from file
                        tempChar = (char)iFile.Read();
                        //If it is part of the word and not punctuation
                        wordRead += tempChar.ToString();
                        //Looks at the next char to check if its punctuation
                        tempChar = (char)iFile.Peek();
                    }
                } else
                {
                    //Else the letter is a non alphanumeric char
                    wordRead = tempChar.ToString();
                }
                if (!caseSensitive.Checked)
                {
                    //If the user wants case sensitive results
                    wordRead = wordRead.ToLower();
                }

                //Updates list
                updateList(wordRead, wordList);
            }
        }
        /// <summary>
        /// Reads words in a file line by line
        /// </summary>
        /// <param name="iFile">Not used</param>
        /// <param name="wordList">Not used</param>
        private void standardRead(StreamReader iFile, List<wordsCounted> wordList)
        {
            string tempRead; //Temporarily holds each line read

            //While not end of file
            while (!iFile.EndOfStream)
            {
                //Gets a line from the file
                tempRead = iFile.ReadLine();

                //Creates an array of strings that contain the words from the
                //line read. These are separated using the Split function and
                //the terminator by which is split is null due to documentation
                //in String.Split() which says a null as the argument will use
                //any whitespace characters as the delimeter.
                string[] wordArray = tempRead.Split(null);

                //for the length of created split array
                for (int i = 0; i < wordArray.Length; i++)
                {
                    //If user wants case sesitivity
                    if (!caseSensitive.Checked)
                    {
                        //Changes the word to lowercase
                        wordArray[i] = wordArray[i].ToLower();
                    }

                    //Updates list
                    updateList(wordArray[i], wordList);
                }
            }
        }

        /// <summary>
        /// Updates ouput box for user to see the words read
        /// </summary>
        /// ******Could be upgraded by adding a sort and upgrading
        /// this to a binary search ********
        /// <param name="w">Not used</param>
        /// <param name="wordList">Not used</param>
        private void updateList(string w, List<wordsCounted> wordList)
        {
            //If the word has not already been added to list (assumed)
            bool wordFound = false;

            //For the amount of items in the list and while we haven't found
            //the word
            for (int i = 0; i < wordList.Count && !wordFound; i++)
            {
                //If the word to be update is found
                if (w == wordList[i].getWord())
                {
                    //Increments object
                    wordList[i].incrementNum();
                    //Sets found to true
                    wordFound = true;
                }
            }

            //If not found
            if (!wordFound)
            {
                //Adds it to list
                wordsCounted tempWord = new wordsCounted(w);
                wordList.Add(tempWord);
            }
        }

        /// <summary>
        /// Updates statistics box
        /// </summary>
        /// <param name="wordList"></param>
        private void updateStatistics(List<wordsCounted> wordList)
        {
            //Declarations and initialization of all variables
            string getCommonWord = "None found";    //Starts with not found
            int numOfCommonWord = 0;    //Holds number of times word is found
            int getNumOfWords = 0;      //Number of words total
            int getNumOfLetters = 0;    //Number of letters total

            //For the amount of items on the list
            for (int i = 0; i < wordList.Count; i++)
            {
                //If the object has a higher count than current highest count
                if (wordList[i].getNum() > numOfCommonWord && 
                    wordList[i].getWord() != "" )
                {
                    //It is our new most common word
                    getCommonWord = wordList[i].getWord();
                    //And updates the count
                    numOfCommonWord = wordList[i].getNum();
                }

                //Add the number of times each word shows up
                getNumOfWords += wordList[i].getNum();

                //Add up the length of each word (letters in word)
                getNumOfLetters += (wordList[i].getWord()).Length;
            }

            //Outputs results to labels
            //Outputs common word and its count
            mostCommonWord.Text = getCommonWord + " " + 
                numOfCommonWord.ToString();
            //Outputs number of words
            numOfWords.Text = getNumOfWords.ToString();
            //Size of list is the number of unique words
            numOfUniqueWords.Text = (wordList.Count).ToString();
            //Outputs number of letters
            numOfLetters.Text = getNumOfLetters.ToString();
            //Gets the number of letters and divides them by number
            //of words to get average letters
            avgLettersPerWord.Text = 
                ((double)getNumOfLetters / getNumOfWords).ToString();

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
                outFile = File.CreateText(sFileDialog.FileName);

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
                //Write out the number of letters and its result
                outFile.WriteLine(numOfLettersLabel.Text + " " + 
                    numOfLetters.Text);
                //Write out the average letters per word and its result
                outFile.WriteLine(avgLettersPerWordLabel.Text + " " +
                    avgLettersPerWord.Text);
                
                //If the user checked that he wants the list along
                //with the statistics
                if ( saveList.Checked )
                {
                    for ( int i = 0; i < outputBox.Items.Count; i++)
                    {
                        //Write out the contents of the outputBox list
                        //line by line into the file
                        outFile.WriteLine(outputBox.Items[i].ToString());
                    }
                }
                //Close output file
                outFile.Close();
            }
            else
            {
                //Error getting file
                MessageBox.Show("Please pick a file to save to.");
            }
        }

        private void clearFile_Click(object sender, EventArgs e)
        {
            /* Temporary! (?)
             * Resets the list by making its datasource a new List
             */
            List<wordsCounted> wordList = new List<wordsCounted>();
            outputBox.DataSource = wordList;

            //Resets all the labels
            mostCommonWord.Text = "";
            numOfWords.Text = "";
            numOfUniqueWords.Text = "";
            numOfLetters.Text = "";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //Closes this class
            this.Close();
        }

        private void wordCounter_Load(object sender, EventArgs e)
        {
            //Sets the labels on startup to blank
            mostCommonWord.Text = "";
            numOfWords.Text = "";
            numOfUniqueWords.Text = "";
            numOfLetters.Text = "";
        }
    }
}
