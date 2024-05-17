using Pacman.GameClasses;
using System;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using System.Linq;

namespace Pacman
{
    class Game
    {
        static Random random = new Random();
        static bool gamePaused = false;
        static bool pausedTextIsShown = false;
        public static bool continueLoop = true;

        static GameBoard board = new GameBoard();
        static string[,] border = board.GetBoard;

        public static PacMan pacman = new PacMan(board);

        public static bool IsTestEnvironment { get; set; } = false;

        public delegate ConsoleKey GetKeyDelegate();
        public static GetKeyDelegate GetKey { get; set; } = () => Console.ReadKey(true).Key;

        static Monster[] monsterList =
        {
            new Monster(ConsoleColor.Red,15,8),
            new Monster(ConsoleColor.Cyan,16,12),
            new Monster(ConsoleColor.Magenta,17,12),
            new Monster(ConsoleColor.DarkCyan,18,12),
        };

        const int GameWidth = 70;
        const int GameHeight = 29;

        static int totalMonsters;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Pacman by Janos";
            Console.WindowWidth = GameWidth;
            Console.BufferWidth = GameWidth;
            Console.WindowHeight = GameHeight;
            Console.BufferHeight = GameHeight;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            totalMonsters = monsterList.Count();

            ShowWelcomeMenu();

            RedrawBoard();
            LoadGUI();


            while (continueLoop)
            {
                ReadUserKey();

                if (gamePaused)
                {
                    BlinkPausedText();
                    continue;
                }

                MonsterAi();

                LoadPlayer();

                PlayerMovement();


                LoadGUI();


                CheckIfNoLives();

                CheckScore();

                Thread.Sleep(200);
            }

        }

