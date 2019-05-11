using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBallSim
{
    //this is the class all postitions shall be derieved from? or let postition be a property in player?
    class Player
    {
        //array of position names, maybe change this to an enum??
        private readonly string[] Positions =
        {
            "C ",
            "1B",
            "2B",
            "SS",
            "3B",
            "LF",
            "CF",
            "RF",
            "DH"
        };

        private int hitChance;
        private int hitPower;
        private string position;

        public string Position { get; set; }
        public int HitChance
        {
            get
            {
                return hitChance;
            }
            private set
            {
                if (value >= 40 && value <= 100)
                {
                    hitChance = value;
                }
                else
                {
                    throw new Exception("Invalid entry"); //refine later
                }
            }
        }
        public int HitPower
        {
            get
            {
                return hitPower;
            }
            private set
            {
                if (value >= 40 && value <= 100)
                {
                    hitPower = value;
                }
                else
                {
                    throw new Exception("Invalid entry"); //refine later
                }
            }
        }

        //constructor that takes a single int parameter for the position number and calculates the hitchance and hitpower
        public Player(int positionNumber)
        {
            Position = Positions[positionNumber];
            setChance();
            setPower();
        }

        //method to set hit chance
        //gets the index of the array element that correlates to the Position property
        //uses the index to return a random number based of off a players position
        //this creates a construct where players of a certain position will be nautrally more skilled than others
        private void setChance()
        {
            Random rand = new Random();
            int index = Array.IndexOf(Positions, Position);
            HitChance = rand.Next(index * 4 + 40, index * 4 + 65);
        }

        //method to set hit power
        //gets the index of the array element that correlates to the Position property
        //uses the index to return a random number based of off a players position
        //this creates a construct where players of a certain position will be nautrally more skilled than others
        private void setPower()
        {
            Random rand = new Random(hitChance);
            int index = Array.IndexOf(Positions, Position);
            HitPower = rand.Next(index * 4 + 40, index * 4 + 65);
        }

        //method containing the logic for a swing based on an incoming pitch
        //should recieve an argument for a Random object so it is seeded correctly
        //first checks if the the result of the swing is a foul
        //if the pitch was not a foul the attempt is tested to see if it is a successful hit
        //if the swing is a successful hit, hit power is tested to see what kind of hit it is
        //if the attempt is not a successful hit the result is a strike
        public int swing(int pitch, Random rand)
        {
            //check if it is a foul
            if (rand.Next(1, pitch) < 15)
            {
                //foul
                return 5;
            }

            var attempt = rand.Next(1, HitChance);
            if (attempt >= pitch)
            {
                //a hit
                var pow = rand.Next(1, HitPower);
                if (pow < 60)
                {
                    //single
                    return 1;
                }
                else if (pow < 70)
                {
                    //double
                    return 2;
                }
                else if (pow < 75)
                {
                    //triple continue
                    return 3;
                }
                else
                {
                    //homerun
                    return 4;
                }
            }
            else
            {
                //a strike
                return 0;
            }
        }

        //override to string method to return the player position, hitchance and hitpower
        public override string ToString()
        {
            return $"{Position} - Hit Chance: {HitChance} Hit Power: {HitPower}";
        }
    }


    class Pitcher
    {
        private int pitchAbility;

        //porperty that sets a pitchers overall ability
        public int PitchAbility
        {
            get
            {
                return pitchAbility;
            }
            private set
            {
                if (value >= 60 && value <= 100)
                {
                    pitchAbility = value;
                }
                else
                {
                    throw new Exception("Invalid entry"); //refine later
                }
            }
        }

        //constructor that calls setPitch method to set pitch ability each time a new Pitcher object is created
        public Pitcher()
        {
            setPitch();
        }

        //sets the ability of the pitcher with a random number between 60 and 100
        private void setPitch()
        {
            Random rand = new Random();
            PitchAbility = rand.Next(60, 100);
        }

        //public method that returns an int value for pitch power
        //method takes a Random object to insure it is seeded correctly
        public int Pitch(Random rand)
        {
            return rand.Next(1, PitchAbility);
        }

        //override to string method to return pitcher ability
        public override string ToString()
        {
            return $"Pitcher Ability: {PitchAbility}";
        }
    }
}
