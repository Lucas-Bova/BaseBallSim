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

        public Player(int positionNumber)
        {
            Position = Positions[positionNumber];
            setChance();
            setPower();
        }

        private void setChance()
        {
            Random rand = new Random();

            for (int i = 0; i < Positions.Length; ++i)
            {
                if (Positions[i].Equals(Position))
                {
                    HitChance = rand.Next(i * 4 + 40, i * 4 + 65);
                }                    
            }
        }

        private void setPower()
        {
            Random rand = new Random(hitChance);

            for (int i = 0; i < Positions.Length; ++i)
            {
                if (Positions[i].Equals(Position))
                {
                    HitPower = rand.Next(i * 4 + 40, i * 4 + 65);
                }
            }
        }

        public int swing(int pitch, Random rand)
        {
            var aHit = false;
            var attempt = rand.Next(1, HitChance);

            //check if it is a foul
            if (rand.Next(1, pitch) < 15)
            {
                //foul
                return 5;
            }
            if (attempt >= pitch)
            {
                aHit = true;
            }

            if (aHit)
            {
                var pow = rand.Next(1, HitPower);
                if (pow >= 75)
                {
                    //a homerun
                    return 4;
                }
                else if (pow >= 65)
                {
                    //triple
                    return 3;
                }
                else if (pow >= 60)
                {
                    //double
                    return 2;
                }
                else
                {
                    //a hit
                    return 1;
                }
            }
            else
            {
                //a strike
                return 0;
            }
        }

        public override string ToString()
        {
            return $"{Position} - Hit Chance: {HitChance} Hit Power: {HitPower}";
        }
    }


    class Pitcher
    {
        private int pitchAbility;

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

        public Pitcher()
        {
            setPitch();
        }

        private void setPitch()
        {
            Random rand = new Random();
            PitchAbility = rand.Next(60, 100);
        }

        public int Pitch(Random rand)
        {
            return rand.Next(1, PitchAbility);
        }


        public override string ToString()
        {
            return $"Pitcher Ability: {PitchAbility}";
        }
    }
}
