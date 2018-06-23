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

        public Color color;

        private Map _map;

        private Random r = new Random();

        public static Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Gray, Color.Pink };

        public Shape(Block[,] blockMap, Map map)
        {
            color = colors[r.Next(0, colors.Length)];
            ShapeMap = blockMap;
            _map = map;
            foreach(Block b in ShapeMap)
            {
                if(b is ShapeBlock)
                {
                    ((ShapeBlock)b).p.BackColor = color;
                    b.color = color;
                }
            }
        }

        public void RotateLeft()
        {
            var newBlockMap = new Block[ShapeMap.GetUpperBound(1) + 1, ShapeMap.GetUpperBound(0) + 1];
            for(int r = 0; r <= newBlockMap.GetUpperBound(0); r++)
            {
                for(int c = 0; c <= newBlockMap.GetUpperBound(1); c++)
                {
                    newBlockMap[r, c] = ShapeMap[Mirror(c, newBlockMap.GetUpperBound(1)), r];                    
                }
            }
            if(_map.CheckIfRenderable(newBlockMap))
            {
                _map.RenderShape();
            }
        }

        public void RotateRight()
        {
            var newBlockMap = new Block[ShapeMap.GetUpperBound(1) + 1, ShapeMap.GetUpperBound(0) + 1];
            for (int r = 0; r <= newBlockMap.GetUpperBound(0); r++)
            {
                for (int c = 0; c <= newBlockMap.GetUpperBound(1); c++)
                {
                    newBlockMap[r, Mirror(c, newBlockMap.GetUpperBound(1))] = ShapeMap[c, r];
                }
            }
            if (_map.CheckIfRenderable(newBlockMap))
            {
                _map.RenderShape();
            }
        }

        public static int Mirror(int value, int maxValue)
        {
            var max = maxValue / 2.0;
            return (int)((max - (double)value) + max);
        }
    }
}
