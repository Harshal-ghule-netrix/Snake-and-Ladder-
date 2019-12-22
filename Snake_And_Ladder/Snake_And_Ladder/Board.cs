using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake_And_Ladder
{
    class Board
    {
        public List<Player> Players;
        List<Snake> Snakes;
        List<Ladder> Ladders;
        public Hashtable Snake_and_ladder;

        public Board()
        {
            Players = new List<Player> ();         
            Snakes = InitalizeSnakes();
            Ladders = InitalizeLadders();
            GenerateSnakesAndLadders();
        }

        private List<Snake> InitalizeSnakes()
        {
            var snakes = new List<Snake>
            {
                new Snake() { Head = 40, Tail = 20 },
                new Snake() { Head = 50, Tail = 16 },
                new Snake() { Head = 92, Tail = 52 },
                new Snake() { Head = 95, Tail = 36 },
                new Snake() { Head = 81 , Tail = 78}
            };

            return snakes;
        }

        private List<Ladder> InitalizeLadders()
        {
            var ladders = new List<Ladder>
            {
                new Ladder() { Start = 4 , End = 22},
                new Ladder() { Start = 14 , End = 77},
                new Ladder() { Start = 33 , End = 51},
                new Ladder() { Start = 64 , End = 82},
                new Ladder() { Start = 74 , End = 90},
            };

            return ladders;
        }

        private void GenerateSnakesAndLadders()
        {
            Snake_and_ladder = new Hashtable();

            foreach (Snake snake in Snakes)
            {
                Snake_and_ladder.Add(snake.Head, snake.Tail);
            }

            foreach (Ladder ladder in Ladders)
            {
                Snake_and_ladder.Add(ladder.Start, ladder.End);
            }            
        }

        private bool IsSnakeOrLadder(int iNum)
        {
            if (Snake_and_ladder.ContainsKey(iNum))
            {
                return true;
            }
            return false;
        }

        private void DisplaySnakeAndLadder(int Start , int End)
        {
            if (Start > End) 
            {
                Console.Write("~ {0}",End); // Snake
            }
            else            
            {
                Console.Write("# {0}", End); // Ladder 
            }
        }

        private void DisplayPlayerRight_Left(Player player)
        {
            Console.Write("\n");
            int pos = 11 - player.Position % 10;
            if (pos == 11)
            {
                pos = 1;
            }
            for (int i = 1; i < (pos*2); i++)
            {
                Console.Write("\t");
            }
            Console.Write(player.Name);
        }

        private void DisplayPlayerLeft_Right(Player player)
        {
            Console.Write("\n");
            int pos = player.Position % 10;
            if (pos == 0)
            {
                pos = 10;
            }
            for (int i = 1; i < (pos * 2); i++)
            {
                Console.Write("\t");
            }
            Console.Write(player.Name);
        }

        private bool IsPlayerPosInRange(Player player,int iNum)
        {
            if (player.Position > iNum && player.Position <= iNum + 10)
            {
                return true;
            }
            return false;
        }

        private void DisplayNumberRightToLeft(ref int iNum)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("\t{0}", iNum);
                if (IsSnakeOrLadder(iNum))
                {
                    DisplaySnakeAndLadder(iNum, (int)Snake_and_ladder[iNum]);
                }
                iNum--;
                Console.Write("\t");
            }

            foreach (Player player in Players)
            {
                if (IsPlayerPosInRange(player, iNum))
                {
                    DisplayPlayerRight_Left(player);
                }
            }

            iNum = iNum - 9;
        }

        private void DisplayNumberLeftToRight(ref int iNum)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("\t{0}", iNum);
                if (IsSnakeOrLadder(iNum))
                {
                    DisplaySnakeAndLadder(iNum, (int)Snake_and_ladder[iNum]);
                }

                Console.Write("\t");
                iNum++;
            }

            iNum = iNum - 11;

            foreach (Player player in Players)
            {
                if (IsPlayerPosInRange(player, iNum))
                {
                    DisplayPlayerLeft_Right(player);
                }
            }
        }

        public void GenerateBoard()
        {
            int iNum = 100;
            Console.WriteLine("\n\n\t\t\t\t\t\t--------------------------Snake & Ladder Game--------------------------\n\n\n\n");

            while (iNum >= 1)
            {
                DisplayNumberRightToLeft(ref iNum);
                Console.Write("\n\n\n");
                
                DisplayNumberLeftToRight(ref iNum);
                Console.Write("\n\n\n");
            }

            Console.WriteLine("\n\n\t\t\t\t\t\t------------------------------------------------------------------------\n\n\n\n");
          
            IsPlayerOnSnakeOrLadder();
            
        }

        private void IsPlayerOnSnakeOrLadder()
        {
            foreach (Player p in Players)
            {
                if (IsSnakeOrLadder(p.Position))
                {
                    p.Position = (int)Snake_and_ladder[p.Position];
                    GenerateBoard();
                }
            }
            
        }

        public bool IsPlayerWon()
        {
            foreach (Player p in Players)
            {
                if (p.Position == 100)
                {
                    Console.WriteLine(p.Name +" Won............... !");
                    return true;
                }
            }
            return false;
        }

        public void InitializeGame()
        {
            Console.WriteLine("\nInitializing the game\n");

            for (int i = 0; i < 40; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("\n Now game begins !\n");
        }

        public void AddPlayer()
        {
            Console.WriteLine("Enter Player Name");

            Player player = new Player();
            player.SetName(Console.ReadLine());

            Players.Add(player);

            Console.WriteLine("\n Player Added !\n");
        }

        public void PlayGame()
        {

            try
            {
                char choice = 'a';

                while (!IsPlayerWon())
                {
                    foreach (Player player in Players)
                    {
                        Console.WriteLine(player.Name + " -> Press R For Rolling The Dice");
                        Console.WriteLine(player.Name + " -> Press Q For Quit \n");
                        choice = Char.ToLower(Convert.ToChar(Console.ReadLine()));
                        int dice = 0;
                        switch (choice)
                        {
                            case 'r':
                                dice = Dice.RollDice();
                                player.SetPosition(dice);

                                break;
                            case 'q':
                                Console.WriteLine("Game Quit");
                                System.Environment.Exit(0);
                                break;

                        }
                        GenerateBoard();
                        Console.WriteLine(player.Name + " Dice number -> {0} \n", dice);
                        Console.WriteLine(player.Name + " Shifted To Position {0} \n", player.Position);
                        if (IsPlayerWon())
                        {
                            return;
                        }

                        if (dice == 6)
                        {
                            Console.WriteLine("One More Chance To Player {0} ", player.Name);
                            Console.WriteLine("Press r");
                            Console.ReadLine();
                            dice = Dice.RollDice();
                            player.SetPosition(dice);
                            GenerateBoard();
                            Console.WriteLine(player.Name + " Dice number -> {0} \n", dice);
                            Console.WriteLine(player.Name + " Shifted To Position {0} \n", player.Position);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
