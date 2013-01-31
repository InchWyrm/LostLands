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
    class BackgroundImage : Sprite
    {
        private Texture2D Image;

        public BackgroundImage(Texture2D Image)
            : base(0, 0, 800, 600, Image)
        {
            originX = 0;
            originY = 0;
            width = 800;
            height = 600;

            this.Image = Image;
        }
    }
}