        static int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        static void LoadGUI()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(40, 2);
            Console.Write("Level: {0}", pacman.GetLevel());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(40, 4);
            Console.Write("Score: {0}", pacman.Score);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 6);
            Console.Write("Lives: {0}", pacman.Lives());
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 8);
            Console.Write("Total Monsters: {0}", totalMonsters);

            Console.SetCursorPosition(40, 10);
            Console.Write($"PacMan Position: ({pacman.GetPosX()}, {pacman.GetPosY()})");

            var nearbyMonsters = monsterList
                .Where(monster => CalculateDistance(pacman.GetPosX(), pacman.GetPosY(), monster.GetPosX(), monster.GetPosY()) <= 5)
                .ToList();

            Console.SetCursorPosition(40, 12);
            Console.Write("Monsters within 5 steps: {0}", nearbyMonsters.Count);

            Console.SetCursorPosition(40, 14);
            Console.Write("Monster Positions: ");
            for (int i = 0; i < monsterList.Length; i++)
            {
                Console.SetCursorPosition(40, 15 + i);
                Console.Write($"Monster {monsterList[i].GetColor()}: ({monsterList[i].GetPosX()}, {monsterList[i].GetPosY()})");

            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, GameHeight - 8);
            Console.Write("{0}", new string('-', 22));
            Console.SetCursorPosition(40, GameHeight - 7);
            Console.Write("|  PRESS P TO PAUSE  |");
            Console.SetCursorPosition(40, GameHeight - 6);
            Console.Write("|  PRESS ESC TO EXIT |");
            Console.SetCursorPosition(40, GameHeight - 5);
            Console.Write("|  PRESS R TO RESET  |");
            Console.SetCursorPosition(40, GameHeight - 4);
            Console.Write("{0}", new string('-', 22));
        }

        static void LoadPlayer()
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetSymbol());
        }

        static void LoadMonsters()
        {
            foreach (var monster in monsterList)
            {
                Console.ForegroundColor = monster.GetColor();
                Console.SetCursorPosition(monster.GetPosX(), monster.GetPosY());
                Console.Write(monster.GetSymbol());
            }
        }

        public static void ReadUserKey()
        {
            if (IsTestEnvironment || Console.KeyAvailable)
            {
                ConsoleKey key = GetKey();
                switch (key)
                {
                    case ConsoleKey.Escape:
                        continueLoop = false;
                        GameOver();
                        break;
                    case ConsoleKey.P:
                        SetGamePaused();
                        break;
                    case ConsoleKey.UpArrow:
                        pacman.NextDirection = "up";
                        break;
                    case ConsoleKey.DownArrow:
                        pacman.NextDirection = "down";
                        break;
                    case ConsoleKey.LeftArrow:
                        pacman.NextDirection = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        pacman.NextDirection = "right";
                        break;
                    case ConsoleKey.R:
                        ResetGame();
                        break;
                }
            }
        }


        public static void ResetGame()
        {
            continueLoop = true;

            if (!IsTestEnvironment)
            {
                Console.Clear();
            }

            pacman.ResetScore();
            pacman.ResetLives();
            pacman.ResetLevel();

            pacman.ResetPacMan();

            foreach (var monster in monsterList)
            {
                monster.ResetMonster();
            }

            board = new GameBoard();
            border = board.GetBoard;

            RedrawBoard();
        }

        static void SetGamePaused()
        {
            switch (gamePaused)
            {
                case false:
                    ShowPausedText(true);
                    break;
                case true:
                    ShowPausedText(false);
                    break;
            }

            gamePaused = gamePaused ? false : true;
        }

        static void BlinkPausedText()
        {
            switch (pausedTextIsShown)
            {
                case true:
                    Thread.Sleep(100);
                    ShowPausedText(false);
                    break;
                case false:
                    Thread.Sleep(100);
                    ShowPausedText(true);
                    break;
            }
        }

        static void ShowPausedText(bool showText)
        {
            switch (showText)
            {
                case true:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(47, GameHeight - 2);
                    Console.Write("PAUSED");
                    pausedTextIsShown = true;
                    break;
                case false:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(47, GameHeight - 2);
                    Console.Write("      ");
                    pausedTextIsShown = false;
                    break;
            }
        }

        public static void PlayerMovement()
        {
            switch (pacman.CheckCell(border, pacman.NextDirection, monsterList))



            {
                case BoardElements.Dot:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnPoint();
                    pacman.Direction = pacman.NextDirection;
                    LoadGUI();
                    break;
                case BoardElements.Star:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnStar();
                    pacman.Direction = pacman.NextDirection;
                    LoadGUI();
                    break;
                case BoardElements.Empty:
                    MovePlayer(pacman.NextDirection);
                    pacman.Direction = pacman.NextDirection;


                    break;
                case BoardElements.Monster:
                    pacman.LoseLife();
                    MovePlayer("reset");
                    LoadGUI();
                    break;
                case BoardElements.Wall:
                    switch (pacman.CheckCell(border, pacman.Direction, monsterList))
                    {
                        case BoardElements.Dot:
                            MovePlayer(pacman.Direction);
                            pacman.EarnPoint();
                            LoadGUI();
                            break;
                        case BoardElements.Star:
                            MovePlayer(pacman.Direction);
                            pacman.EarnStar();
                            LoadGUI();
                            break;
                        case BoardElements.Empty:
                            MovePlayer(pacman.Direction);
                            break;
                        case BoardElements.Monster:
                            pacman.LoseLife();
                            MovePlayer("reset");
                            LoadGUI();
                            break;
                        case BoardElements.Wall:
                            break;
                    }
                    break;
            }
        }
        static void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "up":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeBoard();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY() - 1);
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveUp();
                    break;
                case "right":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeBoard();
                    Console.SetCursorPosition(pacman.GetPosX() + 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveRight();
                    break;
                case "down":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeBoard();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY() + 1);
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveDown();
                    break;
                case "left":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeBoard();
                    Console.SetCursorPosition(pacman.GetPosX() - 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveLeft();
                    break;
                case "reset":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeBoard();
                    pacman.ResetPacMan();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    break;
            }
        }

        static void MonsterAi()
        {
            foreach (var monster in monsterList)
            {
                Console.SetCursorPosition(monster.GetPosX(), monster.GetPosY());
                if (border[monster.GetPosY(), monster.GetPosX()] == " ")
                {
                    Console.Write(' ');
                }
                else if (border[monster.GetPosY(), monster.GetPosX()] == ".")
                {
                    Console.Write('.');
                }
                else if (border[monster.GetPosY(), monster.GetPosX()] == "*")
                {
                    Console.Write('*');
                }

                monster.Direction = Monster.possibleDirections[Monster.random.Next(0, Monster.possibleDirections.Length)];

                switch (monster.Direction)
                {
                    case "up":
                        if (monster.CheckCell(monsterList, monster.GetPosX(), monster.GetPosY() - 1, border))
                            monster.MoveUp();
                        break;
                    case "down":
                        if (monster.CheckCell(monsterList, monster.GetPosX(), monster.GetPosY() + 1, border))
                            monster.MoveDown();
                        break;
                    case "left":
                        if (monster.CheckCell(monsterList, monster.GetPosX() - 1, monster.GetPosY(), border))
                            monster.MoveLeft();
                        break;
                    case "right":
                        if (monster.CheckCell(monsterList, monster.GetPosX() + 1, monster.GetPosY(), border))
                            monster.MoveRight();
                        break;
                }
            }


            LoadMonsters();
        }




        public static void CheckScore()
        {
            if (pacman.Score == 684)
            {
                continueLoop = false;
                WinGame();
            }
        }


        public static void CheckIfNoLives()
        {
            if (pacman.Lives() <= 0)
            {
                continueLoop = false;
                GameOver();
            }

        }

        static void RedrawBoard()
        {

            if (!IsTestEnvironment)
            {
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < board.GetBoard.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetBoard.GetLength(1); j++)
                {
                    Console.Write("{0}", board.GetBoard[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void ChangeBoard()
        {
            border[pacman.GetPosY(), pacman.GetPosX()] = " ";
        }



        static void ShowWelcomeMenu()
        {
            PlayMusicAsync();
            RedrawBoard();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 28));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||     PRESS X TO START     ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||     PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            Console.Write("|{0}|", new string('-', 28));
            Console.ForegroundColor = ConsoleColor.White;

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (keyPressed.Key == ConsoleKey.X)
                {
                    Console.Clear();
                    break;
                }

                keyPressed = Console.ReadKey(true);
            }
        }


        static void GameOver()
        {
            Console.Clear();
            RedrawBoard();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        GAME OVER        ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            int score = pacman.GetScore();
            Console.Write("||       SCORE: {0}{1}  ||", score, new string(' ', 9 - score.ToString().Length));
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GameHeight - 1);

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }


                keyPressed = Console.ReadKey(true);
            }
        }

        static void WinGame()
        {
            Console.Clear();
            RedrawBoard();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        YOU WON!         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            int score = pacman.Score;
            Console.Write("||       SCORE: {0}{1}  ||", score, new string(' ', 9 - score.ToString().Length));
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 5);
            Console.Write("||    PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 6);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GameHeight - 1);

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                keyPressed = Console.ReadKey(true);
            }
        }

        public static void PlayMusic()
        {
            Task.Factory.StartNew(() => Music());
        }

        public static async Task PlayMusicAsync()
        {
            await Task.Run(() => Music());
        }

        public static void Music()
        {

            SoundPlayer PacManMusic = new SoundPlayer(Pacman.PacManMusic.pacman_beginning);
            PacManMusic.Play();

        }
    }
}