using System;
using System.Linq;

namespace Pacman.GameClasses
{
    class Monster
    {
        private Position monsterPos;
        public int prevPosX;
        public int prevPosY;

        private readonly string symbol = ((char)9787).ToString();
        private readonly ConsoleColor color;
        public string Direction = "up";

        public static readonly string[] possibleDirections = { "up", "down", "left", "right" };
        public static readonly Random random = new Random();

        public Monster(ConsoleColor color, int x, int y)
        {
            this.color = color;
            this.monsterPos = new Position(x, y);
            this.prevPosX = x;
            this.prevPosY = y;
        }

        public void ResetMonster()
        {
            this.monsterPos.ResetPosition(prevPosX, prevPosY);
        }

        public bool CheckCell(Monster[] monsterList, int x, int y, string[,] border)
        {
            if (monsterList.Any(monster => x == monster.GetPosX() && y == monster.GetPosY()) || border[y, x] == "#")
            {
                return false;
            }
            return true;
        }

        public string GetSymbol() => this.symbol;
        public int GetPosX() => this.monsterPos.X;
        public int GetPosY() => this.monsterPos.Y;
        public ConsoleColor GetColor() => this.color;

        public void EraseMonster()
        {
            Console.SetCursorPosition(prevPosX, prevPosY);
            Console.Write(' ');
        }

        public void MoveRight()
        {
            if (monsterPos.X + 1 < 34)
            {
                UpdatePreviousPosition();
                monsterPos.X++;
            }
        }

        public void MoveLeft()
        {
            if (monsterPos.X - 1 > 0)
            {
                UpdatePreviousPosition();
                monsterPos.X--;
            }
        }

        public void MoveDown()
        {
            if (monsterPos.Y + 1 < 28)
            {
                UpdatePreviousPosition();
                monsterPos.Y++;
            }
        }

        public void MoveUp()
        {
            if (monsterPos.Y - 1 > 0)
            {
                UpdatePreviousPosition();
                monsterPos.Y--;
            }
        }

        private void UpdatePreviousPosition()
        {
            prevPosX = monsterPos.X;
            prevPosY = monsterPos.Y;
        }
    }
}
