using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Objects
{
    class Wall:BaseObject
    {
        private int wallNumber;

        public int WallNumber
        {
            get
            {
                return wallNumber;
            }

            set
            {
                wallNumber = value;
            }
        }
    }
}
