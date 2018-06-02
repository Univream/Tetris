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

        private Form _frm;

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
            _frm = frm;
            _map = new Map(mapWidth, mapHeight, 20, _frm);
            _map.AddBlock();
            timer = new Timer();
            timer.Enabled = false;
            _levelLabel = label;
            level = 0;
            label.Text = level.ToString();
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
                level = _map.Check();
                if(_map.gameOver())
                {
                    timer.Enabled = false;
                    // Makes sure that the first block on the map is empty
                    
                    
                    if (MessageBox.Show("Game Over - Try again", "You lost", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        _map.Clear();
                        level = 0;
                        _map.AddBlock();
                        timer.Enabled = true;
                    }
                    else
                    {
                        _frm.Close();
                    }
                }
                else
                {
                    _map.AddBlock();
                }
            }
        }

        public void Drop()
        {
            timer.Enabled = false;
            _map.DropBlock();
            level = _map.Check();
            timer.Enabled = true;
        }

        public void BlockLeft()
        {
            _map.BlockLeft();
        }

        public void BlockRight()
        {
            _map.BlockRight();
        }
        
    }
}
