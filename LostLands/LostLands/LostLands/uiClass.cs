using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostLands
{
    class uiClass : Screen
    {
        #region setup screen

        // getting player information
        Player pAll;
        int level, playerXP, skillTimer=5, colorChangeTimer= 50;
        Color skillShade=Color.White;

        // loading the names i am using for different sprites
        Texture2D wood;
        Texture2D bagSlot;
        Texture2D rMouseButton;
        Texture2D lMouseButton;
        Texture2D health, Mana, Stam;
        Texture2D miniMap;
        Texture2D statBox;
        Texture2D BackG;
        Texture2D tarPlate;

        // button variable names
        button backpack, option, map, skillB;

        // seting my variables for bar stats on main screen
        double pH, pS, pM, pMS;

        // seting my flags for button to be used over and over
        bool hidenBar = false;
        bool mapWindow = false;
        bool fpsWindow = true;
        bool statBar1 = true;

        // label class for use here
        levelSkillScreen lSS;
        inventory i1;
        KeyboardState oldk;
        MouseState oldM;
        GraphicsDeviceManager graphics;

        #endregion
        public void PLayerL()
        {
            // geting information from player here
            level = pAll.pLevel();
            playerXP = pAll.Xplayer();
            // xp setup
            spriteBatch.DrawString(description, "Xp:", new Vector2(368, -2), Color.Firebrick);
            spriteBatch.DrawString(targetBar, playerXP + "", new Vector2(400, 0), Color.Black);
            // level setup
            spriteBatch.DrawString(description, "Level: ", new Vector2(495, -2), Color.Firebrick);
            spriteBatch.DrawString(font2, level + "", new Vector2(547, -10), Color.Black);
        }

        public uiClass(Game game, Player p1, ref inventory backpackScreen, ref levelSkillScreen lSS1, ref GraphicsDeviceManager graphics)
            : base(game)
        {
            this.graphics = graphics;
            health = Content.Load<Texture2D>(@"healthH");
            Stam = Content.Load<Texture2D>(@"stamS");
            Mana = Content.Load<Texture2D>(@"manaM");
            wood = Content.Load<Texture2D>(@"woodImage");
            bagSlot = Content.Load<Texture2D>(@"bagSlot");
            miniMap = Content.Load<Texture2D>(@"miniMap1");
            rMouseButton = Content.Load<Texture2D>(@"RHandSlot");
            lMouseButton = Content.Load<Texture2D>(@"LHandSlot");
            statBox = Content.Load<Texture2D>(@"Box");
            BackG = Content.Load<Texture2D>(@"Backgrounds\CreationBackground");
            tarPlate = Content.Load<Texture2D>(@"targetPlate");
            lSS = lSS1;// levelskillscreen = lss
            i1 = backpackScreen;// backscreen
            pAll = p1;// player 
            backpack = new button(322, 565, Content.Load<Texture2D>(@"buttons\UIBackpackButton"), Content.Load<Texture2D>(@"buttons\UIBackpackButton(2)"), Content.Load<Texture2D>(@"buttons\UIBackpackButton(3)"));
            option = new button(562, 565, Content.Load<Texture2D>(@"buttons\UIoptionsButton"), Content.Load<Texture2D>(@"buttons\UIoptionsButton(2)"), Content.Load<Texture2D>(@"buttons\UIoptionsButton(3)"));
            map = new button(482, 565, Content.Load<Texture2D>(@"buttons\UImapButton"), Content.Load<Texture2D>(@"buttons\UImapButton(2)"), Content.Load<Texture2D>(@"buttons\UImapButton(3)"));
            skillB = new button(402, 565, Content.Load<Texture2D>(@"buttons\UISkillButton"), Content.Load<Texture2D>(@"buttons\UISkillButton(2)"), Content.Load<Texture2D>(@"buttons\UISkillButton(3)"));
        }
        private void statBar()
        {
            pH = ((pAll.getCurentHealth()) / pAll.getMaxHealth()) * 100;
            pS = ((pAll.getCurentStam()) / pAll.getMaxStam()) * 100;
            pM = ((pAll.getCurentMana()) / pAll.getMaxMana()) * 100;
            pMS = 100;
        }

        public void useOnBeltItem(int pos, inventorySlot playerSlot)
        {
            inventorySlot temp = new inventorySlot(game, playerSlot.item);
            if (temp.item.type != 3)
            {
                if (pAll.onCharItems[temp.item.type - 1] != null)
                {
                    playerSlot.setItem(pAll.onCharItems[temp.item.type - 1].item);
                    pAll.onCharItems[temp.item.type - 1].setItem(temp.item);
                    pAll.onCharItems[temp.item.type - 1].item.setUsed(!playerSlot.item.usable());
                    playerSlot.item.makeUsable();
                }
                else
                {
                    pAll.onCharItems[pos] = null;
                    pAll.onCharItems[temp.item.type - 1] = new inventorySlot(game, temp.item);
                }
            }
            else if (true)
            {
                playerSlot.item.useUsable(ref pAll);
                if (playerSlot.item.stacks <= 0)
                    pAll.onCharItems[pos] = null;
            }

        }

        public void skillGlow()
        {

            if (colorChangeTimer > 0)
                --colorChangeTimer;
            else
            {
                colorChangeTimer = 50;
                switch (skillTimer)
                {
                    case 1:
                        skillShade = Color.White;
                        break;
                    case 2:
                        skillShade = Color.Wheat;
                        break;
                    case 3:
                        skillShade = Color.WhiteSmoke;
                        break;
                    case 4:
                        skillShade = Color.Yellow;
                        break;
                }

                if (skillTimer > 0)
                    --skillTimer;
                else
                    skillTimer = 4;
            }

        }

        public override void Draw(GameTime gameTime)
        {

            #region stats
            if (Keyboard.GetState().IsKeyUp(Keys.U) && oldk.IsKeyDown(Keys.U))
            {
                statBar1 = !statBar1;
            }

            if(pAll.oneHandSkillPoints() > 0)
                skillGlow();//Makes skill button glow when pplayer has points
            else
                skillShade = Color.White;

            if (!statBar1)
            {
                statBar();
                // stat bars when ui is closed
                //spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)100, 10), Color.Black);
                //spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)pH, 10), Color.Red);
                //spriteBatch.Draw(statBox, new Rectangle(17, 562, (int)pMS, 10), Color.Black);
                //spriteBatch.Draw(statBox, new Rectangle(17, 562, (int)pS, 10), Color.Yellow);
                //spriteBatch.Draw(statBox, new Rectangle(17, 582, (int)pM, 10), Color.Blue);
                //// stat icons for bar
                //spriteBatch.Draw(health, new Vector2(0, 540), Color.White);
                //spriteBatch.Draw(Stam, new Vector2(0, 560), Color.White);
                ////spriteBatch.Draw(Mana, new Vector2(0, 580), Color.White);
                spriteBatch.Draw(tarPlate, new Vector2(200, 0), Color.White);

                spriteBatch.Draw(statBox, new Rectangle(17, 562, (int)100, 10), Color.Black);
                spriteBatch.Draw(statBox, new Rectangle(17, 562, (int)pH, 10), Color.Red);
                spriteBatch.Draw(statBox, new Rectangle(17, 582, (int)pMS, 10), Color.Black);
                spriteBatch.Draw(statBox, new Rectangle(17, 582, (int)pS, 10), Color.Yellow);
                if (pAll.getCurentStam() > pAll.getCostPerSwing())
                    pAll.stamSubText.Y = 572;
                // stat icons for bar
                spriteBatch.Draw(health, new Vector2(0, 560), Color.White);
                spriteBatch.Draw(Stam, new Vector2(0, 580), Color.White);
            }
            else
            {
                // stat bar drawing when uibar is open
                statBar();
                //spriteBatch.Draw(statBox, new Rectangle(17, 502, (int)100, 10), Color.Black);
                //spriteBatch.Draw(statBox, new Rectangle(17, 502, (int)pH, 10), Color.Red);
                //spriteBatch.Draw(statBox, new Rectangle(17, 522, (int)pMS, 10), Color.Black);
                //spriteBatch.Draw(statBox, new Rectangle(17, 522, (int)pS, 10), Color.Yellow);
                ////spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)pM, 10), Color.Blue);
                //// stat icons for bar
                //spriteBatch.Draw(health, new Vector2(0, 500), Color.White);
                //spriteBatch.Draw(Stam, new Vector2(0, 520), Color.White);
                //spriteBatch.Draw(Mana, new Vector2(0, 540), Color.White);
                spriteBatch.Draw(tarPlate, new Vector2(200, 0), Color.White);

                spriteBatch.Draw(statBox, new Rectangle(17, 522, (int)100, 10), Color.Black);
                spriteBatch.Draw(statBox, new Rectangle(17, 522, (int)pH, 10), Color.Red);
                spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)pMS, 10), Color.Black);
                spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)pS, 10), Color.Yellow);
                if (pAll.getCurentStam() > pAll.getCostPerSwing())
                    pAll.stamSubText.Y = 532;
                //spriteBatch.Draw(statBox, new Rectangle(17, 542, (int)pM, 10), Color.Blue);
                // stat icons for bar
                spriteBatch.Draw(health, new Vector2(0, 520), Color.White);
                spriteBatch.Draw(Stam, new Vector2(0, 540), Color.White);
            }

            // player level
            PLayerL();
            #endregion

            # region Utility bar
            if (Keyboard.GetState().IsKeyUp(Keys.U) && oldk.IsKeyDown(Keys.U))
            {
                hidenBar = !hidenBar;
                pAll.uiDown = !pAll.uiDown;
            }
            if (!hidenBar)
            {
                spriteBatch.Draw(wood, new Rectangle(0, 560, 800, 40), Color.White);
                spriteBatch.Draw(bagSlot, new Rectangle(200, 565, 32, 32), Color.White);
                spriteBatch.Draw(bagSlot, new Rectangle(236, 565, 32, 32), Color.White);
                spriteBatch.Draw(bagSlot, new Rectangle(272, 565, 32, 32), Color.White);
                spriteBatch.Draw(rMouseButton, new Vector2(100, 565), Color.White);
                spriteBatch.Draw(lMouseButton, new Vector2(150, 565), Color.White);
                spriteBatch.DrawString(font2, pAll.getName(), new Vector2(10, 560), Color.Brown);
                spriteBatch.Draw(backpack.getState(), backpack.buttonBounds, Color.White);
                spriteBatch.Draw(option.getState(), option.buttonBounds, Color.White);
                spriteBatch.Draw(map.getState(), map.buttonBounds, Color.White);
                spriteBatch.Draw(skillB.getState(), skillB.buttonBounds, skillShade);
                spriteBatch.DrawString(description, "Gold:", new Vector2(630, 570), Color.Gold);

                foreach (inventorySlot playerSlot in pAll.onCharItems)
                {
                    if (playerSlot != null)
                    {
                        playerSlot.hideHover();

                        if (playerSlot.item.getType() == 3)
                        {
                            playerSlot.item.checkPotion(ref pAll);
                        }

                        if (playerSlot == pAll.onCharItems[0])
                        {
                            
                            playerSlot.setPos(100, 565);
                            
                        }
                        else if (playerSlot == pAll.onCharItems[1])
                        {
                            playerSlot.setPos(150, 565);
                        }
                        else if (playerSlot == pAll.onCharItems[2])
                        {
                            playerSlot.setPos(200, 565);
                            if (Keyboard.GetState().IsKeyUp(Keys.D1) && oldk.IsKeyDown(Keys.D1) ||
                                (Mouse.GetState().LeftButton == ButtonState.Released && oldM.LeftButton == ButtonState.Pressed && playerSlot.mouseIsInside()))
                            {
                                useOnBeltItem(2, playerSlot);
                            }
                        }
                        else if (playerSlot == pAll.onCharItems[3])
                        {
                            playerSlot.setPos(236, 565);
                            if (Keyboard.GetState().IsKeyUp(Keys.D2) && oldk.IsKeyDown(Keys.D2) ||
                                (Mouse.GetState().LeftButton == ButtonState.Released && oldM.LeftButton == ButtonState.Pressed && playerSlot.mouseIsInside()))
                            {
                                useOnBeltItem(3, playerSlot);
                            }
                        }
                        else if (playerSlot == pAll.onCharItems[4])
                        {
                            playerSlot.setPos(272, 565);
                            if (Keyboard.GetState().IsKeyUp(Keys.D3) && oldk.IsKeyDown(Keys.D3) ||
                                (Mouse.GetState().LeftButton == ButtonState.Released && oldM.LeftButton == ButtonState.Pressed && playerSlot.mouseIsInside()))
                            {
                                useOnBeltItem(4, playerSlot);
                            }
                        }

                        playerSlot.Draw(gameTime);
                        playerSlot.drawHover();
                        
                    }
                }
                foreach (inventorySlot playerSlot in pAll.onCharItems)
                {
                    if (playerSlot != null && playerSlot.getHoverState())
                        playerSlot.drawHover();
                }
            }
            # endregion

            #region Map function
            if (Keyboard.GetState().IsKeyUp(Keys.M) && oldk.IsKeyDown(Keys.M) || map.isReleased())
            {
                mapWindow = !mapWindow;
            }
            if (!mapWindow)
            {
                spriteBatch.Draw(wood, new Rectangle(700, 0, 100, 100), Color.White);
                spriteBatch.Draw(miniMap, new Vector2(710, 10), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>(@"Box"), new Rectangle(710 + (pAll.X + pAll.MapX) / 32, 10 + (pAll.Y + pAll.MapY) / 32, 1, 1), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>(@"Box"), pAll.currentQuest.miniDot, Color.Yellow);
            }
            #endregion

            if (pAll.onCharItems[0] != null)
            {
                Game.IsMouseVisible = false;
                if (pAll.onCharItems[0].item.usable())
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Arrow"), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                    spriteBatch.Draw(pAll.onCharItems[0].item.ItemPic, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                    
                }
                else
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Arrow"), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.Gray);
                    spriteBatch.Draw(pAll.onCharItems[0].item.ItemPic, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.Gray);
                   
                }
            }
            else
                Game.IsMouseVisible = true;

            #region FPS
            if (Keyboard.GetState().IsKeyUp(Keys.P) && oldk.IsKeyDown(Keys.P))
            {
                fpsWindow = !fpsWindow;
            }
            if (!fpsWindow)
            {
                spriteBatch.Draw(wood, new Rectangle(0, 0, 84, 44), Color.White);
            }
            #endregion

            #region Option
            if (option.isReleased())
            {
                
                graphics.ToggleFullScreen();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.O) && oldk.IsKeyDown(Keys.O))
            {
                Game.IsMouseVisible = !Game.IsMouseVisible;
                lSS.hidenlSScreen = !lSS.hidenlSScreen;
                lSS.hidenSkill = false;
            }
            if (skillB.isReleased() || Keyboard.GetState().IsKeyUp(Keys.L) && oldk.IsKeyDown(Keys.L))
            {
                lSS.hidenlSScreen = !lSS.hidenlSScreen;
                lSS.switchedTo = true;
                lSS.hidenSkill = true;
            }
            #endregion

            #region Backpack
            if (backpack.isReleased() || Keyboard.GetState().IsKeyUp(Keys.B) && oldk.IsKeyDown(Keys.B))
            {
                i1.hidenBackpack = !i1.hidenBackpack;
                Game.IsMouseVisible = true;
            }
            #endregion

            

            base.Draw(gameTime);
            oldk = Keyboard.GetState();
            oldM = Mouse.GetState();
        }
    }
}