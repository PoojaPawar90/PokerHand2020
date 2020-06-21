using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HandPoker
{
    class Player
    {
        public List<Card> card = new List<Card>();
        public HandRank rank;
        public List<int> valuesToCompare = new List<int> { 0, 0, 0, 0, 0 };

        public Player(string s)
        {
            card.Add(new Card(s[0].ToString(), s[1].ToString()));
            card.Add(new Card(s[3].ToString(), s[4].ToString()));
            card.Add(new Card(s[6].ToString(), s[7].ToString()));
            card.Add(new Card(s[9].ToString(), s[10].ToString()));
            card.Add(new Card(s[12].ToString(), s[13].ToString()));

            card = card.OrderBy(x => x.value).ToList();
            rank = getRank();
            setValueCardsToCompare();
        }


        public bool isStraight()
        {
            for (int i = 0; i < 4; i++)
            {
                if (card[i].value + 1 != card[i + 1].value)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isFlush()
        {
            for (int i = 0; i < 4; i++)
            {
                if (card[i].suit != card[i + 1].suit)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isRoyalFlush()
        {
            //int[] royalValue = { 10,11,12,13,14 };
            int[] nonRoyalFlushValues = { 2,3,4,5,6,7,8,9 };
            for (int i = 0; i < 4; i++)
            {
                if (nonRoyalFlushValues.Contains(card[i].value) || card[i].suit != card[i + 1].suit)
                {
                    return false;
                }

            }
            return true;
        }
        private bool isFourKind()
        {
            if (card[0].value == card[1].value && card[1].value == card[2].value && card[2].value == card[3].value)
            {
                return true;
            }
            if (card[1].value == card[2].value && card[2].value == card[3].value && card[3].value == card[4].value)
            {
                return true;
            }
            return false;
        }

        private bool isThreeKind()
        {
            if (card[0].value == card[2].value)
            {
                return true;
            }
            if (card[1].value == card[3].value)
            {
                return true;
            }
            if (card[2].value == card[4].value)
            {
                return true;
            }
            return false;
        }

        private int numOfPairs()
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (card[i].value == card[i + 1].value)
                {
                    count++;
                    i++;
                }
            }
            return count;
        }

        private HandRank getRank()
        {
            bool flag1 = isStraight();
            bool flag2 = isFlush();
            bool flag3 = isFourKind();
            bool flag4 = isThreeKind();
            bool flag5 = isRoyalFlush();
            
            int pair = numOfPairs();

            if (flag5)
            {
                return HandRank.ROYAL_FLUSH;

            }
            if (flag1 && flag2)
            {
                return HandRank.STRAIGHT_FLUSH;
            }
            if (flag3)
            {
                return HandRank.FOUR_KIND;
            }
            if (flag4 && pair == 2)
            {
                return HandRank.FULL_HOUSE;
            }
            if (flag2)
            {
                return HandRank.FLUSH;
            }
            if (flag1)
            {
                return HandRank.STRAIGHT;
            }
            if (flag4)
            {
                return HandRank.THREE_KIND;
            }
            
            if (pair == 2)
            {
                return HandRank.TWO_PAIR;
            }
            if (pair == 1)
            {
                return HandRank.PAIR;
            }

            return HandRank.HIGH_CARD;
        }
        private void setValueCardsToCompare()
        {
            //Console.WriteLine("Rank: "+rank);
            switch (rank)
            {
                case HandRank.ROYAL_FLUSH: 
                case HandRank.STRAIGHT_FLUSH: 
                case HandRank.STRAIGHT:
                    valuesToCompare[0] = card[4].value;
                    break;
                case HandRank.FLUSH: 
                case HandRank.HIGH_CARD:
                    for (int i = 0; i < 5; ++i)
                    {
                        valuesToCompare[i] = card[4 - i].value;
                    }
                    break;
                case HandRank.FULL_HOUSE:
                case HandRank.FOUR_KIND: 
                case HandRank.THREE_KIND:
                    valuesToCompare[0] = card[2].value;
                    break;
                case HandRank.TWO_PAIR:
                    //Only three position for the single card: 0, 2, 4.
                    if (card[0].value != card[1].value)
                    { //ex. 2 33 44
                        valuesToCompare[0] = card[4].value;
                        valuesToCompare[1] = card[2].value;
                        valuesToCompare[2] = card[0].value;
                    }
                    else if (card[2].value != card[3].value)
                    { //ex.33 4 55
                        valuesToCompare[0] = card[4].value;
                        valuesToCompare[1] = card[0].value;
                        valuesToCompare[2] = card[2].value;
                    }
                    else
                    { //ex.55 66 7
                        valuesToCompare[0] = card[2].value;
                        valuesToCompare[1] = card[0].value;
                        valuesToCompare[2] = card[4].value;
                    }
                    break;
                case HandRank.PAIR:
                    int pairValue = 0;
                    for (int i = 0; i < 3; ++i) //search pair
                    {
                        if (card[i].value == card[i + 1].value)
                        {
                            pairValue = card[i].value;
                            break;
                        }
                    }
                    valuesToCompare[0] = pairValue;
                    int r = 0;
                    if (pairValue != 0)
                        r = 1;
                    for (int i = 4; i >= 0; i--)
                        { //i for card, r for values
                            if (card[i].value == pairValue)
                            {
                                continue;
                            }
                            valuesToCompare[r++] = card[i].value;
                        }
                    break;
            }
        }

        public int compareTo(Player other)
        {
            //Console.WriteLine("Rank: "+rank);
            //Console.WriteLine("Other Rank: " + other.rank);
            //Console.WriteLine("Result: " +(rank > other.rank));
            //Console.WriteLine("Result: " + (rank < other.rank));
            if (rank > other.rank)
            {
                return 1;
            }
            if (rank < other.rank)
            {
                return -1;
            }

            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine(valuesToCompare[i]);
                //Console.WriteLine(other.valuesToCompare[i]);
                if (valuesToCompare[i] > other.valuesToCompare[i])
                    return 1;
                if (valuesToCompare[i] < other.valuesToCompare[i])
                    return -1;
            }
            return 0;

        }
    }
    public enum HandRank
    {
        HIGH_CARD,
        PAIR,
        TWO_PAIR,
        THREE_KIND,
        STRAIGHT,
        FLUSH,
        FULL_HOUSE,
        FOUR_KIND,
        STRAIGHT_FLUSH,
        ROYAL_FLUSH
    }


}
