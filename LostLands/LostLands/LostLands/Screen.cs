using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace LostLands
{
    class Screen : ourDarawableGameComponent
    {

        public bool active = true;

        public MouseState oldM;
        public KeyboardState oldK;

        public Screen(Game game)
            : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            oldM = Mouse.GetState();
            oldK = Keyboard.GetState();
        }

    }
}
