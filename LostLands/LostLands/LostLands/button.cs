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

///
/* Austin Yount
 * 480-217-8301
 * ayountcse@yahoo.com
 * ----------------------------------------------
 * This is a resusble button class
 * takes in three pictures and an x and y
 * switches the pictures based on mouse location
 * and will return if the button has been clicked
 * ----------------------------------------------
 */
///

namespace LostLands
{
    class button : ourDarawableGameComponent
    {
        public short state;
        bool released;

        Texture2D sleep, pressed;
        public Rectangle buttonBounds;

        MouseState old;
        KeyboardState oldK;

        // Draws a button based on state returns state
        public button(short x, short y, String sleep, String pressed, Game game):base(game)
        {

            this.sleep = Content.Load<Texture2D>(@"buttons\"+sleep);
            this.pressed = Content.Load<Texture2D>(@"buttons\" + pressed);

            buttonBounds = new Rectangle(x, y, this.sleep.Bounds.Width, this.sleep.Bounds.Height);

            state = 0;// set initial state to sleep

            draw();
        }

        ///Return the value of the pressed state. Used outside of class for easy coding
        public short pressedValue()
        {
            return 2;
        }

        public void draw()
        {
            checkState();
        }

        ///Change State dependent on mouse
        public void checkState()
        {

            if (Mouse.GetState().LeftButton == ButtonState.Released || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (buttonBounds.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (old.LeftButton == ButtonState.Pressed || oldK.IsKeyDown(Keys.Enter))
                    {
                        released = true;
                        state = 2;
                    }
                    else
                    {
                        released = false;
                        state = 0;
                    }
                }
                else
                {
                    released = false;
                    state = 0;
                }
            }
            else if (buttonBounds.Contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                state = 2;
            }
            else
                state = 0;

            old = Mouse.GetState();
            oldK = Keyboard.GetState();
        }

        public Texture2D getState()
        {
            checkState();
            switch (state)
            {
                case 0:
                    return sleep;
                case 2:
                    return pressed;
                default:
                    return sleep;
            }
        }

        public bool isReleased()
        {
            return released;
        }

        public override void Update(GameTime gameTime)
        {
            getState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Draw(getState(), buttonBounds, Color.White);
            
            base.Draw(gameTime);
        }
    }
}