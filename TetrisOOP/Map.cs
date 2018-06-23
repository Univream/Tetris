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

        /// <summary>
        /// Column of start point of the current shape (left top corner of shape)
        /// </summary>
        public int ShapeCol = 3;

        /// <summary>
        /// Row of start point of the current shape (left top corner of shape)
        /// </summary>
        public int ShapeRow = 0;

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

        /// <summary>
        /// Adds a given shape to the Map
        /// </summary>
        /// <param name="shape">Shape to add to the map</param>
        public void AddShape(Shape shape)
        { 
            _shape = shape;
            RenderShape();
        }

        /// <summary>
        /// Draws the shape with its blocks on the map
        /// </summary>
        public void RenderShape()
        {
            for (int i = 0; i <= _shape.ShapeMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= _shape.ShapeMap.GetUpperBound(1); j++)
                {
                    _shape.ShapeMap[i, j].MapPositionX = ShapeCol + j;
                    _shape.ShapeMap[i, j].MapPositionY = ShapeRow + i;

                    if (_shape.ShapeMap[i, j] is ShapeBlock)
                    {
                        ((ShapeBlock)_shape.ShapeMap[i, j]).AddToMap(_shape.ShapeMap[i, j].MapPositionX, _shape.ShapeMap[i, j].MapPositionY);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the shape on its position on the map is renderable
        /// </summary>
        /// <param name="ShapeMap">Shape to check</param>
        /// <returns>True if renderable</returns>
        public bool CheckIfRenderable(Block[,] ShapeMap)
        {
            for (int i = 0; i <= ShapeMap.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= ShapeMap.GetUpperBound(1); j++)
                {
                    if (ShapeMap[i, j] is ShapeBlock)
                    {
                        if(!(_blockMap[ShapeRow + i, ShapeCol + j] is EmptyBlock))
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
            if( _blockMap[ShapeRow + 1, ShapeCol] is EmptyBlock)
            {
                _currentBlock.MoveDown();
                ShapeRow++;
                return true;
            }
            _blockMap[ShapeRow, ShapeCol] = _currentBlock;
            return false;
        }

        /// <summary>
        /// Moves the Shape one step on the map
        /// </summary>
        /// <returns>True if successfull</returns>
        public bool MoveShape()
        {
            if (_shape != null)
            {
                ShapeRow++;
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
                    ShapeCol = 3;
                    ShapeRow = 0;
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
        /// Checks for 4 blocks which have the same color and are next to each other
        /// </summary>
        /// <returns>The number of matches</returns>
        public int Check()
        {
            for (int row = 3; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 1; col < _blockMap.GetUpperBound(1); col++)
                    CheckVertical(row, col);

            for (int row = 0; row < _blockMap.GetUpperBound(0); row++)
                for (int col = 1; col < _blockMap.GetUpperBound(1) - 3; col++)
                    CheckHorizontal(row, col);

            for (int row = 0; row < _blockMap.GetUpperBound(0) - 1; row++)
                for (int col = 1; col < _blockMap.GetUpperBound(1) - 1; col++)
                    CheckLooseBlock(row, col);

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
            // three blocks above
            var block3 = _blockMap[r - 3, c];
            
            if (block is ShapeBlock && block1 is ShapeBlock && block2 is ShapeBlock && block3 is ShapeBlock)
            {
                if(block.color == block1.color && block.color == block2.color && block.color == block3.color)
                {
                    ((ShapeBlock)block).Remove();
                    ((ShapeBlock)block1).Remove();
                    ((ShapeBlock)block2).Remove();
                    ((ShapeBlock)block3).Remove();
                    MakeEmpty(r, c);
                    MakeEmpty(r - 1, c);
                    MakeEmpty(r - 2, c);
                    MakeEmpty(r - 3, c);
                    _deletedBlockSets++;
                }
            }
        }

        /// <summary>
        /// Checks for loose Blocks on the map
        /// </summary>
        /// <param name="r">Row of Block</param>
        /// <param name="c">Column of Blocks</param>
        private void CheckLooseBlock(int r, int c)
        {
            var block = _blockMap[r, c];
            if(block is ShapeBlock)
            {
                var blockleft = _blockMap[r, c - 1];
                var blockright = _blockMap[r, c + 1];
                var blockbelow = _blockMap[r + 1, c];
                while(blockright is EmptyBlock && blockleft is EmptyBlock && blockbelow is EmptyBlock)
                 {
                    ((ShapeBlock)block).MoveDown();
                    _blockMap[r + 1, c] = _blockMap[r, c];
                    MakeEmpty(r, c);
                    PullBlockDown(r, c);
                    blockleft = _blockMap[r, c - 1];
                    blockright = _blockMap[r, c + 1];
                    blockbelow = _blockMap[r + 1, c];
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
            // one block right
            var block1 = _blockMap[r, c + 1];
            // two blocks right
            var block2 = _blockMap[r, c + 2];
            // three blocks right
            var block3 = _blockMap[r, c + 3];

            if (block is ShapeBlock && block1 is ShapeBlock && block2 is ShapeBlock && block3 is ShapeBlock)
            {
                if (block.color == block1.color && block.color == block2.color)
                {
                    ((ShapeBlock)block).Remove();
                    ((ShapeBlock)block1).Remove();
                    ((ShapeBlock)block2).Remove();
                    ((ShapeBlock)block3).Remove();
                    MakeEmpty(r, c);
                    MakeEmpty(r, c + 1);
                    MakeEmpty(r, c + 2);
                    MakeEmpty(r, c + 3);
                    PullBlockDown(r, c);
                    PullBlockDown(r, c + 1);
                    PullBlockDown(r, c + 2);
                    PullBlockDown(r, c + 3);
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
            while (MoveShape()) ;
        }

        /// <summary>
        /// Moves the current block left
        /// </summary>
        public void BlockLeft()
        {
            if(_blockMap[ShapeRow, ShapeCol - 1] is EmptyBlock)
            {
                _currentBlock.MoveLeft();
                _blockMap[ShapeRow, ShapeCol - 1] = _blockMap[ShapeRow, ShapeCol];
                MakeEmpty(ShapeRow, ShapeCol);
                ShapeCol--;
            }
        }

        /// <summary>
        /// Moves the current block right
        /// </summary>
        public void BlockRight()
        {
            if(_blockMap[ShapeRow, ShapeCol + 1] is EmptyBlock)
            {
                _currentBlock.MoveRight();
                _blockMap[ShapeRow, ShapeCol + 1] = _blockMap[ShapeRow, ShapeCol];
                MakeEmpty(ShapeRow, ShapeCol);
                ShapeCol++;
            }
        }

        /// <summary>
        /// Moves the currently active Shape right
        /// </summary>
        public void ShapeRight()
        {
            for (int i = _shape.ShapeMap.GetUpperBound(0); i >= 0; i--)
            {
                if (!(_blockMap[_shape.ShapeMap[i, _shape.ShapeMap.GetUpperBound(1)].MapPositionY, _shape.ShapeMap[i, _shape.ShapeMap.GetUpperBound(1)].MapPositionX + 1] is EmptyBlock))
                {
                    return;
                }
            }

            ShapeCol++;
            RenderShape();
        }

        /// <summary>
        /// Moves the currrently active Shape left
        /// </summary>
        public void ShapeLeft()
        {
            for (int i = _shape.ShapeMap.GetUpperBound(0); i >= 0; i--)
            {
                if (!(_blockMap[_shape.ShapeMap[i, 0].MapPositionY, _shape.ShapeMap[i, 0].MapPositionX - 1] is EmptyBlock))
                {
                    return;
                }
            }

            ShapeCol--;
            RenderShape();
        }

        /// <summary>
        /// Rotates the shape left (clockwise)
        /// </summary>
        public void RotateLeft()
        {
            _shape.RotateLeft();
        }

        /// <summary>
        /// Rotates the shape right (counter clockwise)
        /// </summary>
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