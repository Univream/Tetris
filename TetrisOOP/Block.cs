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

        public Color color { get; protected set; }

        public Block(int size)
        {
            _size = size;
        }

    }
}
