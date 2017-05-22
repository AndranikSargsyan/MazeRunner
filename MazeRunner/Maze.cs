/*
 * Author: Andranik Sargsyan 
*/

using System;

namespace MazeRunner
{
    public sealed class Maze
    {
        private const int MAZE_WIDTH = 20;
        private const int MAZE_HEIGHT = 20;
        private const string BLOCK_CHAR = "#";

        private string[,] blueprint;
        private Position exit;

        private static Maze instance = null;
        private static readonly object padlock = new object();

        private Maze()
        {
            blueprint = createBlueprint();
        }

        public static Maze getInstance()
        {
            lock (padlock)
            {
                if(instance == null)
                {
                    instance = new Maze();
                }

                return instance;
            }
        }

        private string[,] createBlueprint()
        {
            string[,] blueprint = new string[MAZE_HEIGHT, MAZE_WIDTH];

            string input = MazeRunner.Properties.Resources.maze;
            // r is the row index of the matrix
            int r = 0;
            foreach (string row in input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                // c is the column index of the matrix 
                int c;
                for (c = 0; c<MAZE_WIDTH; c++)
                {
                    blueprint[r, c] = row[c].ToString();
                    if((c==0 || c==(MAZE_WIDTH-1) || r==0 || r == (MAZE_HEIGHT - 1)) && blueprint[r ,c] != "#")
                    {
                        setExitPoint(new Position(c, r));
                    }
                }
                r++;
            }

            return blueprint;
        }

        public int getHeight()
        {
            return MAZE_HEIGHT;
        }

        public int getWidth()
        {
            return MAZE_WIDTH;
        }

        public string[,] getBlueprint()
        {
            return blueprint;
        }

        public Position getExitPoint()
        {
            return exit;
        }

        public bool isBlock(Position location)
        {
            if (blueprint[location.Y, location.X] == BLOCK_CHAR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isBlock(int posX, int posY)
        {
            if (blueprint[posY, posX] == BLOCK_CHAR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isExit(Position location)
        {
            if(location.Equals(exit))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private bool setExitPoint(Position ex)
        {
            if(ex.X > MAZE_WIDTH || ex.Y > MAZE_HEIGHT)
            {
                throw new Exception("Maze overflow exception occured");
            } else
            {
                exit = ex;
                return true;
            }
        }      
    }
}
