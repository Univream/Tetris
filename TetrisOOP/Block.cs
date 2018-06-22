using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{
    abstract class Block
    {
        protected int _size;

        public int MapPositionX;
        public int MapPositionY;

        public Color color { get; set; }

        public Block(int size)
        {
            _size = size;
        }

    }
}
