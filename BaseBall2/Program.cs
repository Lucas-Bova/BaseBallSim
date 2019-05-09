using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BaseBallSim
{
    /*
     * create 9 player objects, one for each position... array???
     * randomly sort the player objects
     * 
     * int the inning, use a different object for each at bat
     * have a method that returns the next player object?
     * use the while loop to iterate through the array?
     */
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
