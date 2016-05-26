namespace Word_Counter
{
    partial class wordCounter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputBox = new System.Windows.Forms.ListBox();
            this.statsBox = new System.Windows.Forms.GroupBox();
            this.longestWordsShow = new System.Windows.Forms.Button();
            this.longestWordLabel = new System.Windows.Forms.Label();
            this.avgLettersPerWord = new System.Windows.Forms.Label();
            this.avgLettersPerWordLabel = new System.Windows.Forms.Label();
            this.numOfLetters = new System.Windows.Forms.Label();
            this.numOfLettersLabel = new System.Windows.Forms.Label();
            this.numOfUniqueWords = new System.Windows.Forms.Label();
            this.numOfUniqueWordsLabel = new System.Windows.Forms.Label();
            this.numOfWords = new System.Windows.Forms.Label();
            this.mostCommonWord = new System.Windows.Forms.Label();
            this.numOfWordsLabel = new System.Windows.Forms.Label();
            this.mostCommonWordLabel = new System.Windows.Forms.Label();
            this.optionsBox = new System.Windows.Forms.GroupBox();
            this.caseSensitive = new System.Windows.Forms.CheckBox();
            this.saveList = new System.Windows.Forms.CheckBox();
            this.extraChars = new System.Windows.Forms.CheckBox();
            this.selectFile = new System.Windows.Forms.Button();
            this.saveStatistics = new System.Windows.Forms.Button();
            this.clearFile = new System.Windows.Forms.Button();
            this.oFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.sFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileName = new System.Windows.Forms.Label();
            this.sortFreqButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.statsBox.SuspendLayout();
            this.optionsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.FormattingEnabled = true;
            this.outputBox.ItemHeight = 14;
            this.outputBox.Location = new System.Drawing.Point(12, 23);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(238, 200);
            this.outputBox.TabIndex = 0;
            // 
            // statsBox
            // 
            this.statsBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.statsBox.Controls.Add(this.longestWordsShow);
            this.statsBox.Controls.Add(this.longestWordLabel);
            this.statsBox.Controls.Add(this.avgLettersPerWord);
            this.statsBox.Controls.Add(this.avgLettersPerWordLabel);
            this.statsBox.Controls.Add(this.numOfLetters);
            this.statsBox.Controls.Add(this.numOfLettersLabel);
            this.statsBox.Controls.Add(this.numOfUniqueWords);
            this.statsBox.Controls.Add(this.numOfUniqueWordsLabel);
            this.statsBox.Controls.Add(this.numOfWords);
            this.statsBox.Controls.Add(this.mostCommonWord);
            this.statsBox.Controls.Add(this.numOfWordsLabel);
            this.statsBox.Controls.Add(this.mostCommonWordLabel);
            this.statsBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsBox.Location = new System.Drawing.Point(256, 20);
            this.statsBox.Name = "statsBox";
            this.statsBox.Size = new System.Drawing.Size(262, 114);
            this.statsBox.TabIndex = 1;
            this.statsBox.TabStop = false;
            this.statsBox.Text = "Statistics";
            // 
            // longestWordsShow
            // 
            this.longestWordsShow.Location = new System.Drawing.Point(241, 89);
            this.longestWordsShow.Name = "longestWordsShow";
            this.longestWordsShow.Size = new System.Drawing.Size(15, 15);
            this.longestWordsShow.TabIndex = 10;
            this.longestWordsShow.Text = "╜";
            this.longestWordsShow.UseVisualStyleBackColor = true;
            this.longestWordsShow.Visible = false;
            this.longestWordsShow.Click += new System.EventHandler(this.longestWordsShow_Click);
            // 
            // longestWordLabel
            // 
            this.longestWordLabel.AutoSize = true;
            this.longestWordLabel.Location = new System.Drawing.Point(6, 89);
            this.longestWordLabel.Name = "longestWordLabel";
            this.longestWordLabel.Size = new System.Drawing.Size(92, 14);
            this.longestWordLabel.TabIndex = 18;
            this.longestWordLabel.Text = "Longest word(s):";
            // 
            // avgLettersPerWord
            // 
            this.avgLettersPerWord.Location = new System.Drawing.Point(188, 72);
            this.avgLettersPerWord.Name = "avgLettersPerWord";
            this.avgLettersPerWord.Size = new System.Drawing.Size(68, 14);
            this.avgLettersPerWord.TabIndex = 17;
            this.avgLettersPerWord.Text = "0";
            this.avgLettersPerWord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // avgLettersPerWordLabel
            // 
            this.avgLettersPerWordLabel.AutoSize = true;
            this.avgLettersPerWordLabel.Location = new System.Drawing.Point(6, 72);
            this.avgLettersPerWordLabel.Name = "avgLettersPerWordLabel";
            this.avgLettersPerWordLabel.Size = new System.Drawing.Size(133, 14);
            this.avgLettersPerWordLabel.TabIndex = 16;
            this.avgLettersPerWordLabel.Text = "Average letters per word:";
            // 
            // numOfLetters
            // 
            this.numOfLetters.Location = new System.Drawing.Point(157, 58);
            this.numOfLetters.Name = "numOfLetters";
            this.numOfLetters.Size = new System.Drawing.Size(99, 14);
            this.numOfLetters.TabIndex = 15;
            this.numOfLetters.Text = "0";
            this.numOfLetters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numOfLettersLabel
            // 
            this.numOfLettersLabel.AutoSize = true;
            this.numOfLettersLabel.Location = new System.Drawing.Point(6, 58);
            this.numOfLettersLabel.Name = "numOfLettersLabel";
            this.numOfLettersLabel.Size = new System.Drawing.Size(96, 14);
            this.numOfLettersLabel.TabIndex = 14;
            this.numOfLettersLabel.Text = "Number of letters: ";
            // 
            // numOfUniqueWords
            // 
            this.numOfUniqueWords.Location = new System.Drawing.Point(157, 44);
            this.numOfUniqueWords.Name = "numOfUniqueWords";
            this.numOfUniqueWords.Size = new System.Drawing.Size(99, 14);
            this.numOfUniqueWords.TabIndex = 13;
            this.numOfUniqueWords.Text = "0";
            this.numOfUniqueWords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numOfUniqueWordsLabel
            // 
            this.numOfUniqueWordsLabel.AutoSize = true;
            this.numOfUniqueWordsLabel.Location = new System.Drawing.Point(6, 44);
            this.numOfUniqueWordsLabel.Name = "numOfUniqueWordsLabel";
            this.numOfUniqueWordsLabel.Size = new System.Drawing.Size(130, 14);
            this.numOfUniqueWordsLabel.TabIndex = 12;
            this.numOfUniqueWordsLabel.Text = "Number of unique words:";
            // 
            // numOfWords
            // 
            this.numOfWords.Location = new System.Drawing.Point(157, 30);
            this.numOfWords.Name = "numOfWords";
            this.numOfWords.Size = new System.Drawing.Size(99, 14);
            this.numOfWords.TabIndex = 11;
            this.numOfWords.Text = "0";
            this.numOfWords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mostCommonWord
            // 
            this.mostCommonWord.Location = new System.Drawing.Point(160, 16);
            this.mostCommonWord.Name = "mostCommonWord";
            this.mostCommonWord.Size = new System.Drawing.Size(96, 14);
            this.mostCommonWord.TabIndex = 10;
            this.mostCommonWord.Text = "[]";
            this.mostCommonWord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numOfWordsLabel
            // 
            this.numOfWordsLabel.AutoSize = true;
            this.numOfWordsLabel.Location = new System.Drawing.Point(6, 30);
            this.numOfWordsLabel.Name = "numOfWordsLabel";
            this.numOfWordsLabel.Size = new System.Drawing.Size(95, 14);
            this.numOfWordsLabel.TabIndex = 9;
            this.numOfWordsLabel.Text = "Number of words:";
            // 
            // mostCommonWordLabel
            // 
            this.mostCommonWordLabel.AutoSize = true;
            this.mostCommonWordLabel.Location = new System.Drawing.Point(6, 16);
            this.mostCommonWordLabel.Name = "mostCommonWordLabel";
            this.mostCommonWordLabel.Size = new System.Drawing.Size(108, 14);
            this.mostCommonWordLabel.TabIndex = 8;
            this.mostCommonWordLabel.Text = "Most common word: ";
            // 
            // optionsBox
            // 
            this.optionsBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.optionsBox.Controls.Add(this.caseSensitive);
            this.optionsBox.Controls.Add(this.saveList);
            this.optionsBox.Controls.Add(this.extraChars);
            this.optionsBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsBox.Location = new System.Drawing.Point(256, 140);
            this.optionsBox.Name = "optionsBox";
            this.optionsBox.Size = new System.Drawing.Size(262, 89);
            this.optionsBox.TabIndex = 2;
            this.optionsBox.TabStop = false;
            this.optionsBox.Text = "Optional Settings";
            // 
            // caseSensitive
            // 
            this.caseSensitive.AutoSize = true;
            this.caseSensitive.Location = new System.Drawing.Point(6, 41);
            this.caseSensitive.Name = "caseSensitive";
            this.caseSensitive.Size = new System.Drawing.Size(255, 18);
            this.caseSensitive.TabIndex = 2;
            this.caseSensitive.Text = "Consider upper or lower case (Case-Sensitive)";
            this.caseSensitive.UseVisualStyleBackColor = true;
            this.caseSensitive.CheckedChanged += new System.EventHandler(this.caseSensitive_CheckedChanged);
            // 
            // saveList
            // 
            this.saveList.AutoSize = true;
            this.saveList.Location = new System.Drawing.Point(6, 65);
            this.saveList.Name = "saveList";
            this.saveList.Size = new System.Drawing.Size(156, 18);
            this.saveList.TabIndex = 1;
            this.saveList.Text = "Save current list with stats";
            this.saveList.UseVisualStyleBackColor = true;
            // 
            // extraChars
            // 
            this.extraChars.AutoSize = true;
            this.extraChars.Location = new System.Drawing.Point(6, 19);
            this.extraChars.Name = "extraChars";
            this.extraChars.Size = new System.Drawing.Size(180, 18);
            this.extraChars.TabIndex = 0;
            this.extraChars.Text = "Count by character (not words)";
            this.extraChars.UseVisualStyleBackColor = true;
            this.extraChars.CheckedChanged += new System.EventHandler(this.extraChars_CheckedChanged);
            // 
            // selectFile
            // 
            this.selectFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.selectFile.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFile.Location = new System.Drawing.Point(282, 235);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(88, 30);
            this.selectFile.TabIndex = 3;
            this.selectFile.Text = "Select File";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // saveStatistics
            // 
            this.saveStatistics.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saveStatistics.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveStatistics.Location = new System.Drawing.Point(398, 235);
            this.saveStatistics.Name = "saveStatistics";
            this.saveStatistics.Size = new System.Drawing.Size(88, 30);
            this.saveStatistics.TabIndex = 5;
            this.saveStatistics.Text = "Save Stats";
            this.saveStatistics.UseVisualStyleBackColor = true;
            this.saveStatistics.Click += new System.EventHandler(this.saveStatistics_Click);
            // 
            // clearFile
            // 
            this.clearFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.clearFile.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearFile.Location = new System.Drawing.Point(282, 235);
            this.clearFile.Name = "clearFile";
            this.clearFile.Size = new System.Drawing.Size(88, 30);
            this.clearFile.TabIndex = 6;
            this.clearFile.Text = "Clear";
            this.clearFile.UseVisualStyleBackColor = true;
            this.clearFile.Visible = false;
            this.clearFile.Click += new System.EventHandler(this.clearFile_Click);
            // 
            // oFileDialog
            // 
            this.oFileDialog.FileName = "openFileDialog1";
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Location = new System.Drawing.Point(12, 9);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(13, 13);
            this.fileName.TabIndex = 8;
            this.fileName.Text = "[]";
            // 
            // sortFreqButton
            // 
            this.sortFreqButton.Location = new System.Drawing.Point(225, 2);
            this.sortFreqButton.Name = "sortFreqButton";
            this.sortFreqButton.Size = new System.Drawing.Size(25, 20);
            this.sortFreqButton.TabIndex = 9;
            this.sortFreqButton.Text = "▼";
            this.sortFreqButton.UseVisualStyleBackColor = true;
            this.sortFreqButton.Visible = false;
            this.sortFreqButton.Click += new System.EventHandler(this.sortFreqButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(12, 241);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(238, 20);
            this.searchBox.TabIndex = 10;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(9, 226);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 19;
            this.searchLabel.Text = "Search:";
            // 
            // wordCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 278);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.sortFreqButton);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.clearFile);
            this.Controls.Add(this.saveStatistics);
            this.Controls.Add(this.selectFile);
            this.Controls.Add(this.optionsBox);
            this.Controls.Add(this.statsBox);
            this.Controls.Add(this.outputBox);
            this.MaximumSize = new System.Drawing.Size(547, 1080);
            this.MinimumSize = new System.Drawing.Size(547, 309);
            this.Name = "wordCounter";
            this.Text = "Word Counter";
            this.Load += new System.EventHandler(this.wordCounter_Load);
            this.statsBox.ResumeLayout(false);
            this.statsBox.PerformLayout();
            this.optionsBox.ResumeLayout(false);
            this.optionsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox outputBox;
        private System.Windows.Forms.GroupBox statsBox;
        private System.Windows.Forms.GroupBox optionsBox;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.Button saveStatistics;
        private System.Windows.Forms.Button clearFile;
        private System.Windows.Forms.OpenFileDialog oFileDialog;
        private System.Windows.Forms.SaveFileDialog sFileDialog;
        private System.Windows.Forms.CheckBox extraChars;
        private System.Windows.Forms.Label numOfUniqueWords;
        private System.Windows.Forms.Label numOfUniqueWordsLabel;
        private System.Windows.Forms.Label numOfWords;
        private System.Windows.Forms.Label mostCommonWord;
        private System.Windows.Forms.Label numOfWordsLabel;
        private System.Windows.Forms.Label mostCommonWordLabel;
        private System.Windows.Forms.Label numOfLetters;
        private System.Windows.Forms.Label numOfLettersLabel;
        private System.Windows.Forms.CheckBox saveList;
        private System.Windows.Forms.CheckBox caseSensitive;
        private System.Windows.Forms.Label avgLettersPerWord;
        private System.Windows.Forms.Label avgLettersPerWordLabel;
        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.Button sortFreqButton;
        private System.Windows.Forms.Label longestWordLabel;
        private System.Windows.Forms.Button longestWordsShow;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label searchLabel;
    }
}

