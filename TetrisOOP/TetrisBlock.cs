using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{

    class TetrisBlock : Block
    {
        private Panel p = new Panel();

        private Form _frm;
        
        public static Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Gray, Color.Pink };
        
        private Random r = new Random();

        /// <summary>
        /// Initialize a Tetris block on a given map
        /// </summary>
        /// <param name="map">Game map</param>
        /// <param name="frm">The form the game should be played on</param>
        public TetrisBlock(Map map, Form frm) : base(map.blockSize)
        {
            color = colors[r.Next(0, colors.Length)];
            p.BackColor = color;
            p.Size = new Size(map.blockSize, map.blockSize);
            p.Location = new Point(map.size.Width / 2, 130);
            _frm = frm;
            frm.Controls.Add(p);
        }

        public void MoveDown()
        {
            p.Top += _size;
        }

        public void MoveLeft()
        {
            p.Left -= _size;
        }
    
        public void MoveRight()
        {
            p.Left += _size;
        }

        /// <summary>
        /// Removes underlying graphical panel from the form
        /// </summary>
        public void Remove()
        {
            _frm.Controls.Remove(p);
        }
    }
}
