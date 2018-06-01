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
        public Size size { get; set; }

        private Block[,] _blockMap;

        private int _bRow;

        private int _bColumn;

        public int blockSize { get; set; }

        private TetrisBlock _currentBlock;

        private Form _frm;


        /// <summary>
        /// Initialzes a map
        /// </summary>
        /// <param name="widthMap">width in blocks</param>
        /// <param name="heightMap">height blocks</param>
        /// <param name="blockDim">height </param>
        /// <param name="frm"></param>
        public Map(int widthMap, int heightMap, int blockDim, Form frm)
        {
            size = new Size(widthMap * blockDim, heightMap * blockDim);
            this.blockSize = blockDim;
            _blockMap = new Block[heightMap + 1, widthMap + 2];
            _frm = frm;

            // intialize the map
            for(int r = 0; r < _blockMap.GetUpperBound(0); r++)
            {
                _blockMap[r, 0] = new EdgeBlock(blockSize);
                for(int c = 1; c < _blockMap.GetUpperBound(1); c++)
                {
                    MakeEmpty(r, c);
                }

                _blockMap[r, widthMap + 1] = new EdgeBlock(blockSize);
            }

            for(int c = 0; c <= _blockMap.GetUpperBound(1); c++)
            {
                _blockMap[heightMap, c] = new EdgeBlock(blockSize);
            }
        }

        public void AddBlock()
        {
            _currentBlock = new TetrisBlock(this, _frm);
            _blockMap[0, 5] = _currentBlock;
            _bRow = 0;
            _bColumn = 5;
        }

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

        private void MakeEmpty(int r, int c)
        {
            _blockMap[r, c] = new EmptyBlock(blockSize); 
        }

        public void Check()
        {
            for (int row = 2; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 0; col < _blockMap.GetUpperBound(1); col++)
                    CheckVertical(row, col);

            for (int row = 0; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 0; col < _blockMap.GetUpperBound(1) - 2; col++)
                    CheckHorizontal(row, col);
        }

        public void CheckVertical(int r, int c)
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
                }
            }
        }

        public void CheckHorizontal(int r, int c)
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

                    Check();
                }
            }
        }

        public void PullBlockDown(int r, int c)
        {
            while (_blockMap[--r, c] is TetrisBlock)
            {
                ((TetrisBlock)_blockMap[r, c]).MoveDown();
                _blockMap[r + 1, c] = _blockMap[r, c];
                MakeEmpty(r, c);
            }
        }

        public void DropBlock()
        {
            while(MoveBlock())
            {}
            Check();
            AddBlock();
        }

        public bool BlockLeft()
        {
            if(_blockMap[_bRow, _bColumn - 1] is EmptyBlock)
            {
                _currentBlock.MoveLeft();
                _blockMap[_bRow, _bColumn - 1] = _blockMap[_bRow, _bColumn];
                MakeEmpty(_bRow, _bColumn);
                _bColumn--;
                return true;
            }
            return false;
        }

        public bool BlockRight()
        {
            if(_blockMap[_bRow, _bColumn + 1] is EmptyBlock)
            {
                _currentBlock.MoveRight();
                _blockMap[_bRow, _bColumn + 1] = _blockMap[_bRow, _bColumn];
                MakeEmpty(_bRow, _bColumn);
                _bColumn++;
                return true;
            }
            return false;
        }

    }
}
