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
    class PlayerStatSet : Screen
    {
        short points;

        int tempstr, tempcon, tempdex, tempwis, tempspr;

        button strengthAdd, strengthSub, dexAdd, dexSub, constAdd, constSub, wisAdd, wisSub, spiritAdd, spiritSub,
            reset, skillExit, commit, skills;

        Player p1;
        BackgroundImage Back;
        Rectangle strength, cons, dex, wis, spi;

        public PlayerStatSet(Game game, ref Player p1)
            : base(game)
        {

        }

        private void createButtons()
        {
            // setting up the buttons for adding and substracting for skills and stats
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

            // skill button on character stat screen
            skillExit = new button(800 - 129, 0, Content.Load<Texture2D>(@"Buttons\UIExit"), Content.Load<Texture2D>(@"Buttons\UIExit(2)"), Content.Load<Texture2D>(@"Buttons\UIExit(3)"));
            reset = new button(465, 490, Content.Load<Texture2D>(@"Buttons\resetStats"), Content.Load<Texture2D>(@"Buttons\resetStats(2)"), Content.Load<Texture2D>(@"Buttons\resetStats(3)"));
            commit = new button(160, 490, Content.Load<Texture2D>(@"Buttons\Commit"), Content.Load<Texture2D>(@"Buttons\Commit(2)"), Content.Load<Texture2D>(@"Buttons\Commit(3)"));
            skills = new button(640, 490, Content.Load<Texture2D>(@"buttons\UISkillButton"), Content.Load<Texture2D>(@"buttons\UISkillButton(2)"), Content.Load<Texture2D>(@"buttons\UISkillButton(3)"));
            strength = new Rectangle(280, 285, 120, 40);
            cons = new Rectangle(280, 325, 150, 40);
            dex = new Rectangle(280, 365, 140, 40);
            wis = new Rectangle(280, 405, 120, 40);
            spi = new Rectangle(280, 445, 85, 40);
        }

        #region buttons draw
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
            spriteBatch.Draw(reset.getState(), reset.buttonBounds, Color.White);
            spriteBatch.Draw(skillExit.getState(), skillExit.buttonBounds, Color.White);
            spriteBatch.Draw(commit.getState(), commit.buttonBounds, Color.White);
            spriteBatch.Draw(skills.getState(), skills.buttonBounds, Color.White);
        }
        #endregion
        #region drawViltalies
        private void drawVitalites()
        {
            spriteBatch.DrawString(font2, "Skill Points", new Vector2(280, 245), Color.LightSlateGray);
            spriteBatch.DrawString(font2, points + "", new Vector2(515, 245), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Strength", new Vector2(280, 285), Color.LightSlateGray);
            spriteBatch.DrawString(font2, tempstr + "", new Vector2(515, 285), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Dexterity", new Vector2(280, 325), Color.LightSlateGray);
            spriteBatch.DrawString(font2, tempdex + "", new Vector2(515, 325), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Constitution", new Vector2(280, 365), Color.LightSlateGray);
            spriteBatch.DrawString(font2, tempcon + "", new Vector2(515, 365), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Wisdom", new Vector2(280, 405), Color.LightSlateGray);
            spriteBatch.DrawString(font2, tempwis + "", new Vector2(515, 405), Color.LightSlateGray);
            spriteBatch.DrawString(font2, "Spirit", new Vector2(280, 440), Color.LightSlateGray);
            spriteBatch.DrawString(font2, tempspr + "", new Vector2(515, 440), Color.LightSlateGray);
        }
        #endregion

        public void setPlayerStats(int s, int d, int c, int w, int sp)
        {// this is for the reset to work
            p1.changeStats(s, d, c, w, sp);
        }

        #region vital Description
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
                message = "Determines attack and dodge rate";
                spriteBatch.Draw(Back.image, new Rectangle(Mouse.GetState().X - 150, Mouse.GetState().Y - 40, 360, 40), Color.Black);
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
        #endregion

        private void tempStat()
        {
            tempstr = p1.getStr();
            tempcon = p1.getCons();
            tempdex = p1.getDex();
            tempwis = p1.getWis();
            tempspr = p1.getSpi();
        }

        #region statButtons
        public void statButtons()
        {
            if (strengthAdd.isReleased() && points != 0)
            {
                ++tempstr;
                --points;
            }
            else if (strengthSub.isReleased() && tempstr > p1.getStr())
            {
                --tempstr;
                ++points;
            }
            else
                if (dexAdd.isReleased() && points != 0)
                {
                    ++tempdex;
                    --points;
                }
                else if (dexSub.isReleased() && tempdex > p1.getDex())
                {
                    --tempdex;
                    ++points;
                }
                else
                    if (constAdd.isReleased() && points != 0)
                    {
                        ++tempcon;
                        --points;
                    }
                    else if (constSub.isReleased() && tempcon > p1.getCons())
                    {
                        --tempcon;
                        ++points;
                    }
                    else
                        if (wisAdd.isReleased() && points != 0)
                        {
                            ++tempwis;
                            --points;
                        }
                        else if (wisSub.isReleased() && tempwis > p1.getWis())
                        {
                            --tempwis;
                            ++points;
                        }
                        else
                            if (spiritAdd.isReleased() && points != 0)
                            {
                                ++tempspr;
                                --points;
                            }
                            else if (spiritSub.isReleased() && tempspr > p1.getSpi())
                            {
                                --tempspr;
                                ++points;
                            }
                            else if (reset.isReleased())
                            {
                                tempStat();
                                points = p1.getSkillPoints();
                            }
        }
        #endregion
    }
}
