using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using MathClasses;

namespace RaylibStarterCS
{
    class Barrel
    {
        Texture2D barrelTexture;
        float barrelRotation;
        float barrelWidth;
        float barrelHeight;
        Rectangle sourceRecBarrel; //determines how much of sprite to use in draw
        Rectangle destRecBarrel; // determines where the sprite will be drawn
        Vector2 barrelOrigin; // sets origin of the sprite to rotate around

        public Barrel(float rotation, float tankPositionX, float tankPositionY, float tankWidth, float tankHeight, Texture2D texture)
        {
            barrelRotation = rotation;
            barrelTexture = texture;
            barrelWidth = 16f;
            barrelHeight = 50f;
            sourceRecBarrel = new Rectangle(0f, 0f, barrelWidth, barrelHeight); // draw full sprite
            barrelOrigin = new Vector2(barrelWidth / 2, 0); // want to rotate around end of sprite
            destRecBarrel = new Rectangle(tankPositionX + tankWidth, tankPositionY + tankHeight, barrelWidth, barrelHeight);
        }

        // moves turret in given x and y directions
        public void MoveBarrel(float xDirection, float yDirection)
        {
            destRecBarrel.x += xDirection;
            destRecBarrel.y += yDirection;
        }

        // rotates turret a given amount
        public void RotateBarrel(float rotation)
        {
            barrelRotation += rotation;
        }

        // teleports barrel to given position
        public void SetPosition(float x, float y, float tankWidth, float tankHeight)
        {
            destRecBarrel.x = x + tankWidth;
            destRecBarrel.y = y + tankHeight;
        }

        // sets the turret to a given rotation
        public void SetRotation(float rotation)
        {
            barrelRotation = rotation;
        }

        // returns the rotation of the turret
        public float GetRotation()
        {
            return barrelRotation;
        }

        // returns the length of the turret
        public float BarrelHeight()
        {
            return barrelHeight;
        }

        // returns the texture of the turret
        public Texture2D GetTexture()
        {
            return barrelTexture;
        }

        // returns the source rectangle
        public Rectangle GetSourceRectangle()
        {
            return sourceRecBarrel;
        }

        // returns the destination rectangle
        public Rectangle GetDestRectangle()
        {
            return destRecBarrel;
        }

        // returns the origin of the turret
        public Vector2 GetOrigin()
        {
            return barrelOrigin;
        }
    }
}
