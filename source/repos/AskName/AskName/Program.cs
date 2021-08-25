using System;

namespace AskName
{
    class Program
    {
        static string playerName = "";
        static string[] positiveAnswers = { "yes", "yeah", "sure", "yep", "y" };
        
        static void Main(string[] args)
        {
            Console.WriteLine("What is your name?");
            playerName = Console.ReadLine();
            Console.WriteLine("Nice to meet you, " + playerName + "!");
            Console.WriteLine("Would you like to play a game?");
            string response = Console.ReadLine().ToLower();
            bool playGame = false;
            for (int i = 0; i < positiveAnswers.Length; i++)
            {
                if (response == positiveAnswers[i])
                {
                    playGame = true;
                    PlayGame();
                    break;
                }
            }
            if (!playGame)
            {
                Console.WriteLine("Well fine then");
            }
        }

        static void PlayGame()
        {
            string x;
            PlayGameStart(out x);
            Console.WriteLine(x);
        }

        static void PlayGameStart(out string y)
        {
            y = "How exciting";
        }
    }
}
