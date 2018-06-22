using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisOOP
{
    class Shape
    {
        public Block[,] ShapeMap;

        public Color _color;

        private Random r = new Random();

        public static Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Gray, Color.Pink };

        public Shape(Block[,] blockMap)
        {
            _color = colors[r.Next(0, colors.Length)];
            ShapeMap = blockMap;
            foreach(Block b in ShapeMap)
            {
                if(b is ShapeBlock)
                {
                    ((ShapeBlock)b).p.BackColor = _color;
                    b.color = _color;
                }
            }
        }

        public void RotateBlockMap()
        {
            var newblockMap = new Block[ShapeMap.GetUpperBound(1), ShapeMap.GetUpperBound(0)];
            for(int i = 0; i < newblockMap.GetUpperBound(0); i++)
            {
                for(int j = 0; j < newblockMap.GetUpperBound(1); j++)
                {
                    newblockMap[i, j] = ShapeMap[j, i];
                }
            }
            ShapeMap = newblockMap;
        }
    }
}
