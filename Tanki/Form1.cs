using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanki
{
    public partial class Form1 : Form
    {
        Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
            game.StartGame();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                game.playerTankController.Move(0, -1);
            }
            else
            {
                switch (comboBox1.SelectedIndex) 
                {
                    case 0:
                        game.playerTankController.Shoot(0, -1, new ShortProjectile()); break;
                    case 1:
                        game.playerTankController.Shoot(0, -1, new MediumProjectile()); break;
                    case 2:
                        game.playerTankController.Shoot(0, -1, new LongProjectile()); break;
                }
            }
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                game.playerTankController.Move(1, 0);
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        game.playerTankController.Shoot(1, 0, new ShortProjectile()); break;
                    case 1:
                        game.playerTankController.Shoot(1, 0, new MediumProjectile()); break;
                    case 2:
                        game.playerTankController.Shoot(1, 0, new LongProjectile()); break;
                }
            }
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                game.playerTankController.Move(-1, 0);
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        game.playerTankController.Shoot(-1, 0, new ShortProjectile()); break;
                    case 1:
                        game.playerTankController.Shoot(-1, 0, new MediumProjectile()); break;
                    case 2:
                        game.playerTankController.Shoot(-1, 0, new LongProjectile()); break;
                }
            }
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                game.playerTankController.Move(0, 1);
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        game.playerTankController.Shoot(0, 1, new ShortProjectile()); break;
                    case 1:
                        game.playerTankController.Shoot(0, 1, new MediumProjectile()); break;
                    case 2:
                        game.playerTankController.Shoot(0, 1, new LongProjectile()); break;
                }
            }
            Refresh();
        }
    }
}
