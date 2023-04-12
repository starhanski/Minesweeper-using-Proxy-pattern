using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab10
{
    
    interface IMinesweeperGame
    {
        void StartGame();
        void RevealCell(int row, int col);
        void MarkCell(int row, int col);
        bool IsGameOver();
    }

   
    class MinesweeperGame : IMinesweeperGame
    {
        public int[,] board; 
        public bool[,] revealed; 
        public bool[,] marked; 
        public int numMines; 
        public int numRevealed; 
        public bool gameOver; 
        public int numRows; 
        public int numCols; 

        public MinesweeperGame(int numRows, int numCols, int numMines)
        {
            this.numRows = numRows;
            this.numCols = numCols;
            this.numMines = numMines;
            this.numRevealed = 0;
            this.gameOver = false;

     
            this.board = new int[numRows, numCols];
            this.revealed = new bool[numRows, numCols];
            this.marked = new bool[numRows, numCols];

           
            Random rand = new Random();
            for (int i = 0; i < numMines; i++)
            {
                int row = rand.Next(numRows);
                int col = rand.Next(numCols);
                if (board[row, col] != -1) // якщо комірка вже містить міну, пропускаємо
                {
                    board[row, col] = -1;
                }
                else
                {
                    i--;
                }
            }

            
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (board[row, col] != -1)
                    {
                        int count = 0;
                        for (int r = Math.Max(0, row - 1); r <= Math.Min(numRows - 1, row + 1); r++)
                        {
                            for (int c = Math.Max(0, col - 1); c <= Math.Min(numCols - 1, col + 1); c++)
                            {
                                if (board[r, c] == -1)
                                         {
                                    count++;
                                }
                            }
                        }
                        board[row, col] = count;
                    }
                }
            }
        }

        public void StartGame()
        {
           
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    revealed[row, col] = false;
                    marked[row, col] = false;
                }
            }

            numRevealed = 0;
            gameOver = false;
        }

        public void RevealCell(int row, int col)
        {
            if (!gameOver && !revealed[row, col] && !marked[row, col])
            {
                revealed[row, col] = true;
                numRevealed++;

                if (board[row, col] == -1)
                {
                   
                    gameOver = true;
                }
                else if (board[row, col] == 0)
                {
                  
                    for (int r = Math.Max(0, row - 1); r <= Math.Min(numRows - 1, row + 1); r++)
                    {
                        for (int c = Math.Max(0, col - 1); c <= Math.Min(numCols - 1, col + 1); c++)
                        {
                            RevealCell(r, c);
                        }
                    }
                }
            }
        }

        public void MarkCell(int row, int col)
        {
            if (!gameOver && !revealed[row, col])
            {
                marked[row, col] = !marked[row, col];
            }
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public int GetCellValue(int row, int col)
        {
            return board[row, col];
        }
        
    }
    
}
