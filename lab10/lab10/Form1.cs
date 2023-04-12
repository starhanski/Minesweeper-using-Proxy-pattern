using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab10
{
    public partial class Form1 : Form
    {
       
            private int numRows = 10;
            private int numCols = 10;
            private int numMines = 12;
            private IMinesweeperGame game;
        
            public Form1()
        {
            InitializeComponent();
            game = new MinesweeperGameProxy(numRows, numCols, numMines, this);
            StartGame();
        }
        private void StartGame()
        {
            game.StartGame();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    Button button = new Button();
                    button.Name = "button" + row + col;
                    button.Size = new Size(30, 30);
                    button.Location = new Point(col * 30, row * 30 + 50);
                    button.Click += new EventHandler(OnButtonClick);
                    this.Controls.Add(button);
                }
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            string name = button.Name;
            int row = int.Parse(name.Substring(6, 1));
            int col = int.Parse(name.Substring(7, 1));

            if (Control.ModifierKeys == Keys.Alt)
            {
                game.MarkCell(row, col);
            }
            else
            {
                game.RevealCell(row, col);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
