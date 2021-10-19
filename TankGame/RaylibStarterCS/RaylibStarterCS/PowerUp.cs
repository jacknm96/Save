using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using MathClasses;

namespace RaylibStarterCS
{
    class PowerUp
    {
        private static Random random = new Random();

        Texture2D powerUpTexture;
        Vector2 position;

        public PowerUp(float maxX, float maxY, Texture2D texture)
        {
            powerUpTexture = texture;
            position = new Vector2((float)random.NextDouble() * maxX, (float)random.NextDouble() * maxY); // randomize position in world
        }

        // returns texture of the powerup
        public Texture2D GetTexture()
        {
            return powerUpTexture;
        }

        // returns position of the powerup
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
