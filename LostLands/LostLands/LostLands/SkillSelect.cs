using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostLands
{
    class SkillSelect : OurGamesDarwableComponent
    {
        Rectangle bounds, picBounds;
        int x, y, width, height;
        String text;
        bool active, released, hidden;
        Texture2D activePic, deActivePic, current;

        MouseState old;
        KeyboardState oldK;

        public SkillSelect(Game game, int x2, int y2, int width2, int height2, String text2) : base(game)
        {
            x = x2;
            y = y2;
            width = width2;
            height = height2;
            bounds = new Rectangle(x, y, width, height);
            picBounds = new Rectangle(x-10, y-3, width, height);
            text = text2;
            hidden = false;
            activePic = Content.Load<Texture2D>(@"buttons\SkillSelect(2)");
            deActivePic = Content.Load<Texture2D>(@"buttons\SkillSelect");
            current = deActivePic;
        }

        ///Change State dependent on mouse
        private void checkState()
        {
            
            if (Mouse.GetState().LeftButton == ButtonState.Released || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (bounds.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (old.LeftButton == ButtonState.Pressed || oldK.IsKeyDown(Keys.Enter))
                    {
                        released = true;
                        active = true;
                        current = activePic;
                    }
                    else
                        released = false;
                }
                else
                    released = false;
            }else
                released = false;



            old = Mouse.GetState();
            oldK = Keyboard.GetState();
        }

        /// <summary>
        /// Will stop the button from drawing
        /// </summary>
        public void hide()
        {
            hidden = true;
        }

        public void show()
        {
            hidden = false;
        }

        /// <summary>
        /// Set the button to deActive varibales
        /// </summary>
        public void deActivate()
        {
            active = false;
            current = deActivePic;
        }

        public void activate()
        {
            active = true;
            current = activePic;
        }

        public bool isActive()
        {
            return active;
        }

        /// <summary>
        /// Returns if the button has been clicked
        /// </summary>
        /// <returns></returns>
        public bool isReleased()
        {
            return released;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!hidden)
            {
                checkState();
                spriteBatch.Draw(current, picBounds, Color.White);

                if (active)
                {
                    spriteBatch.DrawString(Content.Load<SpriteFont>("PCFont"), text, new Vector2(x, y), Color.DodgerBlue);
                }
                else
                    spriteBatch.DrawString(Content.Load<SpriteFont>("PCFont"), text, new Vector2(x, y), Color.Navy);
            }
            base.Draw(gameTime);
        }
    }
}
