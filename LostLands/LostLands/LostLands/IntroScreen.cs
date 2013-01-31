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
    class IntroScreen : Screen
    {

        TypeText welcome;
        BackgroundImage Background;
        button newGameB, option, exitB;
        public bool Options;

        // introduction screen and player name and character setup
        public IntroScreen(Game game)
            : base(game)
        {

            Background = new BackgroundImage(Content.Load<Texture2D>(@"Backgrounds\Back"));
            welcome = new TypeText(800 / 2 - 170, 600 / 2 - 250, 20, "Lost Lands", Content.Load<SpriteFont>("MetalLord"));
            newGameB = new button(300, 250 - 35, ("newGame"), ("newGame(3)"),game);
            option = new button(300, 250 + 35, ("option"), ("option(3)"), game);
            exitB = new button(800 - 25, 600 - 25, ("exit"), ("exit(3)"), game);
        }

        public void drawButtons()
        {
            spriteBatch.Draw(newGameB.getState(), newGameB.buttonBounds, Color.White);
            spriteBatch.Draw(option.getState(), option.buttonBounds, Color.White);
        }

        public override void Draw(GameTime gameTime)
        {

            welcome.addText();

            spriteBatch.Begin();

            spriteBatch.Draw(Background.image, Background.bounds, Color.White);
            spriteBatch.DrawString(welcome.font, welcome.text, new Vector2(welcome.x, welcome.y), Color.Brown);

            if (welcome.textDone)
                drawButtons();
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                welcome.forceDone();

            if (newGameB.isReleased())
                active = false;

            if (option.isReleased())
                Options = true;

            spriteBatch.Draw(exitB.getState(), exitB.buttonBounds, Color.White);
            if (exitB.isReleased())
            {
                Game.Exit();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
