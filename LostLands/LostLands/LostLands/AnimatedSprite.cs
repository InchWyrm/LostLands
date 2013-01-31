using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class AnimatedSprite : OurGamesDarwableComponent
    {

        Texture2D pic;
        public Rectangle source, bounds;
        public Color tint = Color.White;
        public Vector2 screenPos;
        public int hframes, widthPerFrame, vframes, timer, speed, HeightPerFrame = 48;
        public float ax, ay;
        float angle;
        Player p1;
        public bool linkedToPlayer, animationOver;
        bool animate = true;

        public AnimatedSprite(Game game, Texture2D pic, Vector2 v, ref Player p1)
            : base(game)
        {
            this.pic = pic;
            widthPerFrame = 32;
            hframes = pic.Width / widthPerFrame; // calculates horizontal amount of frames. this is made for images that have a 32 X 32 character
            vframes = pic.Height/32; // calculates how many frames vertically
            angle = 0;
            timer = speed = 10;
            ax = (int)v.X;// sets array position
            ay = (int)v.Y;
            screenPos.X = ax - p1.MapX;// sets the offset
            screenPos.Y = ay - p1.MapY;// sets the offset
            source = new Rectangle(0,0, 32, 32);// sets at first frame of character
            timer = 10;// timer for animations
            this.p1 = p1;
            linkedToPlayer = false;
        }

        public AnimatedSprite(Game game, Texture2D pic, Vector2 v, ref Player p1, int place)
            : base(game)
        {
            this.pic = pic;
            widthPerFrame = pic.Width;
            hframes = 1; // calculates horizontal amount of frames. this is made for images that have a 32 X 32 character
            vframes = 1; // calculates how many frames vertically
            angle = 0;
            timer = speed = 10;
            ax = (int)v.X;// sets array position
            ay = (int)v.Y;
            screenPos.X = ax - p1.MapX;// sets the offset
            screenPos.Y = ay - p1.MapY;// sets the offset
            source = new Rectangle(0, 0, pic.Width, pic.Height);// sets at first frame of character
            timer = 10;// timer for animations
            this.p1 = p1;
            linkedToPlayer = false;
            animationOver = true;
        }

        public AnimatedSprite(Game game, Texture2D pic, Vector2 v, int numH, int speed, Player p1, float angle)
            : base(game)
        {
            this.pic = pic;
            this.angle = angle;
            hframes = numH;
            widthPerFrame = pic.Width / hframes;
            vframes = pic.Height / 32; // calculates how many frames vertically
            timer = this.speed = speed;
            ax = (int)v.X;// sets array position
            ay = (int)v.Y;
            screenPos.X = ax;
            screenPos.Y = ay;
            source = new Rectangle(0, 0, widthPerFrame, pic.Height);// sets at first frame of character
            timer = 10;// timer for animations
            this.p1 = p1;
            linkedToPlayer = true;
            animationOver = true;
        }

        public AnimatedSprite(Game game, Texture2D pic, Vector2 v, int numH, int speed, bool mouseClickThingyThatDoesntMatterVariable)//yup
            : base(game)
        {
            this.pic = pic;
            hframes = numH;
            widthPerFrame = 64;
            HeightPerFrame = 64;
            widthPerFrame = pic.Width / hframes;
            vframes = pic.Height / HeightPerFrame; // calculates how many frames vertically
            timer = this.speed = speed;
            ax = (int)v.X;// sets array position
            ay = (int)v.Y;
            screenPos.X = ax;
            screenPos.Y = ay;
            source = new Rectangle(0, 0, widthPerFrame, pic.Height);// sets at first frame of character
            timer = 10;// timer for animations
            linkedToPlayer = false;
            animationOver = true;
        }

        public void setPlayer(Player p1) { this.p1 = p1; }

        public void setRotation(float rotate)
        {
            angle = rotate + 1.5f;
        }

        public void loopAnim()
        {
            if (timer == 0)//if the timer is out
            {
                source.X += widthPerFrame;// proced to next vertical frame
                timer = speed;//reset timer
                if (source.X >= widthPerFrame * hframes)// if the frame is outside the amount of horizontal frames
                {
                    animationOver = true;
                    source.X = 0;// set back to first frame
                }
            }
            else
            {
                animationOver = false;
                --timer;// dec timer
            }
        }

        public void drawAnim()
        {
            animationOver = false;
        }

        /// <summary>
        /// reseting the objects offset to the player
        /// </summary>
        public void update()
        {
            if (!linkedToPlayer)
            {
                screenPos.X = ax - p1.MapX;
                screenPos.Y = ay - p1.MapY;
                bounds = new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)widthPerFrame, HeightPerFrame);
            }
            else
            {

                screenPos.X = p1.X + widthPerFrame / 2-10;//Offset slash
                screenPos.Y = p1.Y + HeightPerFrame / 2;
                bounds = new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)widthPerFrame, HeightPerFrame);
            }

        }

        public void stopAnimating()
        {
            animate = false;
            source.X = widthPerFrame;
        }

        public void startAnimating()
        {
            animate = true;
        }

        public override void Draw(GameTime gameTime)
        {
            update();

            if (!linkedToPlayer)
            {
                spriteBatch.Draw(pic, screenPos, source, tint, angle, new Vector2(0, 0), 1, SpriteEffects.None, .5f);
            }
            else
            {
                spriteBatch.Draw(pic, screenPos, source, tint, angle, new Vector2(widthPerFrame / 2, 35), 1, SpriteEffects.None, .5f);
                
            }
            //spriteBatch.Draw(Content.Load<Texture2D>("Box"), bounds, Color.Black);
            if(animate)
                loopAnim();

            base.Draw(gameTime);
        }

    }
}
