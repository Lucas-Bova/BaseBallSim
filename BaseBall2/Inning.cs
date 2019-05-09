using static System.Console;
using System;

namespace BaseBallSim
{
    class Inning //maybe seperate this into an at bat class that inherits from inning, so we can eventually create a pitcher class
    {
        private int outs = 0;
        protected bool[] onBase = { false, false, false, false };
        private int score = 0;
        private int index = 0;


        public int Outs
        {
            get
            {
                return outs;
            }
            set
            {
                //check if the number of outs is equal to or greater than three
                outs = value;
                if (outs >= 3)
                {
                    PrintOutPut("Inning over");
                }
            }
        }
        public int Score { get; set; }
        protected int Index
        {
            get
            {
                return index;
            }
            set
            {
                //reset the index if the value is greater than 8
                //this simulates going through the entire order
                index = value;
                if (index > 8)
                {
                    index = 0;
                }
            }
        }

        //move base runners when there is a hit
        //
        protected void trackBase(int hitAmount)
        {
            //add on base element for each hit, if array is full score a run
            //move the elmeents of the array forward the amount of hitAmount if the movement would move the element to or past the last elment in the arry score a run.
            //decrements through the array to move the runners in order
            //a runner cannot advance until the runner infront of them has advanced
            for (int i = onBase.Length - 2; i >= 0; --i)
            {
                if (onBase[i])
                {
                    if (i + hitAmount >= onBase.Length - 1)
                    {
                        onBase[i] = false;
                        Score++;
                    }
                    else
                    {
                        onBase[i] = false;
                        onBase[i + hitAmount] = true;
                    }
                }
            }
            onBase[hitAmount - 1] = true;
        }

        //move base runners when there is a walk
        //
        protected void trackBase()
        {
            //move a player onto first, only advance runners if they are connected to first
            //0 = first base, 1 = second base, 2 = third base
            //decrements through the array to move the runners in order
            //a runner cannot advance until the runner infront of them has advanced
            for (int i = onBase.Length -2; i >= 0; --i)
            {
                //if bases are loaded
                // i > 1 allows us to determine quickly if we are dealing with the third base element of the array and to not throw an index out of range exception
                if (i > 1 && onBase[i] && onBase[i-1] && onBase[i-2])
                {
                    Score++;
                }
                //if runner on second and first
                //i > 0 allows us to determine quickly if we are dealing with the second base element of the array and to not throw an index out of range exception
                else if (i > 0 && onBase[i] && onBase[i-1])
                {
                    onBase[i + 1] = true;
                }
                //if runner on first and only on first
                else if (i == 0 && onBase[i])
                {
                    onBase[i + 1] = true;
                }
                //if no runners are onone on
                //i == 0 tells us that this is the last loop so we should go ahead and put on a runner here
                else if (i == 0)
                {
                    onBase[0] = true;
                }
            }
        }

        //loops through the onBase array to determine if a runner is on base
        //returns a string with all runners on base
        protected string WhoIsOn()
        {
            string returnString = "Runners on: ";
            for (int i = 0; i < onBase.Length; ++i)
            {
                if (onBase[i])
                {
                    if (i == 0)
                    {
                        returnString += "1st ";
                    }
                    else if (i == 1)
                    {
                        returnString += "2nd ";
                    }
                    else if (i == 2)
                    {
                        returnString += "3rd ";
                    }
                }
            }
            return returnString;
        }

        //printes the a given string with ** pading 
        //this is currently a pure console method
        protected void PrintOutPut(string output)
        {
            WriteLine("********************");
            WriteLine("********************");
            WriteLine(output);
            WriteLine("********************");
            WriteLine("********************");
        }

        //displays the line up of batters
        protected void printLineUp(Player[] players)
        {
            string lineup = "--Lineup-- ";
            for (int i = 0; i < players.Length; ++i)
            {
                lineup += "\n" + players[i].ToString();
            }
            PrintOutPut(lineup);
        }
    }
}
