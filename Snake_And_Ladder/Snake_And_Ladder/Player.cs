using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_And_Ladder
{
    class Player
    {
        
        public string Name { get; set; }
        public int Position { get; set; }

        public Player()
        {           
            Name = null;
            Position = 1;
        }

        public void SetName(String Name)
        {
            this.Name = Name;
        }

        public void SetPosition(int Position)
        {
            if (!( (this.Position + Position) > 100))
            {
                this.Position += Position;
            }          
        }


    }
}
