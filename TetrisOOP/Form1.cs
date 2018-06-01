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
            tetris.NewBlock += new  EventHandler(newBlockOnMap);
        }


        private void BtnPause_Click(object sender, EventArgs e)
        {
            tetris.Toggle();
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
    }
}
