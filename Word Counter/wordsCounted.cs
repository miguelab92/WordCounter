namespace Word_Counter
{
    class wordsCounted
    {
        private string word;    //Holds word
        private int num;        //Holds number of times word is seen

        //Initializes at 1 for a new word
        public wordsCounted(string w) { word = w; num = 1; }
        //Returns private word variable
        public string getWord() { return word; }
        //Returns private num variable
        public int getNum() { return num; }
        //Increments the num variable by one
        public void incrementNum() { ++num; }
        //Overwrites the ToString function to return the word, followed
        //by a space, and finally the number
        public override string ToString()
        { return word + " - Count: " + num.ToString(); }
    }
}
