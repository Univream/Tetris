using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{
    /// <summary>
    /// Represents the game
    /// </summary>
    class Tetris
    {

        private Map _map;

        private Timer timer;

        /// <summary>
        /// Initializes the Tetris game
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="blockSize"></param>
        public Tetris(Form frm, int mapWidth, int mapHeight)
        {
            _map = new Map(mapWidth, mapHeight, 20, frm);
            _map.AddBlock();
            timer = new Timer();
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Tick);
        }

        public void Start()
        {
            timer.Enabled = true;
        }

        public void Toggle()
        {
            timer.Enabled = !timer.Enabled;
        }
        
        private void Tick(object sender, EventArgs e)
        {
            if (!_map.MoveBlock())
            {
                _map.Check();
                _map.AddBlock();                
            }
        }

        public void Drop()
        {
            timer.Enabled = false;
            _map.DropBlock();
            timer.Enabled = true;
        }

        public bool BlockLeft()
        {
            return _map.BlockLeft();
        }

        public bool BlockRight()
        {
            return _map.BlockRight();
        }

    }
}
