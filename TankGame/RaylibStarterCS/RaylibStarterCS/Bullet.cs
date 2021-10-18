using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace RaylibStarterCS
{
    public class Bullet
    {
        Image bullet;
        public Texture2D bulletTexture;
        public float bulletRotation;
        public Vector2 bulletLocation;
        float bulletWidth;
        float bulletHeight;
        Rectangle sourceRecBullet; //determines how much of sprite to use in draw
        Rectangle destRecBullet; // determines where the sprite will be drawn
        Vector2 bulletOrigin; // sets origin of the sprite to rotate around
        float bulletSpeed = 500f;

        public Bullet(float rotation, Vector2 position)
        {
            bullet = LoadImage("../Images/bulletYellow.png");
            bulletTexture = LoadTextureFromImage(bullet);
            bulletWidth = bullet.width;
            bulletHeight = bullet.height;
            sourceRecBullet = new Rectangle(0f, 0f, bulletWidth, bulletHeight);
            bulletOrigin = new Vector2(bulletWidth / 2, 0);
            bulletRotation = rotation + 180; //flip sprite around, otherwise bullet is pointing at tank
            bulletLocation = position;
        }

        // move bullet along trajectory
        public void UpdatePosition(float deltaTime)
        {
            bulletLocation.X -= bulletSpeed * deltaTime * MathF.Sin((bulletRotation - 180) * MathF.PI / 180f);
            bulletLocation.Y += bulletSpeed * deltaTime * MathF.Cos((bulletRotation - 180) * MathF.PI / 180f);
            destRecBullet = new Rectangle(bulletLocation.X + bulletWidth, bulletLocation.Y, bulletWidth, bulletHeight);
        }

        public void DrawBullet()
        {
            DrawTexturePro(bulletTexture, sourceRecBullet, destRecBullet, bulletOrigin, bulletRotation, Color.WHITE);
        }
    }
}
