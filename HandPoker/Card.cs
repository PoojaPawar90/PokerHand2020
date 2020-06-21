using System;
using System.Collections.Generic;
using System.Text;


namespace HandPoker
{
    class Card
    {

        public string suit;
        public int value;
        
        
        public Card(string v, string c)
        {
            convert(v);
            suit = c;
        }

        // This will convert 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace* in to nos.
        public void convert(string x)
        {
            switch (x)
            {
                case "A":
                    value = 14;
                    return;
                case "K":
                    value = 13;
                    return;

                case "Q":
                    value = 12;
                    return;

                case "J":
                    value = 11;
                    return;

                case "T":
                    value = 10;
                    return;

                default:
                    value = int.Parse(x);
                    return;
            }
        }
    }
}
