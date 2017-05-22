/*
 * Author: Andranik Sargsyan 
*/

using System;

namespace MazeRunner
{
    public struct Position
    {
        public int X, Y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Position operator + (Position x1, Position x2)
        {
            return new Position( x1.X + x2.X, x1.Y + x2.Y );
        }

        public static bool operator == (Position x1, Position x2)
        {
            if ((x1.X == x2.X) && (x1.Y == x2.Y))
            {
                return true;
            } else
            {
                return false;
            }        
        }

        public static bool operator != (Position x1, Position x2)
        {
            if ((x1.X != x2.X) || (x1.Y != x2.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return (String.Format("({0}, {1})", X, Y));
        }
    }
}
