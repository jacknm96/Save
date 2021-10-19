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
        Texture2D barrelTexture;
        Texture2D bulletTexture;
        Texture2D powerUpTexture;

        Image tank;
        Texture2D tankTexture;
        MathClasses.Vector3 tankPosition;
        Vector2 origin; // sets origin of the sprite to rotate around
        float tankRotation;
        float tankWidth;
        float tankHeight;
        Rectangle sourceRec; //determines how much of sprite to use in draw
        Rectangle destRec; // determines where the sprite will be drawn
        #endregion

        Turret turret;
        static int powerUpCount = 20;
        PowerUp[] powerUps = new PowerUp[powerUpCount];

        bool turretDetached;

        float m_timer = 0;

        Camera2D camera;

        float tankSpeed = 500f;
        float tankRotateSpeed = 250f;
        float turretRotateSpeed = 250f;

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
            barrelTexture = LoadTextureFromImage(LoadImage("../Images/barrelGreen.png"));
            bulletTexture = LoadTextureFromImage(LoadImage("../Images/bulletYellow.png"));
            powerUpTexture = LoadTextureFromImage(LoadImage("../Images/barrelGreen_up.png"));

            // load textures for tank
            tank = LoadImage("../Images/tankBlack.png");
            tankTexture = LoadTextureFromImage(tank);
            tankWidth = tank.width;
            tankHeight = tank.height;
            sourceRec = new Rectangle(0f, 0f, tankWidth, tankHeight); // draw full sprite
            tankPosition = new MathClasses.Vector3(GetScreenWidth() / 2 - tankWidth, GetScreenHeight() / 2 - tankHeight, 0);
            origin = new Vector2(tankWidth / 2, tankHeight / 2); // want to rotate around center of sprite

            destRec = new Rectangle(tankPosition.x + tankWidth, tankPosition.y + tankHeight, tankWidth, tankHeight); // where we want to draw the sprite

            turret = new Turret(tankPosition.x, tankPosition.y, tankWidth, tankHeight, tankRotation, barrelTexture, bulletTexture);

            for (int i = 0; i < powerUpCount; i++) // spawn powerups
            {
                powerUps[i] = new PowerUp(GetScreenWidth() * 3, GetScreenHeight() * 3, powerUpTexture); // spawn in random location
            }

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
            UpdateTime();
            if (IsKeyPressed(KeyboardKey.KEY_P)) // detach/retach turret
            {
                turretDetached = !turretDetached;
                if (!turretDetached) // if reattaching turret, snap back to tank position
                {
                    turret.SetTurretPosition(tankPosition.x, tankPosition.y, tankWidth, tankHeight);
                }
            }

            Move();

            if (turretDetached) // if detached, move turret independently
                MoveTurret();

            FindPowerUps();

            if (IsKeyPressed(KeyboardKey.KEY_SPACE) || IsKeyDown(KeyboardKey.KEY_RIGHT_ALT)) // space = single shoot, alt = rapid fire
            {
                turret.ShootBullet(tankWidth, tankHeight);
            }
            turret.AdjustBullets(deltaTime, camera.target.X, camera.target.Y, camera.zoom);
            m_timer += deltaTime;

            CameraControl();
        }

        // calculates deltatime and frame data
        void UpdateTime()
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
        }

        // moves our tank
        void Move()
        {
            float xDirection = tankSpeed * deltaTime * MathF.Sin(tankRotation * MathF.PI / 180f);
            float yDirection = tankSpeed * deltaTime * MathF.Cos(tankRotation * MathF.PI / 180f);
            if (IsKeyDown(KeyboardKey.KEY_W)) // move forward
            {
                tankPosition.x -= xDirection;
                destRec.x -= xDirection;
                if (tankPosition.x < camera.target.X - 10 || tankPosition.x > camera.target.X + GetScreenWidth() / camera.zoom - tankHeight - 60)
                // if tank is about to move offscreen, camera follows tank
                {
                    camera.target.X -= xDirection;
                }
                tankPosition.y += yDirection;
                destRec.y += yDirection;
                if (tankPosition.y < camera.target.Y - 10 || tankPosition.y > camera.target.Y + GetScreenHeight() / camera.zoom - tankHeight - 60)
                // if tank is about to move offscreen, camera follows tank
                {
                    camera.target.Y += yDirection;
                }

                // move barrels
                if (!turretDetached)
                    turret.MoveTurret(-xDirection, yDirection);

                // if tank offscreen and start moving, recenters camera on tank
                if (camera.target.X > tankPosition.x + tankWidth || camera.target.X < tankPosition.x - GetScreenWidth() / camera.zoom
                    || camera.target.Y > tankPosition.y + tankHeight || camera.target.Y < tankPosition.y - GetScreenHeight() / camera.zoom)
                {
                    camera.target.X = tankPosition.x - (GetScreenWidth() / camera.zoom) / 2 + tankWidth;
                    camera.target.Y = tankPosition.y - (GetScreenHeight() / camera.zoom) / 2 + tankHeight;
                }
            }

            if (IsKeyDown(KeyboardKey.KEY_S)) // move backward
            {
                tankPosition.x += xDirection;
                destRec.x += xDirection;
                if (tankPosition.x < camera.target.X - 10 || tankPosition.x > camera.target.X + GetScreenWidth() / camera.zoom - tankHeight - 60)
                // if tank is about to move offscreen, camera follows tank
                {
                    camera.target.X += xDirection;
                }
                tankPosition.y -= yDirection;
                destRec.y -= yDirection;
                if (tankPosition.y < camera.target.Y - 10 || tankPosition.y > camera.target.Y + GetScreenHeight() / camera.zoom - tankHeight - 60)
                // if tank is about to move offscreen, camera follows tank
                {
                    camera.target.Y -= yDirection;
                }

                // move barrels
                if (!turretDetached)
                    turret.MoveTurret(xDirection, -yDirection);

                // if tank offscreen and start moving, recenters camera on tank
                if (camera.target.X > tankPosition.x + tankWidth || camera.target.X < tankPosition.x - GetScreenWidth() / camera.zoom
                    || camera.target.Y > tankPosition.y + tankHeight || camera.target.Y < tankPosition.y - GetScreenHeight() / camera.zoom)
                {
                    camera.target.X = tankPosition.x - (GetScreenWidth() / camera.zoom) / 2 + tankWidth;
                    camera.target.Y = tankPosition.y - (GetScreenHeight() / camera.zoom) / 2 + tankHeight;
                }
            }

            if (IsKeyDown(KeyboardKey.KEY_A)) // rotate tank left. if turret attached, rotate turret too
            { 
                tankRotation -= tankRotateSpeed * deltaTime;
                if (!turretDetached)
                    turret.RotateBarrels(-tankRotateSpeed * deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_D)) // rotate tank right. if turret attached, rotate turret too
            { 
                tankRotation += tankRotateSpeed * deltaTime;
                if (!turretDetached)
                    turret.RotateBarrels(tankRotateSpeed * deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_Q) || IsKeyDown(KeyboardKey.KEY_J)) // rotate barrels left
                turret.RotateBarrels(-(turretRotateSpeed * deltaTime));

            if (IsKeyDown(KeyboardKey.KEY_E) || IsKeyDown(KeyboardKey.KEY_L)) // rotate barrels right
                turret.RotateBarrels(turretRotateSpeed * deltaTime);
        }

        // checks to see if we pick up any powerups
        void FindPowerUps()
        {
            for(int i = 0; i < powerUpCount; i++) //iterate through powerups
            {
                if (powerUps[i] != null)
                {
                    Vector2 location = powerUps[i].GetPosition();
                    if (MathF.Abs(location.X - tankPosition.x) < tankHeight && MathF.Abs(location.Y - tankPosition.y) < tankHeight) //checks to see if our tank is next to a powerup
                    {
                        powerUps[i] = null; // delete powerup
                        turret.AddBarrel(tankWidth, tankHeight);
                    }
                }
            }
        }

        // if turret detached, I and K keys move turret
        void MoveTurret()
        {
            float xDirection = tankSpeed * deltaTime * MathF.Sin(turret.GetRotation() * MathF.PI / 180f);
            float yDirection = tankSpeed * deltaTime * MathF.Cos(turret.GetRotation() * MathF.PI / 180f);
            if (IsKeyDown(KeyboardKey.KEY_I))
            {
                turret.MoveTurret(-xDirection, yDirection);
            }
            if (IsKeyDown(KeyboardKey.KEY_K))
            {
                turret.MoveTurret(xDirection, -yDirection);
            }
        }

        void CameraControl()
        {
            // use arrow keys to move camera
            if (IsKeyDown(KeyboardKey.KEY_UP))
                camera.target.Y -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_DOWN))
                camera.target.Y += 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_LEFT))
                camera.target.X -= 500.0f * deltaTime;

            if (IsKeyDown(KeyboardKey.KEY_RIGHT))
                camera.target.X += 500.0f * deltaTime;

            // Camera zoom controls
            camera.zoom += GetMouseWheelMove() * 0.05f;

            if (camera.zoom > 3.0f) camera.zoom = 3.0f;
            else if (camera.zoom < 0.1f) camera.zoom = 0.1f;

            // Camera reset (zoom and rotation)
            if (IsKeyPressed(KeyboardKey.KEY_R))
            {
                camera.zoom = 1.0f;
                camera.rotation = 0.0f;
            }
        }

        // draw new frame
        public void Draw()
        {
            BeginDrawing();
            BeginMode2D(camera);

            ClearBackground(Color.ORANGE);
            DrawTexturePro(tankTexture, sourceRec, destRec, origin, tankRotation, Color.WHITE);

            foreach(Barrel barrel in turret.GetChildren()) // draw each barrel
            {
                DrawTexturePro(barrel.GetTexture(), barrel.GetSourceRectangle(), barrel.GetDestRectangle(), barrel.GetOrigin(), barrel.GetRotation(), Color.WHITE);
            }

            foreach (Bullet bullet in turret.GetBullets()) // draw each bullet
            {
                bullet.DrawBullet();
            }

            for (int i = 0; i < powerUpCount; i++) // draw powerups
            {
                if (powerUps[i] != null)
                    DrawTextureEx(powerUps[i].GetTexture(), powerUps[i].GetPosition(), 0, 1, Color.WHITE);
            }

            EndMode2D();

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            EndDrawing();
        }

    }
}
