using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostLands
{
    class MainScreen : Screen
    {
        Player player;
        Texture2D texture;
        landScape gamelandScape;
        Monsters Mobs;
        uiClass ui;
        inventory backpackScreen;
        levelSkillScreen iSSM;
        AnimatedSprite Sarge;
        List<LootableItem> onScreenItems = new List<LootableItem>();

        //no need comment

        public MainScreen(Game game, ref Player p1, ref GraphicsDeviceManager graphics)
            : base(game)
        {
            texture = Content.Load<Texture2D>("map");
            player = p1;
            gamelandScape = new landScape(game);
            player.X = 300;
            player.Y = 300;
            Mobs = new Monsters(game, ref player, ref onScreenItems);
            backpackScreen = new inventory(game, ref player);
            iSSM = new levelSkillScreen(game, ref player);
            ui = new uiClass(game, player, ref backpackScreen, ref iSSM, ref graphics);
            //onScreenItems.Add(new LootableItem(game, 100, 300, new UsableItem(game, 5)));
            //onScreenItems.Add(new LootableItem(game, 100, 300, new UsableItem(game, 5)));
            //onScreenItems.Add(new LootableItem(game, 132, 300, new LClickItem(game, 1)));
            //onScreenItems.Add(new LootableItem(game, 164, 300, new UsableItem(game, 1)));
            //onScreenItems.Add(new LootableItem(game, 164, 300, new UsableItem(game, 1)));
            Sarge = new AnimatedSprite(game, Content.Load<Texture2D>(@"NPC/Sarge"), new Vector2(400, 300), ref player, -1);
            player.startQuestLine(Sarge, ref Mobs);
        }

        public bool talkingToSarge()
        {
            bool areYou= false;
            if (player.currentQuest.isComplete() || player.currentQuest.started)
                if(Sarge.bounds.Intersects(player.getBounds())){
                    areYou = true;
                }
            return areYou;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (!backpackScreen.hidenBackpack)
            {
                backpackScreen.Draw(gameTime);
            }else{
                if (!iSSM.hidenlSScreen)
                {
                    iSSM.Draw(gameTime);
                }else{
                    //spriteBatch.Draw(texture, new Vector2( -player.X,-player.Y ), Color.White);
                    //if (Keyboard.GetState().IsKeyUp(Keys.Space))
                        spriteBatch.Draw(texture, new Vector2(0, 0), new Rectangle(0 + player.MapX, 0 + player.MapY, 800, 600), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
                    gamelandScape.drawLandScape(ref player, player.MapX, player.MapY, gameTime);

                    if (player.currentQuest.isComplete() || player.currentQuest.isStarted())
                    {
                        Sarge.Draw(gameTime);
                        if (talkingToSarge())
                        {
                            player.currentQuest.makeSargeTalk();
                            if (player.currentQuest.finishQuest())
                            {
                                player.aQuestWasCompleted();
                                Random ran = new Random();
                                foreach (Item reward in player.currentQuest.rewardItems)
                                {
                                    onScreenItems.Add(new LootableItem(game, ran.Next((int)Sarge.ax - 100, (int)Sarge.ax + 100), ran.Next((int)Sarge.ay - 10, (int)Sarge.ay + 100), reward));
                                }
                            }
                        }
                        else if (player.currentQuest.startDialog.textDone && player.currentQuest.started)
                            player.currentQuest.started = false;
                        else if (player.currentQuest.endDialog.textDone)
                        {
                            player.questLine(Sarge, ref Mobs);
                        }
                        
                    }

                    Mobs.drawMonster(gameTime);

                    #region lootableItem
                    foreach (LootableItem pickUpableItem in onScreenItems)
                    {
                        if (pickUpableItem != null)
                        {
                            pickUpableItem.update(player);
                            pickUpableItem.Draw(gameTime);
                            if (pickUpableItem.remove)
                            {
                                onScreenItems.Remove(pickUpableItem);
                                break;
                            }
                            else if (Mouse.GetState().LeftButton == ButtonState.Released && old.LeftButton == ButtonState.Pressed)
                                if (pickUpableItem.pickup())
                                {
                                    player.pickupItem = true;
                                    pickUpableItem.hideHover();
                                    if (pickUpableItem.item.type == 1)
                                    {
                                        if (player.onCharItems[0] == null)
                                            player.onCharItems[0] = pickUpableItem;
                                        else
                                            player.Items.Add(pickUpableItem);
                                    }else if (pickUpableItem.item.type == 2)
                                    {
                                        if (player.onCharItems[1] == null)
                                            player.onCharItems[1] = pickUpableItem;
                                        else
                                            player.Items.Add(pickUpableItem);
                                    }else if (pickUpableItem.item.type == 3)
                                    {
                                        bool stackable = false;

                                        for (int i = 2; i < 5; ++i)
                                            if (player.onCharItems[i] != null && player.onCharItems[i].item.potionType == pickUpableItem.item.potionType
                                                && player.onCharItems[i].item.getID() == pickUpableItem.item.getID())
                                            {
                                                if (player.onCharItems[i].item.stacks < 5)
                                                {
                                                    stackable = true;
                                                    player.onCharItems[i].item.addStack(pickUpableItem.item.stacks);
                                                    break;
                                                }

                                            }

                                        foreach (inventorySlot invenItem in player.Items)
                                            if (invenItem.item.potionType == pickUpableItem.item.potionType
                                                && invenItem.item.getID() == pickUpableItem.item.getID())
                                            {
                                                if (invenItem.item.stacks < 5)
                                                {
                                                    stackable = true;
                                                    invenItem.item.addStack(pickUpableItem.item.stacks);
                                                    break;
                                                }

                                            }

                                        if (!stackable)
                                            player.Items.Add(pickUpableItem);
                                    }
                                    onScreenItems.Remove(pickUpableItem);
                                    break;
                                }
                        }
                    }
                    #endregion

                    player.Draw(gameTime);
                    foreach (LootableItem pickUpableItem in onScreenItems)
                    {
                        pickUpableItem.drawHover();
                    }
                    ui.Draw(gameTime);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
            old = Mouse.GetState();
        }
    }
}