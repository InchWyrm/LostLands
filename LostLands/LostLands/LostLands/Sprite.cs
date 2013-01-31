using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace LostLands
{
    class Sprite
    {
        public int originX, originY, width, height;
        private Texture2D Image;

        public Sprite(int X, int Y, int w, int h, Texture2D Image)
        {
            originX = X;
            originY = Y;
            width = w;
            height = h;

            this.Image = Image;
        }

        public Rectangle bounds
        {
            get { return new Rectangle(originX, originY, width + originX, height + originY); }
        }

        public Texture2D image
        {
            get { return Image; }
            set
            {
                Image = null;
            }
        }
    }
}
