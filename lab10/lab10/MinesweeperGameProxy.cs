using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab10
{
    class MinesweeperGameProxy : IMinesweeperGame
    {
        public MinesweeperGame game;
        public Form form;
        public MinesweeperGameProxy(int numRows, int numCols, int numMines, Form form)
        {
            this.game = new MinesweeperGame(numRows, numCols, numMines);
            this.form = form;
        }

        public void StartGame()
        {
            game.StartGame();
            UpdateForm();
        }

        public void RevealCell(int row, int col)
        {
            game.RevealCell(row, col);
            UpdateForm();

            if (game.IsGameOver())
            {
                MessageBox.Show("Гра закінчена!");
            }
        }

        public void MarkCell(int row, int col)
        {
            game.MarkCell(row, col);
            UpdateForm();
        }

        public bool IsGameOver()
        {
            return game.IsGameOver();
        }

        private void UpdateForm()
        {
            for (int row = 0; row < game.numRows; row++)
            {
                for (int col = 0; col < game.numCols; col++)
                {
                    Button button = (Button)form.Controls["button" + row + col];
                    if (game.IsGameOver() && game.GetCellValue(row, col) == -1)
                    {
                        button.BackColor = Color.Red;
                    }
                    else if (game.revealed[row, col])
                    {
                        button.Enabled = false;
                        button.Text = game.GetCellValue(row, col).ToString();
                    }
                    else if (game.marked[row, col])
                    {
                        button.Text = "X";
                    }
                  
                }
            }
        }
    }
}
