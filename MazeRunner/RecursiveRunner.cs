/*
 * Author: Andranik Sargsyan 
*/

using System;
using System.Windows.Controls;

namespace MazeRunner
{
    public class RecursiveRunner : Runner
    {
        // Recursive Runner remembers where he/she has been
        private bool[,] wasHere;
        // Here Recursive Runner saves the correct path he/she found
        private bool[,] correctPath;

        public RecursiveRunner(Maze maze, Position pos)
        {
            this.name = "Recursive Runner";
            this.runnerImage = new Image();
            this.maze = maze;
            this.position = pos;
            this.direction = LEFT;
            this.initializePaths();
            this.solve(position);             
        }

        // Recursive Runner has its own way of setting position, 
        // because once we change the starting point, changes the correct path
        public override void setPosition(int posX, int posY)
        {
            this.position.X = posX;
            this.position.Y = posY;
            this.initializePaths();
            this.solve(position);
        }

        // This method initializes/removes Recursive Runner's memory
        private void initializePaths()
        {
            this.wasHere = new bool[maze.getWidth(), maze.getHeight()];
            this.correctPath = new bool[maze.getWidth(), maze.getHeight()];

            for(int i=0; i < maze.getWidth(); i++)
            {
                for(int j = 0; j < maze.getHeight(); j++)
                {
                    wasHere[i, j] = false;
                    correctPath[i, j] = false;
                }
            }
        }

        // Solves the Maze
        public bool solve(Position pos)
        {
            if(maze.isExit(pos))
            {            
                correctPath[pos.Y, pos.X] = true;
                return true;
            }

            if(maze.isBlock(pos.X, pos.Y) || wasHere[pos.Y, pos.X])
            {
                return false;
            }

            wasHere[pos.Y, pos.X] = true;

            if(pos.X != 0)
            {
                if(solve(pos + LEFT))
                {
                    correctPath[pos.Y, pos.X] = true;
                    return true;
                }
            }

            if(pos.X != maze.getWidth())
            {
                if(solve(pos + RIGHT))
                {
                    correctPath[pos.Y, pos.X] = true;
                    return true;
                }
            }

            if(pos.Y!= 0)
            {
                if (solve(pos + UP))
                {
                    correctPath[pos.Y, pos.X] = true;
                    return true;
                }
            }

            if (pos.Y != maze.getHeight())
            {
                if (solve(pos + DOWN))
                {
                    correctPath[pos.Y, pos.X] = true;
                    return true;
                }
            }

            return false;
        } 

        // This method updates Runner's position according to Correct Path
        public override bool run()
        {
            if (maze.isExit(position))
            {
                return false;
            }

            for (int i = 0; i < movements.Length; i++)
            {
                if(movements[i, 0] == direction)
                {
                    Position testDir = position + direction;
                    Position testRight = position + movements[i, 1];
                    Position testLeft = position + movements[i, 2];
                    Position testBack = position + movements[i, 3];

                    if (correctPath[testDir.Y, testDir.X])
                    {
                        position = testDir;
                        return true;
                    }
                    else if (correctPath[testRight.Y, testRight.X])
                    {
                        position = testRight;
                        direction = movements[i, 1];
                        return true;
                    }
                    else if(correctPath[testLeft.Y, testLeft.X])
                    {
                        position = testLeft;
                        direction = movements[i, 2];
                        return true;
                    }
                    else if(correctPath[testBack.Y, testBack.X])
                    {
                        position = testBack;
                        direction = movements[i, 3];
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
