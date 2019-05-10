using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BaseBallSim
{
    class AtBat : Inning
    {
        private int strikes = 0;
        private int balls = 0;
        private Player[] players;

        public int Strikes
        {
            get
            {
                return strikes;
            }
            set
            {
                //check if the runner strikedout
                //if the runner struckout incremente outs and reset at bat
                strikes = value;
                if (strikes >= 3)
                {
                    Outs++;
                    ResetAtbat();
                }
            }
        }
        public int Balls
        {
            get
            {
                return balls;
            }
            set
            {
                //check if the runner walked
                //if the runner walked advance runners and reset at bat
                balls = value;
                if (balls >= 4)
                {
                    trackBase();
                    ResetAtbat();
                }
            }
        }


        public void AtBatIN(Player[] players, Pitcher pitcher)
        {
            Random rand = new Random();

            //print lineup
            printLineUp(players);

            //throw pitches until a hit happens
            while (Outs < 3)
            {
                //print scoreboard information
                PrintOutPut($"Score: {Score}\nOuts: {Outs}\nBalls: {Balls} Strikes: {Strikes}\n{WhoIsOn()}\nBatter: {players[Index].ToString()}");

                //get a random number representing pitch power
                //use the getChances method to return the chances for a strike and a ball based on the pitch power
                double ballChance;
                double strikeChance;
                var pitch = pitcher.Pitch(rand);
                getChances(pitch, out ballChance, out strikeChance);

                WriteLine($"Pitch Power = {pitch}\nChance for ball = {ballChance:P}\nChance for Strike = {strikeChance:P}");


                //user interaction
                Write("Swing? (1 = yes/2 = no)");
                var swing = char.Parse(ReadLine());

                //if the user decided to swing
                if (swing == '1')
                {
                    //result of the swing represented by an int variable that will be passed into the swingOutcome Method to determine which actions should execute based on the result
                    var outcome = players[Index].swing(pitch, rand);
                    SwingOutcome(outcome);
                }
                //the user did not decide to swing
                else
                {
                    //check if the pitch was a ball or a strike
                    //might need to look at this logic in more depth soon
                    if (rand.Next(1, pitch) <= 15)
                    {
                        PrintOutPut("Ball");
                        Balls++;
                    }
                    else
                    {
                        PrintOutPut("Strike");
                        Strikes++;
                    }
                }
            }
        }

        //simple method that will only add a strike to the total number of strikes if there are fewer than two strikes
        //this is so a batter will not strike out on a foul
        private void foul()
        {
            if (Strikes < 2)
            {
                //record the foul
                Strikes++;
            }
        }

        //logic for a homerun
        //if a runner is on base score is incremented for each runner
        //score is also incremented for the batter aswell
        private void Homerun()
        {
            for (int i = 0; i < onBase.Length; ++i)
            {
                if (onBase[i])
                {
                    Score++;
                    onBase[i] = false;
                }
            }
            Score++;
        }

        //determines the outcome of the swing based on the outcome variable
        //to add a new outcome just add a new case
        private void SwingOutcome(int outcome)
        {
            //outcome of the swing
            switch (outcome)
            {
                case 0:
                    //strike
                    PrintOutPut("STRIKEEEE!!!!");
                    Strikes++;
                    break;
                case 1:
                    //single base hit
                    PrintOutPut("It's a hit!!!");
                    trackBase(1);
                    ResetAtbat();
                    break;
                case 2:
                    //double
                    PrintOutPut("It's a double!!!");
                    trackBase(2);
                    ResetAtbat();
                    break;
                case 3:
                    //triple
                    PrintOutPut("It's a triple!!!");
                    trackBase(3);
                    ResetAtbat();
                    break;
                case 4:
                    //Home Run
                    PrintOutPut("ITS GOOOONNNNEEEE!!!!!!!!");
                    Homerun();
                    ResetAtbat();
                    break;
                case 5:
                    //foul
                    foul();
                    PrintOutPut("Foul!!!");
                    break;
            }
        }

        //resets the at bat so strikes and balls are set to 0
        //this should be done after a hit, walk, strikeout, or homerun
        private void ResetAtbat()
        {
            Balls = 0;
            Strikes = 0;
            ++Index;
        }

        //gets the precent chance for the pitch to be a ball or a strike returned as double values
        //if the pitch power is less than 15 the ball chance will be %100
        private void getChances(int pitch, out double ballChance, out double strikeChance)
        {
            if (pitch < 15.0)
            {
                ballChance = 1.0;
            }
            else
            {
                ballChance = 15.0 / pitch;
            }
            strikeChance = 1 - ballChance;
        }
    }
}
