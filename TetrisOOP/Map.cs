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

        // Cors of shape
        public int ShapePosCol = 3;
        public int ShapePosRow = 0;

        private Block[,] _blockMap;

        /// <summary>
        /// Block dimemsion in pixel
        /// </summary>
        public int BlockSize { get; set; }

        private TetrisBlock _currentBlock;

        private Shape _shape;

        private Form _frm;

        private int _deletedBlockSets = 0;
        
        /// <summary>
        /// Initialzes a map
        /// </summary>
        /// <param name="widthMap">width in blocks</param>
        /// <param name="heightMap">height blocks</param>
        /// <param name="blockDim">height</param>
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
        }

        public void AddShape(Shape shape)
        { 
            _shape = shape;

            // define start redern point
            RenderShape();
        }

        public void RenderShape()
        {
            for (int i = 0; i <= _shape.ShapeMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= _shape.ShapeMap.GetUpperBound(1); j++)
                {
                    _shape.ShapeMap[i, j].MapPositionX = ShapePosCol + j;
                    _shape.ShapeMap[i, j].MapPositionY = ShapePosRow + i;

                    if (_shape.ShapeMap[i, j] is ShapeBlock)
                    {
                        ((ShapeBlock)_shape.ShapeMap[i, j]).AddToMap(_shape.ShapeMap[i, j].MapPositionX, _shape.ShapeMap[i, j].MapPositionY);
                    }
                }
            }
        }

        public bool CheckIfRenderable(Block[,] ShapeMap)
        {
            for (int i = 0; i <= ShapeMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= ShapeMap.GetUpperBound(1); j++)
                {
                    if (ShapeMap[i, j] is ShapeBlock)
                    {
                        if(!(_blockMap[ShapePosRow + i, ShapePosCol + j] is EmptyBlock))
                        {
                            return false;
                        }
                    }
                }
            }
            _shape.ShapeMap = ShapeMap;
            return true;
        }

        /// <summary>
        /// Moves the current block one step
        /// </summary>
        /// <returns></returns>
        public bool MoveBlock()
        {
            if( _blockMap[ShapePosRow + 1, ShapePosCol] is EmptyBlock)
            {
                _currentBlock.MoveDown();
                ShapePosRow++;
                return true;
            }
            _blockMap[ShapePosRow, ShapePosCol] = _currentBlock;
            return false;
        }

        public bool MoveShape()
        {
            if (_shape != null)
            {
                ShapePosRow++;
                if (!CheckIfRenderable(_shape.ShapeMap))
                {
                    for (int k = _shape.ShapeMap.GetUpperBound(0); k >= 0; k--)
                    {
                        for (int l = 0; l <= _shape.ShapeMap.GetUpperBound(1); l++)
                        {
                            if (_shape.ShapeMap[k, l] is ShapeBlock)
                            {
                                _blockMap[_shape.ShapeMap[k, l].MapPositionY, _shape.ShapeMap[k, l].MapPositionX] = (ShapeBlock)_shape.ShapeMap[k, l];
                            }   
                        }
                    }

                    // Reset startpoint
                    ShapePosCol = 3;
                    ShapePosRow = 0;
                    return false;
                }
                
                RenderShape();
                return true;
            }
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
                for (int col = 1; col < _blockMap.GetUpperBound(1); col++)
                    CheckVertical(row, col);

            for (int row = 0; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 1; col < _blockMap.GetUpperBound(1) - 2; col++)
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
            // one block abbove
            var block1 = _blockMap[r - 1, c];
            // two block abbove
            var block2 = _blockMap[r - 2, c];
            
            if (block is ShapeBlock && block1 is ShapeBlock && block2 is ShapeBlock)
            {
                if(block.color == block1.color && block.color == block2.color)
                {
                    ((ShapeBlock)block).Remove();
                    ((ShapeBlock)block1).Remove();
                    ((ShapeBlock)block2).Remove();
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

            if (block is ShapeBlock && block1 is ShapeBlock && block2 is ShapeBlock)
            {
                if (block.color == block1.color && block.color == block2.color)
                {
                    ((ShapeBlock)block).Remove();
                    ((ShapeBlock)block1).Remove();
                    ((ShapeBlock)block2).Remove();
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
        private void PullBlockDown(int r, int c)
        {
            try
            {
                while (_blockMap[--r, c] is ShapeBlock)
                {
                    ((ShapeBlock)_blockMap[r, c]).MoveDown();
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
            if(_blockMap[ShapePosRow, ShapePosCol - 1] is EmptyBlock)
            {
                _currentBlock.MoveLeft();
                _blockMap[ShapePosRow, ShapePosCol - 1] = _blockMap[ShapePosRow, ShapePosCol];
                MakeEmpty(ShapePosRow, ShapePosCol);
                ShapePosCol--;
            }
        }

        /// <summary>
        /// Moves the current block right
        /// </summary>
        public void BlockRight()
        {
            if(_blockMap[ShapePosRow, ShapePosCol + 1] is EmptyBlock)
            {
                _currentBlock.MoveRight();
                _blockMap[ShapePosRow, ShapePosCol + 1] = _blockMap[ShapePosRow, ShapePosCol];
                MakeEmpty(ShapePosRow, ShapePosCol);
                ShapePosCol++;
            }
        }

        public void ShapeRight()
        {
            for (int i = _shape.ShapeMap.GetUpperBound(0); i >= 0; i--)
            {
                if (!(_blockMap[_shape.ShapeMap[i, _shape.ShapeMap.GetUpperBound(1)].MapPositionY, _shape.ShapeMap[i, _shape.ShapeMap.GetUpperBound(1)].MapPositionX + 1] is EmptyBlock))
                {
                    return;
                }
            }

            ShapePosCol++;
            RenderShape();
        }

        public void ShapeLeft()
        {
            for (int i = _shape.ShapeMap.GetUpperBound(0); i >= 0; i--)
            {
                if (!(_blockMap[_shape.ShapeMap[i, 0].MapPositionY, _shape.ShapeMap[i, 0].MapPositionX - 1] is EmptyBlock))
                {
                    return;
                }
            }

            ShapePosCol--;
            RenderShape();
        }

        public void RotateLeft()
        {
            _shape.RotateLeft();
        }

        public void RotateRight()
        {
            _shape.RotateRight();
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
            if(_blockMap[0, 3] is EmptyBlock)
            {
                return false;
            }

            return true; 
        }
    }
}