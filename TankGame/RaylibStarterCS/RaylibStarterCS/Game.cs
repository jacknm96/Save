using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using MathClasses;

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

        #region images
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
        MathClasses.Vector3 tankPosition;
        Vector2 origin;
        float tankRotation;
        float tankWidth;
        float tankHeight;
        Rectangle sourceRec;
        Rectangle destRec;
        #endregion

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
            // load textures for barrel
            barrel = LoadImage("../Images/barrelGreen.png");
            barrelTexture = LoadTextureFromImage(barrel);
            barrelWidth = barrel.width;
            barrelHeight = barrel.height;
            sourceRecBarrel = new Rectangle(0f, 0f, barrelWidth, barrelHeight); // draw full sprite
            barrelOrigin = new Vector2(barrelWidth / 2, 0); // want to rotate around end of sprite

            // load textures for tank
            tank = LoadImage("../Images/tankBlack.png");
            tankTexture = LoadTextureFromImage(tank);
            tankWidth = tank.width;
            tankHeight = tank.height;
            sourceRec = new Rectangle(0f, 0f, tankWidth, tankHeight); // draw full sprite
            tankPosition = new MathClasses.Vector3(GetScreenWidth() / 2 - tankWidth, GetScreenHeight() / 2 - tankHeight, 0);
            origin = new Vector2(tankWidth / 2, tankHeight / 2); // want to rotate around center of sprite

            destRec = new Rectangle(tankPosition.x + tankWidth, tankPosition.y + tankHeight, tankWidth, tankHeight);
            destRecBarrel = new Rectangle(tankPosition.x + tankWidth, tankPosition.y + tankHeight, barrelWidth, barrelHeight);

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
            Move();

            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                ShootBullet();
            }
            AdjustBullets();
            m_timer += deltaTime;

            CameraControl();
        }

        void Move()
        {
            float xDirection = 500.0f * deltaTime * MathF.Sin(tankRotation * MathF.PI / 180f);
            float yDirection = 500.0f * deltaTime * MathF.Cos(tankRotation * MathF.PI / 180f);
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                tankPosition.x -= xDirection;
                destRec.x -= xDirection;
                destRecBarrel.x -= xDirection;
                if (tankPosition.x < camera.target.X - 10 || tankPosition.x > camera.target.X + GetScreenWidth() - tankHeight - 60)
                {
                    camera.target.X -= xDirection;
                }
                tankPosition.y += yDirection;
                destRec.y += yDirection;
                destRecBarrel.y += yDirection;
                if (tankPosition.y < camera.target.Y - 10 || tankPosition.y > camera.target.Y + GetScreenHeight() - tankHeight - 60)
                {
                    camera.target.Y += yDirection;
                }
            }

            if (IsKeyDown(KeyboardKey.KEY_S))
            {
                tankPosition.x += xDirection;
                destRec.x += xDirection;
                destRecBarrel.x += xDirection;
                if (tankPosition.x < camera.target.X - 10 || tankPosition.x > camera.target.X + GetScreenWidth() - tankHeight - 60)
                {
                    camera.target.X += xDirection;
                }
                tankPosition.y -= yDirection;
                destRec.y -= yDirection;
                destRecBarrel.y -= yDirection;
                if (tankPosition.y < camera.target.Y - 10 || tankPosition.y > camera.target.Y + GetScreenHeight() - tankHeight - 60)
                {
                    camera.target.Y -= yDirection;
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
        }

        void ShootBullet()
        {
            bullets.Add(new Bullet(tankRotation + barrelRotation,
                    new Vector2(-12 + tankPosition.x + tankWidth - (barrelHeight + 15) * MathF.Sin((tankRotation + barrelRotation) * MathF.PI / 180f),
                    tankPosition.y + tankHeight + (barrelHeight + 15) * MathF.Cos((tankRotation + barrelRotation) * MathF.PI / 180f))));
        }

        void AdjustBullets()
        {
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
        }

        void CameraControl()
        {
            // use arrow keys to move camera
            if (IsKeyDown(KeyboardKey.KEY_UP))
                camera.target.Y += 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_DOWN))
                camera.target.Y -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_LEFT))
                camera.target.X -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_RIGHT))
                camera.target.X += 500.0f * deltaTime;

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
            DrawTexturePro(tankTexture, sourceRec, destRec, origin, tankRotation, Color.WHITE);

            DrawTexturePro(barrelTexture, sourceRecBarrel, destRecBarrel, barrelOrigin, barrelRotation + tankRotation, Color.WHITE);

            foreach (Bullet bullet in bullets)
            {
                bullet.DrawBullet();
            }

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
