using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_And_Ladder
{
    class Dice
    {
        private Dice()
        { }
        public static int RollDice()
        {
            return new Random().Next(1,7);
        }
       
    }
}
