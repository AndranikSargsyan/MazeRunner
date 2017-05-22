/*
 * Author: Andranik Sargsyan 
*/

using System;
using System.Windows.Controls;

namespace MazeRunner
{
    public abstract class Runner
    {
        protected static Position RIGHT = new Position(1, 0);
        protected static Position LEFT = new Position(-1, 0);
        protected static Position UP = new Position(0, -1);
        protected static Position DOWN = new Position(0, 1);

        /* This two dimensional array maps the directions from runners perspective to ours 
         * in the following manner.
         * 
         * 1. First column represents the FORWARD side and the movement of the runner from our perspective
         * 2. Second column represents the RIGHT side of the runner
         * 3. Third column represents the LEFT side of the runner
         * 4. Fourth column represents the FORWARD
        */
        protected Position[,] movements = new Position[4, 4]
        {
           {RIGHT, DOWN, UP, LEFT},
           {LEFT, UP, DOWN, RIGHT},
           {DOWN, LEFT, RIGHT, UP},
           {UP, RIGHT, LEFT, DOWN}
        };

        protected string name;
        protected Image runnerImage;
        protected Maze maze;
        protected Position position;
        protected Position direction;
                    
        public int getX()
        {
            return position.X;
        }

        public int getY()
        {
            return position.Y;
        }

        public string getName()
        {
            return this.name;
        }

        public Image getImage()
        {
            return this.runnerImage;
        }

        public bool canMove(Position dir)
        {
            try
            {
                if (!maze.isBlock(position + dir))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public virtual void setPosition(int posX, int posY)
        {
            this.position.X = posX;
            this.position.Y = posY;
        }      

        public virtual bool run()
        {
            return true;
        }    
    }
}
