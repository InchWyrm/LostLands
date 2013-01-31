using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class OurGamesDarwableComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected ContentManager Content;

        protected Game game;

        public OurGamesDarwableComponent(Game game)
            : base(game)
        {
            this.game = game;

            spriteBatch =
                (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            Content =
                (ContentManager)Game.Services.GetService(typeof(ContentManager));
        }

        public SpriteBatch spriteBatch { get; set; }
    }
}
