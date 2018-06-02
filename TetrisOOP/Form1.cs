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
            tetris = new Tetris(this, 9, 14);
            tetris.Start();
            tetris.NewBlock += new EventHandler(newBlockOnMap);
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
            BtnLeft.Enabled = tetris.BlockLeft();
            BtnRight.Enabled = true;

        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            BtnRight.Enabled = tetris.BlockRight();
            BtnLeft.Enabled = true;
        }

        private void newBlockOnMap(object sender, EventArgs e)
        {
            BtnLeft.Enabled = true;
            BtnRight.Enabled = true;
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
