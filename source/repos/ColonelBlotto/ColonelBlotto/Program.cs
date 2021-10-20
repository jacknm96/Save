using System;

namespace ColonelBlotto
{
    class Program
    {
        static Player player1;
        static Player player2;
        static int player1Troops = 20;
        static int player2Troops = 20;
        
        static void Main(string[] args)
        {
            int numFronts = 4;
            WriteRules();
            Console.WriteLine("Ready to play? (y/n)");
            while (Console.ReadLine().ToLower() == "y" && player1Troops > 0 && player2Troops > 0)
            {
                PlayRound(numFronts);
                numFronts++;
                Console.WriteLine("Play again? Number of fronts will increase. (y/n)");
            }
            if (player1Troops <= 0)
            {
                Console.WriteLine("Player 1 has no troops remaining. Player 2 wins!");
            } else if (player2Troops <= 0)
            {
                Console.WriteLine("Player 2 has no troops remaining. Player 1 wins!");
            }
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
        }

        static void PlayRound(int fronts)
        {
            player1 = new Player(player1Troops, new int[fronts]);
            player2 = new Player(player2Troops, new int[fronts]);
            int[] player1Deploy = player1.DeployTroops("Player 1", "Player 2");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine();
            }
            int[] player2Deploy = player2.DeployTroops("Player 2", "Player 1");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine();
            }
            int player1WonFronts = 0;
            int player2WonFronts = 0;
            int player1Captured = 0;
            int player2Captured = 0;
            for (int i = 0; i < fronts; i++)
            {
                if (player1Deploy[i] > player2Deploy[i])
                {
                    player1WonFronts++;
                    player1Captured += player2Deploy[i];
                } else if (player1Deploy[i] < player2Deploy[i])
                {
                    player2WonFronts++;
                    player2Captured += player1Deploy[i];
                }
            }
            Console.WriteLine();
            Console.WriteLine(player1.DisplayDeployment());
            Console.WriteLine(player2.DisplayDeployment());
            Console.WriteLine();
            if (player1WonFronts > player2WonFronts)
            {
                
                Console.WriteLine("Player 1 wins this round!");
                player1.AddTroops(2);
                player2.AddTroops(-2);
            } else if (player1WonFronts < player2WonFronts)
            {
                Console.WriteLine("Player 2 wins this round!");
                player1.AddTroops(-2);
                player2.AddTroops(2);
            } else
            {
                Console.WriteLine("It is a tie!!");
            }
            Console.WriteLine("Player 1 captured " + player1Captured.ToString() + " and lost " + player2Captured.ToString() + " troops!");
            Console.WriteLine("Player 2 captured " + player2Captured.ToString() + " and lost " + player1Captured.ToString() + " troops!");
            player1.AddTroops(player1Captured);
            player1.AddTroops(-player2Captured);
            player2.AddTroops(player2Captured);
            player2.AddTroops(-player1Captured);
            player1Troops = player1.NumTroops();
            player2Troops = player2.NumTroops();
            Console.WriteLine();
        }

        static void WriteRules()
        {
            Console.WriteLine("Welcome to Colonel Blotto!");
            Console.WriteLine("Each player will start with 20 troops. One at a time, the players will decide how many troops");
            Console.WriteLine("to deploy on each front. One player should look away while the other is deploying their troops");
            Console.WriteLine("to avoid cheating.");
            Console.WriteLine();
            Console.WriteLine("Once both players have deployed, the player who wins the most fronts wins the round. On each");
            Console.WriteLine("front, the winning player will capture the opposing player's troops (For example, on the first");
            Console.WriteLine("front, player 1 deploys 3 troops and player 2 deploys 5 troops. Player 2 wins, and takes Player");
            Console.WriteLine("1's 3 troops. On the next round, Player 2 will have 3 extra troops and Player 1 will lose 3.");
            Console.WriteLine("Rinse and repeat for the remaining fronts)");
            Console.WriteLine();
            Console.WriteLine("The winning player per round will also gain 2 troops, as they rally allies, and the loser loses");
            Console.WriteLine("2 troops as they defect.");
            Console.WriteLine();
            Console.WriteLine("You can choose to not deploy all your troops. Troops not deployed will be saved for the next round.");
            Console.WriteLine();
            Console.WriteLine("In addition, the number of fronts will increase by 1 each round, starting at 4 fronts.");
            Console.WriteLine("The game ends when a player has no troops remaining, or when you elect to stop playing.");
            Console.WriteLine();
        }
    }
}
