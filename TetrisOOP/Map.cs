using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{
    /// <summary>
    /// Represents the Tetris area
    /// </summary>
    class Map
    {
        /// <summary>
        /// Size in pixel
        /// </summary>
        public Size Size { get; set; }

        private Block[,] _blockMap;

        private int _bRow;

        private int _bColumn;

        /// <summary>
        /// Block dimemsion in pixel
        /// </summary>
        public int BlockSize { get; set; }

        private TetrisBlock _currentBlock;

        private Form _frm;

        private int _deletedBlockSets = 0;
        
        /// <summary>
        /// Initialzes a map
        /// </summary>
        /// <param name="widthMap">width in blocks</param>
        /// <param name="heightMap">height blocks</param>
        /// <param name="blockDim">height </param>
        /// <param name="frm"></param>
        public Map(int widthMap, int heightMap, int block, Form frm)
        {
            Size = new Size(widthMap * block, heightMap * block);
            this.BlockSize = block;
            _blockMap = new Block[heightMap + 1, widthMap + 2];
            _frm = frm;

            // intialize the map
            for(int r = 0; r < _blockMap.GetUpperBound(0); r++)
            {
                _blockMap[r, 0] = new EdgeBlock(BlockSize);
                for(int c = 1; c < _blockMap.GetUpperBound(1); c++)
                {
                    MakeEmpty(r, c);
                }

                _blockMap[r, widthMap + 1] = new EdgeBlock(BlockSize);
            }

            for(int c = 0; c <= _blockMap.GetUpperBound(1); c++)
            {
                _blockMap[heightMap, c] = new EdgeBlock(BlockSize);
            }
        }

        /// <summary>
        /// Adds a TetrisBlock to the map
        /// </summary>
        public void AddBlock()
        {
            _currentBlock = new TetrisBlock(this, _frm);
            _bRow = 0;
            _bColumn = 5;
        }

        /// <summary>
        /// Moves the current block one step
        /// </summary>
        /// <returns></returns>
        public bool MoveBlock()
        {
            if( _blockMap[_bRow + 1, _bColumn] is EmptyBlock)
            {
                _currentBlock.MoveDown();
                _bRow++;
                return true;
            }
            _blockMap[_bRow, _bColumn] = _currentBlock;
            return false;
            
        }

        /// <summary>
        /// Turns the specified TetrisBlock into an EmptyBlock
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void MakeEmpty(int r, int c)
        {
            _blockMap[r, c] = new EmptyBlock(BlockSize); 
        }

        /// <summary>
        /// Checks for 3 blocks which have the same color and are next to each other
        /// </summary>
        /// <returns></returns>
        public int Check()
        {
            for (int row = 2; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 0; col < _blockMap.GetUpperBound(1); col++)
                    CheckVertical(row, col);

            for (int row = 0; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 0; col < _blockMap.GetUpperBound(1) - 2; col++)
                    CheckHorizontal(row, col);

            return _deletedBlockSets;
        }

        /// <summary>
        /// Checks for vertical match
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void CheckVertical(int r, int c)
        {
            var block = _blockMap[r, c];
            // block abbove
            var block1 = _blockMap[r - 1, c];
            // block abbove
            var block2 = _blockMap[r - 2, c];

            if (block is TetrisBlock && block1 is TetrisBlock && block2 is TetrisBlock)
            {
                if(block.color == block1.color && block.color == block2.color)
                {
                    ((TetrisBlock)block).Remove();
                    ((TetrisBlock)block1).Remove();
                    ((TetrisBlock)block2).Remove();
                    MakeEmpty(r, c);
                    MakeEmpty(r - 1, c);
                    MakeEmpty(r - 2, c);
                    _deletedBlockSets++;
                }
            }
        }

        /// <summary>
        /// Checks the for horizontal match
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void CheckHorizontal(int r, int c)
        {
            var block = _blockMap[r, c];
            // block abbove
            var block1 = _blockMap[r, c + 1];
            // block abbove
            var block2 = _blockMap[r, c + 2];

            if (block is TetrisBlock && block1 is TetrisBlock && block2 is TetrisBlock)
            {
                if (block.color == block1.color && block.color == block2.color)
                {
                    ((TetrisBlock)block).Remove();
                    ((TetrisBlock)block1).Remove();
                    ((TetrisBlock)block2).Remove();
                    MakeEmpty(r, c);
                    MakeEmpty(r, c + 1);
                    MakeEmpty(r, c + 2);
                    PullBlockDown(r, c);
                    PullBlockDown(r, c + 1);
                    PullBlockDown(r, c + 2);
                    _deletedBlockSets++;
                    Check();
                }
            }
        }

        /// <summary>
        /// Moves the block above the specified one down
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public void PullBlockDown(int r, int c)
        {
            try
            {
                while (_blockMap[--r, c] is TetrisBlock)
                {
                    ((TetrisBlock)_blockMap[r, c]).MoveDown();
                    _blockMap[r + 1, c] = _blockMap[r, c];
                    MakeEmpty(r, c);
                }
            }
            catch(IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Moves the current block as far down as it can
        /// </summary>
        public void DropBlock()
        {
            while(MoveBlock())
            {}
            AddBlock();
        }

        /// <summary>
        /// Moves the current block left
        /// </summary>
        public void BlockLeft()
        {
            if(_blockMap[_bRow, _bColumn - 1] is EmptyBlock)
            {
                _currentBlock.MoveLeft();
                _blockMap[_bRow, _bColumn - 1] = _blockMap[_bRow, _bColumn];
                MakeEmpty(_bRow, _bColumn);
                _bColumn--;
            }
        }

        /// <summary>
        /// Moves the current block right
        /// </summary>
        public void BlockRight()
        {
            if(_blockMap[_bRow, _bColumn + 1] is EmptyBlock)
            {
                _currentBlock.MoveRight();
                _blockMap[_bRow, _bColumn + 1] = _blockMap[_bRow, _bColumn];
                MakeEmpty(_bRow, _bColumn);
                _bColumn++;
            }
        }

        /// <summary>
        /// Clears the map
        /// </summary>
        public void Clear()
        {
            for (int row = 0; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 0; col < _blockMap.GetUpperBound(1); col++)
                {
                    if (_blockMap[row, col] is TetrisBlock)
                    {
                        ((TetrisBlock)_blockMap[row, col]).Remove();
                        MakeEmpty(row, col);
                    }
                    _deletedBlockSets = 0;
                }
        }

        /// <summary>
        /// Checks whether the game is over
        /// </summary>
        /// <returns>True if game is over</returns>
        public bool GameOver()
        {
            if(_blockMap[0, 5] is EmptyBlock)
            {
                return false;
            }

            return true; 
        }
    }
}
