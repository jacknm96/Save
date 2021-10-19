using System;

namespace MathClasses
{
    public struct Colour
    {
        public UInt32 colour;

        public Colour(byte r, byte g, byte b, byte a)
        {
            colour = 0;
            red = r;
            green = g;
            blue = b;
            alpha = a;
        }

        // red value
        public byte red
        {
            get
            {
                return (byte)((colour >> 24) & 0xff);
            }
            set
            {
                colour = (colour & 0x00ffffff) | ((UInt32)value << 24);
            }
        }

        // green value
        public byte green
        {
            get
            {
                return (byte)((colour >> 16) & 0xff);
            }
            set
            {
                colour = (colour & 0xff00ffff) | ((UInt32)value << 16);
            }
        }


        // blue value
        public byte blue
        {
            get
            {
                return (byte)((colour >> 8) & 0xff);
            }
            set
            {
                colour = (colour & 0xffff00ff) | ((UInt32)value << 8);
            }
        }

        // alpha value
        public byte alpha
        {
            get
            {
                return (byte)((colour) & 0xff);
            }
            set
            {
                colour = (colour & 0xffffff00) | ((UInt32)value);
            }
        }
    }
}
