using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LostLands
{
    class Screen : OurGamesDarwableComponent
    {
        
        public bool active= true;

        protected MouseState old;

        public SpriteFont font1, font2, description, targetBar;

        public Screen(Game game)
            : base(game)
        {
            font1 = Content.Load<SpriteFont>("MetalLord");
            font2 = Content.Load<SpriteFont>("PCfont");
            description = Content.Load<SpriteFont>("Description");
            targetBar = Content.Load<SpriteFont>("targetBar");
        }

    }
}