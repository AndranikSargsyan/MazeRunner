/*
 * Author: Andranik Sargsyan 
*/

using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MazeRunner
{
    public class RandomRunner : Runner
    {
        private ArrayList possibleDirections;
        private Random randomDir;

        public RandomRunner(Maze maze, Position pos)
        {
            this.name = "Random Runner";
            this.runnerImage = new Image();
            this.maze = maze;
            this.position = pos;
            this.direction = LEFT;
            this.possibleDirections = new ArrayList(4);
            this.randomDir = new Random();
        }

        public override bool run()
        {
            if (maze.isExit(position))
            {
                return false;
            }

            possibleDirections.Clear();

            for (int i = 0; i < movements.Length; i++)
            {
                if (movements[i, 0] == direction)
                {
                    // If the current direction is available adds it to the possible directions
                    if (canMove(direction))
                    {
                        possibleDirections.Add(direction);
                    }
                    // If the RIGHT direction is available adds it to the possible directions
                    if (canMove(movements[i, 1]))
                    {
                        possibleDirections.Add(movements[i, 1]);
                    }
                    // If the LEFT direction is available adds it to the possible directions
                    if (canMove(movements[i, 2]))
                    {
                        possibleDirections.Add(movements[i, 2]);
                    }
                    // If there is more than one possible routes to take, chooses randomly
                    if (possibleDirections.Count > 1)
                    {
                        int r = randomDir.Next(0, possibleDirections.Count);
                        direction = (Position) possibleDirections[r];
                    }
                    else if (possibleDirections.Count == 1)
                    {
                        direction = (Position) possibleDirections[0];
                    }
                    else
                    {
                        // If there is no other way to move, turns back
                        direction = movements[i, 3];
                    }

                    position = position + direction;
                    return true;
                }
            }

            return false;
        }     
    }
}
