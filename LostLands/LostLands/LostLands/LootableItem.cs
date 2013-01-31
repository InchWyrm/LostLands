using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class LootableItem : inventorySlot
    {
        int originX, originY, life = 3000;
        public bool remove= false;

        AnimatedSprite newAS;
        bool drawHint;

        public LootableItem(Game game, int x, int y, Item item)
            : base(game, item)
        {
            originX = x;
            originY = y;
            setPos(x, y);
            showHover = false;
            newAS = new AnimatedSprite(game, Content.Load<Texture2D>("hintMouseClick"), new Vector2(x, y), 8, 8, true);
            drawHint = true;
        }

        public bool pickup()
        {
            if (mouseIsInside())
            {
                drawHint = false;
                return true;
            }
            return false;
        }

        public void update(Player player)
        {
            newAS.setPlayer(player);
            if (player.onCharItems[0] != null || player.Items.ToArray().Length > 0)
               drawHint = false;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                showHover = true;
            else
                showHover = false;
            bounds.X = originX - player.MapX;
            bounds.Y = originY - player.MapY;
            if (Math.Sqrt(Math.Pow(bounds.X - player.X, 2) + Math.Pow(bounds.Y - player.Y, 2)) > 800 || life < 0){
                remove = true;
            }
            --life;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            if (drawHint)
                newAS.Draw(gameTime);
            
            old = Mouse.GetState();
        }

    }
}
