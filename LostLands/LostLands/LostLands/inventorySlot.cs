using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class inventorySlot : OurGamesDarwableComponent
    {
        public Rectangle bounds;
        public Item item;
        protected MouseState old;
        protected bool showHover;
        int xDis, yDis;
        SpriteFont itemDesc;

        public inventorySlot(Game game)
            : base(game)
        {
            showHover = true;
            itemDesc = Content.Load<SpriteFont>("ItemDesc");
        }

        public inventorySlot(Game game, Item item)
            : base(game)
        {
            this.item = item;
            showHover = true;
            itemDesc = Content.Load<SpriteFont>("ItemDesc");
        }

        public inventorySlot(Game game, Item item, int x, int y)
            : base(game)
        {
            this.item = item;
            bounds = new Rectangle(x, y, 32, 32);
            itemDesc = Content.Load<SpriteFont>("ItemDesc");
        }

        public void setPos(int x, int y)
        {
            bounds = new Rectangle(x, y, 32, 32);
        }

        public void setItem(Item item)
        {
            this.item = item;
        }

        public void showTheHover()
        {
            showHover = true;
        }

        public void hideHover()
        {
            showHover = false;
        }

        public bool getHoverState()
        {
            return showHover;
        }

        public void switchHover()
        {
            showHover = !showHover;
        }

        public bool mouseIsInside()
        {
            if (Mouse.GetState().X > bounds.X && Mouse.GetState().X < bounds.X + bounds.Width)
                if (Mouse.GetState().Y > bounds.Y && Mouse.GetState().Y < bounds.Y + bounds.Height)
                {
                    return true;
                }
            return false;
        }

        public void update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                showHover = true;
            else
                showHover = false;
        }

        public void drawHover()
        {
            if (mouseIsInside() && showHover)
            {
                if (Mouse.GetState().Y > 100)
                    yDis = -70;
                else
                    yDis = 10;

                if (Mouse.GetState().X < 50)
                    xDis = 0;
                else if (Mouse.GetState().X < 725)
                    xDis = -40;
                else
                    xDis = -100;

                
                spriteBatch.Draw(Content.Load<Texture2D>("MetalPlate"), new Rectangle(Mouse.GetState().X + xDis, Mouse.GetState().Y + yDis, 105, 70), Color.White);
                spriteBatch.DrawString(itemDesc, item.ToString(), new Vector2(Mouse.GetState().X + xDis + 10, Mouse.GetState().Y + yDis + 10), Color.Black);
            }
            update();
        }

        public void drawType()
        {
            switch (item.getType())
            {
                case 1:
                    //spriteBatch.DrawString(itemDesc, "L", new Vector2(bounds.X+1, bounds.Y), Color.White);
                    break;
                case 2:
                    //spriteBatch.DrawString(itemDesc, "R", new Vector2(bounds.X+bounds.Width-10, bounds.Y), Color.White);
                    break;
                case 3:
                    //spriteBatch.DrawString(itemDesc, "U", new Vector2(bounds.X + bounds.Width/2-4, bounds.Y), Color.White);
                    spriteBatch.DrawString(itemDesc, item.stacks + "", new Vector2(bounds.X + bounds.Width / 2 - 4, bounds.Y + bounds.Width -17), Color.White);
                    break;
            }
        }

        

        public override void Draw(GameTime gameTime)
        {
            if (item != null)
            {
                if (item.usable())
                {
                    spriteBatch.Draw(item.ItemPic, bounds, Color.White);
                }else
                    spriteBatch.Draw(item.ItemPic, bounds, Color.Gray);

                drawType();
                base.Draw(gameTime);
                old = Mouse.GetState();
            }
        }
    }
}
