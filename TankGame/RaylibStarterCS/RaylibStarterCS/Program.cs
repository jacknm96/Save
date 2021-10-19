using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace RaylibStarterCS
{
	class Program
	{
		static void Main(string[] args)
        {
            Game game = new Game();

            InitWindow(1640, 900, "Hello World");

            game.Init();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            game.Shutdown();

            CloseWindow();
        }
	}
}
