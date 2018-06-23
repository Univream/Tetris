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
        private Form _frm;

        private  Map _map;

        private Timer timer;

        private Func<Shape>[] _shapes;

        private Label _levelLabel;
        private Random r = new Random();

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
            _map = new Map(mapWidth, mapHeight, 20, frm);
            _shapes = new Func<Shape>[]
            {
                () => { return new Shape(
                    new Block[,] { 
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) },
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) },
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) } }, _map); },
                () => { return new Shape(
                    new Block[,] { 
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) }, 
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) }, 
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) } }, _map); },
                () => { return new Shape(
                    new Block[,] {
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) },
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) },
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) } }, _map); },
                () => { return new Shape(
                    new Block[,] {
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) },
                        { new EmptyBlock(_map.BlockSize), new ShapeBlock(_map, _frm) },
                        { new ShapeBlock(_map, _frm), new ShapeBlock(_map, _frm) } }, _map); },
                () => { return new Shape(
                    new Block[,] {
                        { new ShapeBlock(_map, _frm) },
                        { new ShapeBlock(_map, _frm) },
                        { new ShapeBlock(_map, _frm) } }, _map); }
            };

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

        /// <summary>
        /// Defines the game procedure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, EventArgs e)
        {
            if (!_map.MoveShape())
            {
                level = _map.Check();
                if(_map.GameOver())
                {
                    timer.Enabled = false;
                    GameOver(this, new EventArgs());
                }
                else
                {
                    _map.AddShape(_shapes[r.Next(_shapes.Length)].Invoke());
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
            _map.AddShape(_shapes[r.Next(_shapes.Length)].Invoke());
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
        /// Moves currently active shape left
        /// </summary>
        public void ShapeLeft()
        {
            _map.ShapeLeft();
        }

        /// <summary>
        /// Moves currently active shape right
        /// </summary>
        public void ShapeRight()
        {
            _map.ShapeRight();
        }

        public void RotateLeft()
        {
            _map.RotateLeft();
            _map.RenderShape();
        }

        public void RotateRight()
        {
            _map.RotateRight();
            _map.RenderShape();
        }

        /// <summary>
        /// Gets fired when the game is over
        /// </summary>
        public event EventHandler GameOver;
        
    }
}
