using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOOP
{
    public partial class Form1 : Form
    {
        private Tetris tetris;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tetris = new Tetris(this, 9, 14, ref Lbllevel);
            tetris.Start();
            tetris.GameOver += Tetris_GameOver;
        }

        /// <summary>
        /// Restarts the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Restart(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            Form1_Load(sender, e);
        }

        private void Tetris_GameOver(object sender, EventArgs e)
        {
            if (MessageBox.Show("Game over - Try again", "Tetris", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Restart(sender, e);
            }
            else
            {
                this.Close();
            }

        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            tetris.Toggle();
            if (BtnPause.Text == "Start")
                BtnPause.Text = "Stop";
            else
                BtnPause.Text = "Start";
        }

        private void BtnDrop_Click(object sender, EventArgs e)
        {
            tetris.Drop();
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            tetris.ShapeLeft();

        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            tetris.ShapeRight();
        }
        

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture down arrow key
            if (keyData == Keys.Down || keyData == Keys.Enter)
            {
                BtnDrop_Click(new object(), new EventArgs());
                return true;
            }
            //capture left arrow key
            if (keyData == Keys.Left)
            {
                BtnLeft_Click(new object(), new EventArgs());
                return true;
            }
            //capture right arrow key
            if (keyData == Keys.Right)
            {
                BtnRight_Click(new object(), new EventArgs()); 
                return true;
            }
            if(keyData == Keys.Space)
            {
                BtnPause_Click(new object(), new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
