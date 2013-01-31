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
    class PlayerCreationScreen : Screen
    {
        short points, bstr, bdex, bcons, bwis, bspr;
        button strengthAdd, strengthSub, dexAdd, dexSub, constAdd, constSub, wisAdd, wisSub, spiritAdd, spiritSub, warrior, archer, mage, reset, advent;
        public Player p1;
        string pickedClass, cdescription, name = "";
        BackgroundImage Back;
        Texture2D CharPick;
        KeyboardState keyState, oldKeyState;
        bool gotName;
        Rectangle strength, cons, dex, wis, spi;

        public PlayerCreationScreen(Game game, ref Player player): base(game)
        {
            Back = new BackgroundImage(Content.Load<Texture2D>(@"Backgrounds\CreationBackground"));
            createButtons();
            points = 10;
            p1 = player;
            setWarrior();
        }
        public Player getPlayer()
        {
            return p1;
        }
        private void createButtons()
        {
            Texture2D add = Content.Load<Texture2D>(@"Buttons\add"), add2 = Content.Load<Texture2D>(@"Buttons\add(2)"), add3 = Content.Load<Texture2D>(@"Buttons\add(3)");
            Texture2D sub = Content.Load<Texture2D>(@"Buttons\subtract"), sub2 = Content.Load<Texture2D>(@"Buttons\subtract(2)"), sub3 = Content.Load<Texture2D>(@"Buttons\subtract(3)");
            strengthAdd = new button(560, 290, add, add2, add3);
            strengthSub = new button(460, 290, sub, sub2, sub3);
            dexAdd = new button(560, 330, add, add2, add3);
            dexSub = new button(460, 330, sub, sub2, sub3);
            constAdd = new button(560, 370, add, add2, add3);
            constSub = new button(460, 370, sub, sub2, sub3);
            wisAdd = new button(560, 410, add, add2, add3);
            wisSub = new button(460, 410, sub, sub2, sub3);
            spiritAdd = new button(560, 450, add, add2, add3);
            spiritSub = new button(460, 450, sub, sub2, sub3);

            warrior = new button(70, 180, Content.Load<Texture2D>(@"Buttons\Warrior"), Content.Load<Texture2D>(@"Buttons\Warrior(2)"), Content.Load<Texture2D>(@"Buttons\Warrior(3)"));
            //archer = new button(325, 180, Content.Load<Texture2D>(@"Buttons\Archer"), Content.Load<Texture2D>(@"Buttons\Archer(2)"), Content.Load<Texture2D>(@"Buttons\Archer(3)"));
            //mage = new button(590, 180, Content.Load<Texture2D>(@"Buttons\mage"), Content.Load<Texture2D>(@"Buttons\mage(2)"), Content.Load<Texture2D>(@"Buttons\mage(3)"));
            reset = new button(465, 490, Content.Load<Texture2D>(@"Buttons\resetStats"), Content.Load<Texture2D>(@"Buttons\resetStats(2)"), Content.Load<Texture2D>(@"Buttons\resetStats(3)"));
            advent = new button(160, 490, Content.Load<Texture2D>(@"Buttons\startAdv"), Content.Load<Texture2D>(@"Buttons\startAdv(2)"), Content.Load<Texture2D>(@"Buttons\startAdv(3)"));

            strength = new Rectangle(280, 285, 120, 40);
            cons = new Rectangle(280, 325, 150, 40);
            dex = new Rectangle(280, 365, 140, 40);
            wis = new Rectangle(280, 405, 120, 40);
            spi = new Rectangle(280, 445, 85, 40);
            setWarrior();
        }
        public void drawButtons()
        {
            spriteBatch.Draw(strengthAdd.getState(), strengthAdd.buttonBounds, Color.White);
            spriteBatch.Draw(strengthSub.getState(), strengthSub.buttonBounds, Color.White);
            spriteBatch.Draw(dexAdd.getState(), dexAdd.buttonBounds, Color.White);
            spriteBatch.Draw(dexSub.getState(), dexSub.buttonBounds, Color.White);
            spriteBatch.Draw(constAdd.getState(), constAdd.buttonBounds, Color.White);
            spriteBatch.Draw(constSub.getState(), constSub.buttonBounds, Color.White);
            spriteBatch.Draw(wisAdd.getState(), wisAdd.buttonBounds, Color.White);
            spriteBatch.Draw(wisSub.getState(), wisSub.buttonBounds, Color.White);
            spriteBatch.Draw(spiritAdd.getState(), spiritAdd.buttonBounds, Color.White);
            spriteBatch.Draw(spiritSub.getState(), spiritSub.buttonBounds, Color.White);
            spriteBatch.Draw(warrior.getState(), warrior.buttonBounds, Color.White);
            //spriteBatch.Draw(archer.getState(), archer.buttonBounds, Color.White);
            //spriteBatch.Draw(mage.getState(), mage.buttonBounds, Color.White);
            spriteBatch.Draw(reset.getState(), reset.buttonBounds, Color.White);
            if(points == 0)
                spriteBatch.Draw(advent.getState(), advent.buttonBounds, Color.White);
        }
        private void askName()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            foreach (Keys key in keyState.GetPressedKeys())
            {
                if (oldKeyState.IsKeyUp(key))
                {
                    if (key == Keys.Back)
                    {
                        if(name.Length != 0)
                        name = name.Remove(name.Length - 1, 1);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        if (name.Length > 10)
                            name = name.Substring(0, 10);
                        System.Diagnostics.Trace.WriteLine(name); // (just do whatever you need with the name
                        gotName = true;
                        p1.setName(name);
                    }
                    else
                    {
                        if (key.ToString().Length == 1)
                            if (name.Length != 0)
                                name += key.ToString().ToLower();
                            else
                                name += key.ToString();
                    }
                }
            }
            spriteBatch.DrawString(font2, "What is your name?", new Vector2(200, 200), Color.White);
            spriteBatch.DrawString(font2, name, new Vector2(200, 250), Color.White);
        }
        private void drawVitalites()
        {
            spriteBatch.DrawString(font2, "Skill Points", new Vector2(280, 245), Color.LightSlateGray);
            spriteBatch.DrawString(font2, points + "", new Vector2(515, 245), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Strength", new Vector2(280, 285), Color.LightSlateGray);
            spriteBatch.DrawString(font2, bstr + p1.getStr() + "", new Vector2(515, 285), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Constitution", new Vector2(280, 325), Color.LightSlateGray);
            spriteBatch.DrawString(font2, bdex + p1.getDex() + "", new Vector2(515, 325), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Dexterity", new Vector2(280, 365), Color.LightSlateGray);
            spriteBatch.DrawString(font2, bcons + p1.getCons() + "", new Vector2(515, 365), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Wisdom", new Vector2(280, 405), Color.LightSlateGray);
            spriteBatch.DrawString(font2, bwis + p1.getWis() + "", new Vector2(515, 405), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Spirit", new Vector2(280, 440), Color.LightSlateGray);
            spriteBatch.DrawString(font2, bspr + p1.getSpi() + "", new Vector2(515, 440), Color.LightSlateGray);
        }
        private void setWarrior()
        {
            bstr = 20;
            bdex = 10;
            bcons = 18;
            bwis = 6;
            bspr = 6;
            CharPick = Content.Load<Texture2D>(@"CharPics\Warrior(2)");
            pickedClass = "Warrior";
            cdescription = "A strong willed fighter. Warriors love physical combat and confrontations.\n" +
                "They use their higher strength to wield weapons to damage thier opponents.\n" +
                "They will overpower any opposer with thier higher constitution\nand drive giving them more health";
        }
        private void setArcher(){
            bstr = 15;
            bdex = 20;
            bcons = 12;
            bwis = 6;
            bspr = 7;
            CharPick = Content.Load<Texture2D>(@"CharPics\Archer(2)");
            pickedClass = "Archer";
            cdescription = "As fast strategists archers would rather dodge a hit then take it.\n" +
                "Thier low constitution is made up with a higer amount of dexterity giving them a\n" +
                "better chance to dodge and the ability to attack very quickly.\nArchers also have a medium to high amount of strength.";
        }
        private void setMage()
        {
            bstr = 10;
            bdex = 11;
            bcons = 7;
            bwis = 15;
            bspr = 20;
            CharPick = Content.Load<Texture2D>(@"CharPics\Mage(2)");
            pickedClass = "Mage";
            cdescription = "As spellcasters mage's have the highest amount of wisdom giving them higher spell\n" +
                "damage and have the highest spririt giving them the most amount of mana.\n" +
                "They have the lowest amount of constitution resulting\nin a reliance on magic to survive.";
        }

        private void vitalDescription()
        {
            string message = "";
            if (strength.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                message = "Determines weapon damage";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 320, 40), Color.Black);
            }
            else if (cons.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                message = "Determines max health and regen";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 350, 40), Color.Black);
            }
            else if (dex.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                message = "Determines attack and Stamina regen rate";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 410, 40), Color.Black);
            }
            else if (wis.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                message = "Determines magic damage";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 300, 40), Color.Black);
            }
            else if (spi.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                message = "Determines max mana and regen rate";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 380, 40), Color.Black);
            }


            spriteBatch.DrawString(description, message, new Vector2(Mouse.GetState().X - 100, Mouse.GetState().Y - 35), Color.Brown);
        }

        public void statButtons()
        {
            if (strengthAdd.isReleased() && points != 0)
            {
                setPlayerStats(1, 0, 0, 0, 0);
                --points;
            }
            else if (strengthSub.isReleased() && points < 10 && p1.getStr() + bstr > bstr)
            {
                setPlayerStats(-1, 0, 0, 0, 0);
                ++points;
            }
            else

                if (dexAdd.isReleased() && points != 0)
                {
                    setPlayerStats(0, 0, 1, 0, 0);
                    --points;
                }
                else if (dexSub.isReleased() && points < 10 && p1.getDex() + bdex > bdex)
                {
                    setPlayerStats(0, 0, -1, 0, 0);
                    ++points;
                }
                else

                    if (constAdd.isReleased() && points != 0)
                    {
                        setPlayerStats(0, 1, 0, 0, 0);
                        --points;
                    }
                    else if (constSub.isReleased() && points < 10 && p1.getCons() + bcons > bcons)
                    {
                        setPlayerStats(0, -1, 0, 0, 0);
                        ++points;
                    }
                    else

                        if (wisAdd.isReleased() && points != 0)
                        {
                            setPlayerStats(0, 0, 0, 1, 0);
                            --points;
                        }
                        else if (wisSub.isReleased() && points < 10 && p1.getWis() + bwis > bwis)
                        {
                            setPlayerStats(0, 0, 0, -1, 0);
                            ++points;
                        }
                        else

                            if (spiritAdd.isReleased() && points != 0)
                            {
                                setPlayerStats(0, 0, 0, 0, 1);
                                --points;
                            }
                            else if (spiritSub.isReleased() && points < 10 && p1.getSpi() + bspr > bspr)
                            {
                                setPlayerStats(0, 0, 0, 0, -1);
                                ++points;
                            }
                            else

                                if (reset.isReleased())
                                {
                                    setPlayerStats(-p1.getStr(), -p1.getCons(), -p1.getDex(), -p1.getWis(), -p1.getSpi());
                                    points = 10;
                                }

        }

        public void setPlayerStats(int s, int d, int c, int w, int sp)
        {
            p1.changeStats(s, d, c, w, sp);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Back.image, Back.bounds, Color.White);
            if (!gotName)
                askName();
            else
            {
                if (warrior.isReleased())
                    setWarrior();
                //else if (archer.isReleased())
                //    setArcher();
                //else if (mage.isReleased())
                //    setMage();

                //spriteBatch.Draw(Back.image, new Rectangle(0, 60, 800, 100), Color.Black);//Black box
                spriteBatch.Draw(CharPick, new Vector2(50, 245), Color.White);

                statButtons();

                spriteBatch.DrawString(font2, pickedClass, new Vector2(300, 0), Color.White);
                spriteBatch.DrawString(font2, name, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(description, cdescription, new Vector2(0, 60), Color.Silver);
                drawVitalites();
                drawButtons();
                vitalDescription();

                if (advent.isReleased())
                {
                    p1.changeStats(bstr, bcons, bdex, bwis, bspr);
                    p1.setVitalities();
                    p1.setClass(pickedClass);
                    //p1.onCharItems[0] = new inventorySlot(game, new LClickItem(game, 1));
                    active = false;
                }
            }
            spriteBatch.End();
        }
    }
}