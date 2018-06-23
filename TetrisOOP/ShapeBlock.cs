using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{
    class ShapeBlock : Block
    {
        private Form _frm;

        private Map _map;

        public Panel p = new Panel();

        public ShapeBlock(Map map, Form frm) : base(map.BlockSize)
        {
            _map = map;
            p.BackColor = color;
            p.Size = new Size(map.BlockSize, map.BlockSize);
            _frm = frm;
        }

        public void AddToMap(int colBlocks, int rowBlocks)
        {
            p.Location = new Point((_map.BlockSize * colBlocks) - 10, 130 + (_map.BlockSize * rowBlocks));
            _frm.Controls.Add(p);
        }

        public void MoveDown()
        {
            p.Top += _size;
        }

        public void MoveDown(int blocks)
        {
            p.Top += _size * blocks;
        }

        public void MoveLeft(int blocks)
        {
            p.Left -= _size * blocks;
        }

        public void MoveRight(int blocks)
        {
            p.Left += _size * blocks;
        }

        public void MoveLeft()
        {
            p.Left -= _size;
        }

        public void MoveRight()
        {
            p.Left += _size;
        }

        public void Remove()
        {
            _frm.Controls.Remove(p);
        }
    }
}
