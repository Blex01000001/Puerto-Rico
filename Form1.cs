using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puerto_Rico
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game game = new Game(3);
            //Game game1 = new Game(5);
            //Game game2 = new Game(5);
            //Game game3 = new Game(5);
            //Game game4 = new Game(5);
            //Game game5 = new Game(5);
            //Console.WriteLine("game");
        }
    }
}
