using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace LostLands
{
    class inventory : Screen
    {
        KeyboardState oldk;
        Texture2D iBackGround;
        Texture2D pBox, playerArea;
        Texture2D iBox;
        Player player;
        int slotStartX = 11 * 32, slotStartY = 32;
        button exit, help;

        public bool hidenBackpack = true;

        public inventory(Game game, ref Player p1)
            : base(game)
        {
            iBackGround = Content.Load<Texture2D>(@"pBackGround");
            iBox = Content.Load<Texture2D>("invSlot");
            pBox = Content.Load<Texture2D>("EquippedSlot");
            playerArea = Content.Load<Texture2D>("playerEquipArea");
            exit = new button(800 - 129, 0, Content.Load<Texture2D>(@"buttons\UIExit"), Content.Load<Texture2D>(@"buttons\UIExit(2)"), Content.Load<Texture2D>(@"buttons\UIExit(3)"));
            help = new button(800 - 129 * 2, 0, Content.Load<Texture2D>(@"buttons\help"), Content.Load<Texture2D>(@"buttons\help(2)"), Content.Load<Texture2D>(@"buttons\help(2)"));
            player = p1;
        }

        public void addItemToPlayer(inventorySlot itemSlot)
        {
            inventorySlot temp = new inventorySlot(game, itemSlot.item);
            if (itemSlot.item.getType() == 1)
            {
                if (player.onCharItems[0] != null)
                {
                    itemSlot.setItem(player.onCharItems[0].item);
                    player.onCharItems[0].setItem(temp.item);
                }
                else
                {
                    player.Items.Remove(itemSlot);
                    player.onCharItems[0] = new inventorySlot(game, temp.item);
                }

            }

            if (itemSlot.item.getType() == 2)
            {
                if (player.onCharItems[1] != null)
                {
                    itemSlot.setItem(player.onCharItems[1].item);
                    player.onCharItems[1].setItem(temp.item);
                }
                else
                {
                    player.Items.Remove(itemSlot);
                    player.onCharItems[1] = new inventorySlot(game, temp.item);
                }
            }
        }

        public void addItemToPlayerBelt(inventorySlot itemSlot)
        {
            inventorySlot temp = new inventorySlot(game, itemSlot.item);

            if (player.onCharItems[2] == null)
            {
                player.Items.Remove(itemSlot);
                player.onCharItems[2] = new inventorySlot(game, temp.item);
            }
            else if (player.onCharItems[3] == null)
            {
                player.Items.Remove(itemSlot);
                player.onCharItems[3] = new inventorySlot(game, temp.item);
            }
            else if (player.onCharItems[4] == null)
            {
                player.Items.Remove(itemSlot);
                player.onCharItems[4] = new inventorySlot(game, temp.item);
            }
            else
            {
                itemSlot.setItem(player.onCharItems[4].item);
                player.onCharItems[4].setItem(temp.item);
            }
        }

        public void sortInv()
        {
            
            foreach (inventorySlot invSlot in player.Items)
                invSlot.setPos(player.Items.IndexOf(invSlot) * 34 + slotStartX - (player.Items.IndexOf(invSlot) / 13) * 442, (player.Items.IndexOf(invSlot) / 13) * 34 + slotStartY);

            foreach (inventorySlot invSlot in player.Items)
            {
                if (invSlot.item.type == 3 && invSlot.item.stacks < 5)
                {
                    foreach (inventorySlot usables in player.Items)
                    {
                        if (usables.item.getType() == 3 && usables.item.stacks < 5 && usables.item.getID() == invSlot.item.getID() && usables.item.potionType == invSlot.item.potionType && usables != invSlot)
                        {
                            if (usables.item.stacks > invSlot.item.stacks)
                            {
                                usables.item.addStack(1);
                                --invSlot.item.stacks;
                            }
                            else
                            {
                                invSlot.item.addStack(1);
                                --usables.item.stacks;
                            }
                            if (usables.item.stacks <= 0)
                            {
                                player.Items.Remove(usables);
                                return;
                            }
                            else if (invSlot.item.stacks <= 0)
                            {
                                player.Items.Remove(invSlot);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void removeFromPlayer(int slot)
        {
            player.onCharItems[slot].item.makeUsable();
            addItem(player.onCharItems[slot].item);
            player.onCharItems[slot] = null;
        }

        public void addItem(Item item)
        {
            player.Items.Add(new inventorySlot(game, item, player.Items.Count * 34 + slotStartX, (player.Items.Count / 13) * 34 + slotStartY));
        }

        public void displayStats()
        {
            spriteBatch.Draw(playerArea, new Rectangle(11 * 32, 9*34, 13*34 - 2, 8*34 - 2), Color.White);
            spriteBatch.DrawString(description, "Melee Damage: " + player.meleeDamage(), new Vector2(360, 306), Color.White);
            spriteBatch.DrawString(description, "Defense Rating: " + player.defenseRating(), new Vector2(360, 326), Color.White);
            spriteBatch.DrawString(description, "Magic Damage: " + player.getWis(), new Vector2(360, 346), Color.White);
        }

        public void showHelp()
        {
            if (help.state == 1 || help.state == 2)
            {
                spriteBatch.Draw(playerArea, new Rectangle(100, 50, 600, 150), Color.White);

                spriteBatch.DrawString(description, "Left click to equip item to player\nLeft click to unequip items\nOnly Left click and right click type items can be equipped to player\nRight click to put item in belt\nAny item can be put in belt", new Vector2(110, 60), Color.White);

            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.B) && oldk.IsKeyDown(Keys.B) || exit.isReleased())
            {
                hidenBackpack = !hidenBackpack;
                Game.IsMouseVisible = true;
                foreach (inventorySlot playerSlot in player.onCharItems)
                {
                    if(playerSlot != null)
                        playerSlot.hideHover();
                }
            }
            sortInv();
            spriteBatch.Draw(iBackGround, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(playerArea, new Rectangle(0, 32, 320, 544), Color.White);

            spriteBatch.Draw(Content.Load<Texture2D>(@"CharPics\"+player.getClass()+"(2)"), new Rectangle(75, 75, 200, 300), Color.White);

            //spriteBatch.DrawString(description, "Head", new Vector2(125, 75), Color.White);
            //spriteBatch.Draw(pBox, new Vector2(125,100), Color.White);
            spriteBatch.DrawString(description, player.getName(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(description, "Right Hand", new Vector2(48, 175 ), Color.White);
            spriteBatch.Draw(pBox, new Vector2(75, 200 ), Color.White);
            spriteBatch.DrawString(description, "Left Hand", new Vector2(160, 175 ), Color.White);
            spriteBatch.Draw(pBox, new Vector2(175, 200 ), Color.White);


            spriteBatch.DrawString(description, "Belt", new Vector2(125, 350 ), Color.White);
            spriteBatch.DrawString(description, "1", new Vector2(75, 375 ), Color.White);
            spriteBatch.Draw(pBox, new Vector2(50, 400 ), Color.White);
            spriteBatch.DrawString(description, "2", new Vector2(150, 375 ), Color.White);
            spriteBatch.Draw(pBox, new Vector2(125, 400 ), Color.White);
            spriteBatch.DrawString(description, "3", new Vector2(225, 375 ), Color.White);
            spriteBatch.Draw(pBox, new Vector2(200, 400 ), Color.White);

            //spriteBatch.DrawString(description, "Gold:", new Vector2(10, 544), Color.Gold);

            for (int r = 0; r < 13; ++r)
                for (int c = 0; c < 8; ++c)
                    spriteBatch.Draw(iBox, new Vector2(r * 34 + slotStartX, c * 34 + slotStartY), Color.White);

            foreach (inventorySlot invItem in player.Items)
            {
                invItem.showTheHover();
                invItem.Draw(gameTime);
                if (Mouse.GetState().LeftButton == ButtonState.Released && old.LeftButton == ButtonState.Pressed)
                    if (invItem.mouseIsInside())
                    {
                        addItemToPlayer(invItem);
                        break;
                    }
                if (Mouse.GetState().RightButton == ButtonState.Released && old.RightButton == ButtonState.Pressed)
                    if (invItem.mouseIsInside())
                    {
                        addItemToPlayerBelt(invItem);
                        break;
                    }
            }

            foreach (inventorySlot invItem in player.Items)
            {
                invItem.drawHover();
            }

            for (int i = 0; i < 5; ++i)
                if (player.onCharItems[i] != null)
                {
                    player.onCharItems[i].showTheHover();
                    if (player.onCharItems[0] != null)
                        player.onCharItems[0].setPos(84, 208);
                    if (i == 1)
                        player.onCharItems[1].setPos(185, 208 );
                    if (i == 2)
                        player.onCharItems[2].setPos(60, 408 );
                    if (i == 3)
                        player.onCharItems[3].setPos(135, 408 );
                    if (i == 4)
                        player.onCharItems[4].setPos(210, 408 );
                    player.onCharItems[i].Draw(gameTime);
                    player.onCharItems[i].drawHover();
                    if (Mouse.GetState().LeftButton == ButtonState.Released && old.LeftButton == ButtonState.Pressed)
                        if (player.onCharItems[i].mouseIsInside())
                            removeFromPlayer(i);
                }

            spriteBatch.Draw(exit.getState(), new Vector2(exit.buttonBounds.X, exit.buttonBounds.Y), Color.White);
            spriteBatch.Draw(help.getState(), new Vector2(help.buttonBounds.X, help.buttonBounds.Y), Color.White);

            displayStats();

            showHelp();

            base.Draw(gameTime);
            oldk = Keyboard.GetState();
            old = Mouse.GetState();
        }
    }
}
