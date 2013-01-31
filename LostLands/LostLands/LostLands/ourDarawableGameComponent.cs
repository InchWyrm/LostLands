using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class ourDarawableGameComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected ContentManager Content;

        protected Game game;

        public SpriteFont font1, font2, description, targetBar;

        public ourDarawableGameComponent(Game game)
            : base(game)
        {
            this.game = game;

            spriteBatch =
                (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            Content =
                (ContentManager)Game.Services.GetService(typeof(ContentManager));

            font1 = Content.Load<SpriteFont>("MetalLord");
            font2 = Content.Load<SpriteFont>("PCfont");
            description = Content.Load<SpriteFont>("Description");
            targetBar = Content.Load<SpriteFont>("targetBar");

        }

        public SpriteBatch spriteBatch { get; set; }

    }
}
