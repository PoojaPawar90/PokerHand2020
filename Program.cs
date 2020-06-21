using System;
using System.IO;

namespace HandPoker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string Filepath = System.AppDomain.CurrentDomain.BaseDirectory + "PlayersInput.txt";
                string[] lines = System.IO.File.ReadAllLines(Filepath);
                int player1sHandsCount = 0, player2sHandsCount = 0;
                
                foreach (string line in lines)
                {
                    //Console.WriteLine(line);
                    Player player1 = new Player(line.Substring(0, 14));
                    Player player2 = new Player(line.Substring(15));
                    
                    switch (player1.compareTo(player2))
                    {
                        case 1:
                            
                            player1sHandsCount++;
                            break;
                        case -1:
                            
                            player2sHandsCount++;
                            break;
                        case 0:
                            Console.WriteLine("Tie.");
                            break;
                    }
                }
                Console.WriteLine("Output of provided test file");
                Console.WriteLine("Player 1 :" + player1sHandsCount);
                Console.WriteLine("Player 2 :" + player2sHandsCount);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred: \n {ex}");
                Console.ReadLine();
            }
        }
    }
}
