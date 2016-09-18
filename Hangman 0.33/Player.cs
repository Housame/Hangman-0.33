using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_0._33
{
    class Player
    {
        public string name;

        public Player (string name)
        {
            Name = name;
        }
        

        public string Name
        {
            get
            { return name; }
            set
            { name = value; }
        }

    }
}
