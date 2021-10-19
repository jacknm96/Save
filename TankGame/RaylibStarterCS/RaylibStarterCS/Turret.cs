using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using MathClasses;

namespace RaylibStarterCS
{
    class Turret
    {
        List<Barrel> barrels = new List<Barrel>();
        Vector2 position;
        List<Bullet> bullets = new List<Bullet>();
        int numBarrels;
        float turretRotation;
        Texture2D barrelTexture;
        Texture2D bulletTexture;

        // initialize turret based on tank position, also needs barrel and bullet textures
        public Turret(float tankPositionX, float tankPositionY, float tankWidth, float tankHeight, float tankRotation, Texture2D barrel, Texture2D bullet)
        {
            position = new Vector2(tankPositionX, tankPositionY);
            barrelTexture = barrel;
            bulletTexture = bullet;
            barrels.Add(new Barrel(tankRotation, tankPositionX, tankPositionY, tankWidth, tankHeight, barrelTexture)); // initialize starting barrel
            numBarrels++;
        }

        // rotate barrels by given amount
        public void RotateBarrels(float rotation)
        {
            turretRotation += rotation;
            foreach(Barrel barrel in barrels)
            {
                barrel.RotateBarrel(rotation);
            }
        }

        // spawn new barrel. reposition all barrels to be evenly spaced around turret
        public void AddBarrel(float tankWidth, float tankHeight)
        {
            barrels.Add(new Barrel(0, position.X, position.Y, tankWidth, tankHeight, barrelTexture)); // add new barrel
            numBarrels++;
            for (int j = 0; j < numBarrels; j++) //rotate barrels to be evenly spaced
            {
                barrels[j].SetRotation((360 * j / numBarrels) + turretRotation);
            }
        }

        // move turret, and barrels, in given direction
        public void MoveTurret(float xDirection, float yDirection)
        {
            position.X += xDirection;
            position.Y += yDirection;
            foreach (Barrel barrel in barrels)
            {
                barrel.MoveBarrel(xDirection, yDirection);
            }
        }

        // telport turret, and barrels, to give position
        public void SetTurretPosition(float x, float y, float tankWidth, float tankHeight)
        {
            position.X = x;
            position.Y = y;
            foreach (Barrel barrel in barrels)
            {
                barrel.SetPosition(x, y, tankWidth, tankHeight);
            }
        }

        // creates a new bullet at each barrel of tank
        public void ShootBullet(float tankWidth, float tankHeight)
        {
            foreach (Barrel barrel in barrels) // fire a bullet from each barrel
            {
                bullets.Add(new Bullet(barrel.GetRotation(),
                    new Vector2(-12 + position.X + tankWidth - (barrel.BarrelHeight() + 15) * MathF.Sin(barrel.GetRotation() * MathF.PI / 180f),
                    position.Y + tankHeight + (barrel.BarrelHeight() + 15) * MathF.Cos(barrel.GetRotation() * MathF.PI / 180f)), bulletTexture));
            }
        }

        // move bullets, and delete any bulets that go beyond screen edge
        public void AdjustBullets(float deltaTime, float cameraX, float cameraY, float cameraZoom)
        {
            List<Bullet> toRemove = new List<Bullet>(); // list of bullets that we want to delete
            foreach (Bullet bullet in bullets) // update bullet positions
            {
                bullet.UpdatePosition(deltaTime);
                if (bullet.bulletLocation.X < cameraX || bullet.bulletLocation.X > cameraX + GetScreenWidth() / cameraZoom
                    || bullet.bulletLocation.Y < cameraY || bullet.bulletLocation.Y > cameraY + GetScreenHeight() / cameraZoom) // detect if beyond screen edge
                {
                    toRemove.Add(bullet);
                }
            }
            foreach (Bullet bullet in toRemove) // delete appropriate bullets
            {
                bullets.Remove(bullet);
            }
        }

        // returns all barrels
        public List<Barrel> GetChildren()
        {
            return barrels;
        }

        // returns all bullets
        public List<Bullet> GetBullets()
        {
            return bullets;
        }

        // returns current rotation of the turret
        public float GetRotation()
        {
            return turretRotation;
        }
    }
}
