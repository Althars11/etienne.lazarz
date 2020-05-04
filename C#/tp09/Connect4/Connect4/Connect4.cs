using System;
using System.Data;

namespace Connect4
{
    public class Connect4
    {
        public enum state
        {
            YELLOW,
            RED,
            EMPTY,
        }
        
        public state[,] grid;
        
        public Connect4()
        {
            grid = new state[6,7];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    grid[i, j] = state.EMPTY;
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("|"+grid[i,j]);
                }
                Console.Write("|");
                Console.WriteLine();
                for (int j = 0; j < 15; j++)
                {
                    Console.Write("-");
                }
            }
        }

        public bool Play(StatementType player, int column)
        {
            if (column<0 || column>6)
            {
                return false;
            }

            bool isempty = false;
            int i = 6;
            while (isempty || i>-1)
            {
                if (grid[i,column]== state.EMPTY)
                {
                    grid[i, column] = (state)player;
                    isempty = true;
                }
                i--;
            }

            return isempty;
        }

        public bool Check_Win(state player)
        {
            //check row
            int compteur = 0;
            bool is_victorious = false;
            for (int i = 0; i < 6; i++)//ligne
            {
                for (int j = 0; j < 4; j++)//colone
                {
                    if ((grid[i,j]==player && grid[i,j+1]==player)==(grid[i,j+2]==player && grid[i,j+3] == player))
                    {
                        is_victorious = true;
                    }
                }
            }
            
            //check column
            if (!is_victorious)
            {
                for (int i = 0; i < 7; i++)//colone
                {
                    for (int j = 0; j < 3; j++)//ligne
                    {
                        if ((grid[j,i]==player && grid[j+1,i]==player)==(grid[j+2,i]==player && grid[j+3,i]==player))
                        {
                            is_victorious = true;
                        }
                    }
                }
            }
            
            //check diagonales
            

            return is_victorious;
        }
    }
}