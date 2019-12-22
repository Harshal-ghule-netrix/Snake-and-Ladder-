using System;
using System.Threading;

namespace Snake_And_Ladder
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            
            Console.WriteLine("Enter how many players");
            int TotalPlayers = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= TotalPlayers; i++)
            {
                board.AddPlayer();         
            }

            board.InitializeGame();

            board.GenerateBoard();
            board.PlayGame();
            

            


        }
    }
}
