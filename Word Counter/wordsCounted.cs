using System.Text;

namespace Word_Counter
{
    class WordsCounted
    {
        private string _word;    //Holds word
        private int _num;        //Holds number of times word is seen

        //Initializes at 1 for a new word
        public WordsCounted( string w = "" ) { _word = w; _num = 1; }
        //Returns private word variable
        public string getWord() { return _word; }
        //Returns private num variable
        public int getNum() { return _num; }
        //Increments the num variable by one
        public void incrementNum() { ++_num; }
        //Overwrites the ToString function to return the word and number
        public override string ToString()  { return string.Format("{0, -19} {1,10}", _word, _num); }

    }
}
