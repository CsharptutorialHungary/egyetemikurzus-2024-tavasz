using System;

namespace Pacman.GameClasses
{
    class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void ResetPosition(int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
        }
    }
}
