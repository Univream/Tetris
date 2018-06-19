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

        private Label _levelLabel;

        private int _level = 0;

        public int level {
            get
            {
                return _level;
            }
            private set
            {
                _level = value;
                timer.Interval = 5000 / (level + 9);
                _levelLabel.Text = _level.ToString();
            }
        }
        
        /// <summary>
        /// Initializes the Tetris game
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="blockSize"></param>
        public Tetris(Form frm, int mapWidth, int mapHeight, ref Label label)
        {
            _map = new Map(mapWidth, mapHeight, 20, frm);
            _map.AddBlock();
            timer = new Timer();
            timer.Enabled = false;
            _levelLabel = label;
            level = 0;
            label.Text = level.ToString();
            timer.Tick += new EventHandler(Tick);
        }

        /// <summary>
        /// starts Tetris
        /// </summary>
        public void Start()
        {
            timer.Enabled = true;
        }

        /// <summary>
        /// Toggles between game stop and start
        /// </summary>
        public void Toggle()
        {
            timer.Enabled = !timer.Enabled;
        }
        
        private void Tick(object sender, EventArgs e)
        {
            if (!_map.MoveBlock())
            {
                level = _map.Check();
                if(_map.GameOver())
                {
                    timer.Enabled = false;
                    GameOver(this, new EventArgs());
                }
                else
                {
                    _map.AddBlock();
                }
            }
        }

        /// <summary>
        /// Drops the currently active block
        /// </summary>
        public void Drop()
        {
            timer.Enabled = false;
            _map.DropBlock();
            level = _map.Check();
            timer.Enabled = true;
        }

        /// <summary>
        /// Moves currently active block left
        /// </summary>
        public void BlockLeft()
        {
            _map.BlockLeft();
        }

        /// <summary>
        /// Moves currently active block right
        /// </summary>
        public void BlockRight()
        {
            _map.BlockRight();
        }

        /// <summary>
        /// Gets fired when the game is over
        /// </summary>
        public event EventHandler GameOver;
        
    }
}
