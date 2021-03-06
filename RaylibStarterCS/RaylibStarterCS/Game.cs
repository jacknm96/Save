using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace RaylibStarterCS
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        Image barrel;
        Texture2D barrelTexture;
        float barrelRotation;
        float barrelWidth;
        float barrelHeight;
        Rectangle sourceRecBarrel;
        Rectangle destRecBarrel;
        Vector2 barrelOrigin;

        Image tank;
        Texture2D tankTexture;
        Vector3 tankPosition;
        Vector2 origin;
        float tankRotation;
        float tankWidth;
        float tankHeight;
        Rectangle sourceRec;
        Rectangle destRec;

        List<Bullet> bullets = new List<Bullet>();

        float m_timer = 0;

        Camera2D camera;

        public Game()
        {
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            barrel = LoadImage("../Images/barrelGreen.png");
            barrelTexture = LoadTextureFromImage(barrel);
            barrelWidth = barrel.width;
            barrelHeight = barrel.height;

            tank = LoadImage("../Images/tankBlack.png");
            tankTexture = LoadTextureFromImage(tank);
            tankWidth = tank.width;
            tankHeight = tank.height;
            sourceRec = new Rectangle( 0f, 0f, tankWidth, tankHeight );
            sourceRecBarrel = new Rectangle(0f, 0f, barrelWidth, barrelHeight);
            tankPosition = new Vector3(GetScreenWidth() / 2 - tankWidth, GetScreenHeight() / 2 - tankHeight, 0);
            origin = new Vector2(tankWidth / 2, tankHeight / 2);
            barrelOrigin = new Vector2(barrelWidth / 2, 0);

            SetTargetFPS(60);       // Set our game to run at 60 frames-per-second

            camera.target = new Vector2(0, 0);
            camera.offset = Vector2.Zero;
            camera.rotation = 0;
            camera.zoom = 1.0f;
        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            // insert game logic here 
            if (tankPosition.X >= camera.target.X + 20 && tankPosition.X <= camera.target.X + GetScreenWidth() + 20
                    && tankPosition.Y >= camera.target.Y + 20 && tankPosition.Y <= camera.target.Y + GetScreenHeight() + 20)
            {
                if (IsKeyDown(KeyboardKey.KEY_W))
                {
                    tankPosition.X -= 500.0f * deltaTime * MathF.Sin(tankRotation * MathF.PI / 180f);
                    tankPosition.Y += 500.0f * deltaTime * MathF.Cos(tankRotation * MathF.PI / 180f);
                }

                if (IsKeyDown(KeyboardKey.KEY_S))
                {
                    tankPosition.X += 500.0f * deltaTime * MathF.Sin(tankRotation * MathF.PI / 180f);
                    tankPosition.Y -= 500.0f * deltaTime * MathF.Cos(tankRotation * MathF.PI / 180f);
                }
            }

            if (IsKeyDown(KeyboardKey.KEY_A))
                tankRotation -= 250.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_D))
                tankRotation += 250.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_Q))
                barrelRotation -= 250.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_E))
                barrelRotation += 250.0f * deltaTime;

            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                bullets.Add(new Bullet(tankRotation + barrelRotation,
                    //new Vector2(tankPosition.X + barrelHeight * MathF.Cos(tankRotation + barrelRotation), tankPosition.Y + barrelHeight * MathF.Sin(tankRotation + barrelRotation))));
                    new Vector2(-12 + tankPosition.X + tankWidth - (barrelHeight + 15) * MathF.Sin((tankRotation + barrelRotation) * MathF.PI / 180f), 
                    tankPosition.Y + tankHeight + (barrelHeight + 15) * MathF.Cos((tankRotation + barrelRotation) * MathF.PI / 180f))));
            }
            List<Bullet> toRemove = new List<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                bullet.UpdatePosition(deltaTime);
                if (bullet.bulletLocation.X < camera.target.X || bullet.bulletLocation.X > camera.target.X + GetScreenWidth() 
                    || bullet.bulletLocation.Y < camera.target.Y || bullet.bulletLocation.Y > camera.target.Y + GetScreenHeight())
                {
                    toRemove.Add(bullet);
                }
            }
            foreach (Bullet bullet in toRemove)
            {
                 bullets.Remove(bullet);
            }
            m_timer += deltaTime;

            // use arrow keys to move camera
            if (IsKeyDown(KeyboardKey.KEY_UP))
                camera.target.Y += 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_DOWN))
                camera.target.Y -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_LEFT))
                camera.target.X -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_RIGHT))
                camera.target.X += 500.0f * deltaTime;

            destRec = new Rectangle(tankPosition.X + tankWidth, tankPosition.Y + tankHeight, tankWidth, tankHeight);
            destRecBarrel = new Rectangle(tankPosition.X + tankWidth, tankPosition.Y + tankHeight, barrelWidth, barrelHeight);


            // Camera zoom controls
            camera.zoom += ((float)GetMouseWheelMove() * 0.05f);

            if (camera.zoom > 3.0f) camera.zoom = 3.0f;
            else if (camera.zoom < 0.1f) camera.zoom = 0.1f;

            // Camera reset (zoom and rotation)
            if (IsKeyPressed(KeyboardKey.KEY_R))
            {
                camera.zoom = 1.0f;
                camera.rotation = 0.0f;
            }
        }

        public void Draw()
        {
            BeginDrawing();
                BeginMode2D(camera);

                    ClearBackground(Color.WHITE);

            /*DrawTexture(logoTexture,
				GetScreenWidth() - logoTexture.width, 0, Color.WHITE);*/

            // demonstrate spinning sprite
            //DrawTextureEx(tankTexture, new Vector2(tankPosition.X, tankPosition.Y), tankRotation, 1, Color.WHITE);
            DrawTexturePro(tankTexture, sourceRec, destRec, origin, tankRotation, Color.WHITE);

            DrawTexturePro(barrelTexture, sourceRecBarrel, destRecBarrel, barrelOrigin, barrelRotation + tankRotation, Color.WHITE);

            foreach (Bullet bullet in bullets)
            {
                bullet.DrawBullet();
            }
            //DrawTextureEx(barrelTexture, new Vector2(destRec.x, destRec.y), tankRotation, 1, Color.WHITE);

            /* draw a thin line
            DrawLine(300, 300, 600, 400, Color.BLACK);

            // draw a moving purple circle
            DrawCircle((int)(Math.Sin(m_timer) * 100) + 300, 150, 50, Color.PURPLE);
            
            // draw a rotating red box
            DrawRectanglePro(new Rectangle(100, 200, 60, 20), new Vector2(30, 10), m_timer*30, Color.RED);*/

            EndMode2D();

                DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            EndDrawing();
        }

    }

    public class Bullet
    {
        Image bullet;
        public Texture2D bulletTexture;
        public float bulletRotation;
        public Vector2 bulletLocation;
        float bulletWidth;
        float bulletHeight;
        Rectangle sourceRecBullet;
        Rectangle destRecBullet;
        Vector2 bulletOrigin;

        public Bullet(float rotation, Vector2 position)
        {
            bullet = LoadImage("../Images/bulletYellow.png");
            bulletTexture = LoadTextureFromImage(bullet);
            bulletWidth = bullet.width;
            bulletHeight = bullet.height;
            sourceRecBullet = new Rectangle(0f, 0f, bulletWidth, bulletHeight);
            bulletOrigin = new Vector2(bulletWidth / 2, 0);
            bulletRotation = rotation + 180;
            bulletLocation = position;
        }

        public void UpdatePosition(float deltaTime)
        {
            bulletLocation.X -= 500.0f * deltaTime * MathF.Sin((bulletRotation - 180) * MathF.PI / 180f);
            bulletLocation.Y += 500.0f * deltaTime * MathF.Cos((bulletRotation - 180) * MathF.PI / 180f);
            destRecBullet = new Rectangle(bulletLocation.X + bulletWidth, bulletLocation.Y, bulletWidth, bulletHeight);
        }

        public void DrawBullet()
        {
            DrawTexturePro(bulletTexture, sourceRecBullet, destRecBullet, bulletOrigin, bulletRotation, Color.WHITE);
        }
    }
}
