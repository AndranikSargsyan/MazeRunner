/*
 * Author: Andranik Sargsyan 
*/

using System;
using System.Windows.Controls;

namespace MazeRunner
{
    public class RighthandRunner : Runner
    {
        // This represents the position of right block relative to the runner
        private Position rightBlock;

        public RighthandRunner(Maze maze, Position pos)
        {
            this.name = "Righthand Runner";
            this.runnerImage = new Image();
            this.maze = maze;
            this.direction = RIGHT;
            this.rightBlock = DOWN;
            this.position = pos;   
        }

        // Updates Runner's position according to "Righthand Runner's Rule"
        public override bool run()
        {
            if (maze.isExit(position))
            {
                return false;
            }

            for (int i = 0; i < movements.Length; i++)
            {
                // Here I am matching the runners perspective to ours
                if (movements[i, 0] == direction)
                {                
                    if (rightBlock == movements[i, 1])
                    {
                        if (canMove(movements[i, 1]))
                        {
                            direction = movements[i, 1];
                            rightBlock = movements[i, 3];
                        }
                        else if (canMove(direction))
                        {
                            position = position + direction;                         
                            return true;
                        }
                        else if (canMove(movements[i, 2]))
                        {
                            direction = movements[i, 2];
                            rightBlock = movements[i, 0];
                        }
                        else
                        {
                            direction = movements[i, 3];
                            rightBlock = movements[i, 2];
                        }
                    }
                    else if (rightBlock == movements[i, 2])
                    {
                        if (canMove(movements[i, 2]))
                        {
                            direction = movements[i, 2];
                            rightBlock = movements[i, 3];
                        }
                        else if (canMove(direction))
                        {
                            position = position + direction;
                            return true;
                        }
                        else if (canMove(movements[i, 1]))
                        {
                            direction = movements[i, 1];
                            rightBlock = movements[i, 0];
                        }
                        else
                        {
                            direction = movements[i, 3];
                            rightBlock = movements[i, 1];
                        }
                    }

                    position = position + direction;
                    return true;
                }
            }
            return false;
        }

        public Position getRightBlock()
        {
            return rightBlock;
        }      
    }
}
