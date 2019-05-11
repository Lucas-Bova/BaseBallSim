using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BaseBallSim
{
    /*
     * create 9 player objects, one for each position
     * randomly sort the player objects
     * 
     * in the inning, use a different object for each at bat
     */
    class Program
    {
        static void Main(string[] args)
        {
            Write("Play or Sim? (p for play and s for sim): ");
            var tempChar = char.Parse(ReadLine());
            if (tempChar == 's')
            {
                Write("How many innings? ");
                var tempInt = int.Parse(ReadLine());
                RunSim(tempInt);
            }
            Player[] playerArr = new Player[9];
            for (int i = 0; i < playerArr.Length; ++i)
            {
                playerArr[i] = new Player(i);
            }
            Pitcher pitcher = new Pitcher();
            AtBat atbat = new AtBat();
            atbat.AtBatIN(shuffleArray(playerArr), pitcher);
            Read();
        }

        //shuffles the arary 
        private static Player[] shuffleArray(Player[] players)
        {
            Random rand = new Random();

            //using the fisher-yates algorithm because I wanted to learn how
            int n = players.Length;
            while (n > 1)
            {
                //get a random number form 0 - the max length of the array
                //decrement n which is the object that represents the elements to shuffle
                int k = rand.Next(n--);
                //store the n'ith element in a temporary variable
                Player temp = players[n];
                //set the n'ith element (the last element in the array to not have been assigned a new variable by the algorithm) 
                //equal to the k'ith element (a random number between 0 (the first element) and n (the greatest element in the array that has not yet been assigned)
                players[n] = players[k];
                //assign the k'ith element the value of the n'ith element
                //this essesntially swaps the k and n elements
                players[k] = temp;
            }
            return players;
        }
        private static void RunSim(int iterations)
        {
            int totalRuns = 0;
            //add more data here

            Player[] playerArr = new Player[9];
            for (int i = 0; i < playerArr.Length; ++i)
            {
                playerArr[i] = new Player(i);
            }
            Pitcher pitcher = new Pitcher();
            for (int i = 0; i < iterations; ++i)
            {
                AtBat atbat = new AtBat();
                atbat.atBatSim(shuffleArray(playerArr), pitcher);
                //get values from the at bat object and add them to running totals
                totalRuns += atbat.Score;
            }
            WriteLine("Total number of runs scored in {0} innings is {1}", iterations, totalRuns);
            Read();
        }
    }
}
