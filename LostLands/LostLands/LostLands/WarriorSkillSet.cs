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
    class WarriorSkillSet : Screen
    {
        int bonusPoints, tempSpeedOneH, tempDamOneH, tempBlockOneH, tempSpeedTwoH,
            tempDamTwoH, tempBlockTwoH, oneHandSPoints, twoHandSPoints;

        button swordSpeedAddOneH, swordSpeedSubOneH, swordSpeedAddTwoH, swordSpeedSubTwoH, swordDamageSubOneH, swordDamageAddOneH, swordDamageAddTwoH, swordDamageSubTwoH,
            blockOneHandAdd, blockOneHandSub, blockTwoHandedAdd, blockTwoHandedSub;

        Player p1;
        Texture2D Warrior;
        Texture2D skillBox;
        Texture2D skillBoxFill;
        BackgroundImage Back;
        levelSkillScreen lSS;

        public WarriorSkillSet(Game game, ref Player p1, levelSkillScreen lss)
            : base(game)
        {
            this.lSS = lss;
            Back = new BackgroundImage(Content.Load<Texture2D>(@"Backgrounds\CreationBackground"));
            createButtons();
            this.p1 = p1;
        }
        private void createButtons()
        {
            Texture2D add = Content.Load<Texture2D>(@"Buttons\add"), add2 = Content.Load<Texture2D>(@"Buttons\add(2)"), add3 = Content.Load<Texture2D>(@"Buttons\add(3)");
            Texture2D sub = Content.Load<Texture2D>(@"Buttons\subtract"), sub2 = Content.Load<Texture2D>(@"Buttons\subtract(2)"), sub3 = Content.Load<Texture2D>(@"Buttons\subtract(3)");
            
            // 1h sword add and sub buttons layout
            swordSpeedSubOneH = new button(195, 100, sub, sub2, sub3);
            swordSpeedAddOneH = new button(455, 100, add, add2, add3);
            swordDamageSubOneH = new button(195, 160, sub, sub2, sub3);
            swordDamageAddOneH = new button(455, 160, add, add2, add3);
            blockOneHandSub = new button(195, 220, sub, sub2, sub3);
            blockOneHandAdd = new button(455, 220, add, add2, add3);

            // 2h sword add and sub buttons layout
            swordSpeedSubTwoH = new button(195, 290, sub, sub2, sub3);
            swordSpeedAddTwoH = new button(455, 290, add, add2, add3);
            swordDamageSubTwoH = new button(195, 350, sub, sub2, sub3);
            swordDamageAddTwoH = new button(455, 350, add, add2, add3);
            blockTwoHandedSub = new button(195, 410, sub, sub2, sub3);
            blockTwoHandedAdd = new button(455, 410, add, add2, add3);

            // skill tree box to show your progression on that skill
            skillBox = Content.Load<Texture2D>(@"Box");
            skillBoxFill = Content.Load<Texture2D>(@"Box");
        }

        private void setPicOfClass()
        {
            Warrior = Content.Load<Texture2D>(@"CharPics\Warrior(2)");
        }

        public int oneHandSwordSkillPoints()
        {
            oneHandSPoints = p1.oneHandSkillPoints();
            return oneHandSPoints;
        }

        public int twoHandedSwordSkillPoints()
        {
            twoHandSPoints = p1.twoHandSkillPoints();
            return twoHandSPoints;
        }

        public void skillTreeW(GameTime gameTime)
        {
           
                spriteBatch.Draw(Back.image, Back.bounds, Color.White);
                // starting buttons for skill sets for melee 
                // melee combat buttons and skill bar for 1h sword           
                spriteBatch.DrawString(font2, "1h  Sword", new Vector2(30, 160), Color.Firebrick);
                spriteBatch.Draw(skillBox, new Rectangle(240, 120, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 120, (tempSpeedOneH + p1.getOneSwordSpeed()) * 20, 10), Color.Red);
                if (tempSpeedOneH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 120, tempSpeedOneH + 2, 10), Color.Gold);
                }
                spriteBatch.DrawString(font2, "Sword  Speed", new Vector2(240, 80), Color.Firebrick);
                spriteBatch.Draw(swordSpeedAddOneH.getState(), swordSpeedAddOneH.buttonBounds, Color.White);
                spriteBatch.Draw(swordSpeedSubOneH.getState(), swordSpeedSubOneH.buttonBounds, Color.White);
                spriteBatch.Draw(skillBox, new Rectangle(240, 180, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 180, (tempDamOneH + p1.getOneSwordDam()) * 20, 10), Color.Red);
                if (tempDamOneH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 180, tempDamOneH + 2, 10), Color.Gold);
                }
                spriteBatch.DrawString(font2, "Sword Damage", new Vector2(240, 140), Color.Firebrick);
                spriteBatch.Draw(swordDamageAddOneH.getState(), swordDamageAddOneH.buttonBounds, Color.White);
                spriteBatch.Draw(swordDamageSubOneH.getState(), swordDamageSubOneH.buttonBounds, Color.White);
                spriteBatch.Draw(skillBox, new Rectangle(240, 240, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 240, (tempBlockOneH + p1.getOneSwordBlock()) * 20, 10), Color.Red);
                if (tempBlockOneH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 240, tempBlockOneH + 2, 10), Color.Gold);
                }
                spriteBatch.DrawString(font2, "Block  Chance", new Vector2(240, 200), Color.Firebrick);
                spriteBatch.Draw(blockOneHandAdd.getState(), blockOneHandAdd.buttonBounds, Color.White);
                spriteBatch.Draw(blockOneHandSub.getState(), blockOneHandSub.buttonBounds, Color.White);

                // melee combat button and skill bars for 2h sword
                spriteBatch.DrawString(font2, "2h Sword", new Vector2(30, 350), Color.Firebrick);
                spriteBatch.Draw(skillBox, new Rectangle(240, 310, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 310, (tempSpeedTwoH + p1.getTwoHandSpeedSkill()) * 20, 10), Color.Red);
                if (tempSpeedTwoH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 310, tempSpeedTwoH + 2, 10), Color.Gold);
                }
                spriteBatch.DrawString(font2, "Sword  Speed", new Vector2(240, 270), Color.Firebrick);
                spriteBatch.Draw(swordSpeedAddTwoH.getState(), swordSpeedAddTwoH.buttonBounds, Color.White);
                spriteBatch.Draw(swordSpeedSubTwoH.getState(), swordSpeedSubTwoH.buttonBounds, Color.White);

                spriteBatch.DrawString(font2, "Sword Damage", new Vector2(240, 330), Color.Firebrick);
                spriteBatch.Draw(skillBox, new Rectangle(240, 370, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 370, (tempDamTwoH + p1.getTwoDamSkill()) * 20, 10), Color.Red);
                if (tempDamTwoH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 370, tempDamTwoH + 2, 10), Color.Gold);
                }
                spriteBatch.Draw(swordDamageAddTwoH.getState(), swordDamageAddTwoH.buttonBounds, Color.White);
                spriteBatch.Draw(swordDamageSubTwoH.getState(), swordDamageSubTwoH.buttonBounds, Color.White);
                spriteBatch.DrawString(font2, "Block  Chance", new Vector2(240, 390), Color.Firebrick);
                spriteBatch.Draw(skillBox, new Rectangle(240, 430, 210, 10), Color.Black);
                spriteBatch.Draw(skillBoxFill, new Rectangle(240, 430, (tempBlockTwoH + p1.getTwoHandBlockSkill()) * 20, 10), Color.Red);
                if (tempBlockTwoH == 10)
                {
                    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 430, tempBlockTwoH + 2, 10), Color.Gold);
                }
                spriteBatch.Draw(blockTwoHandedAdd.getState(), blockTwoHandedAdd.buttonBounds, Color.White);
                spriteBatch.Draw(blockTwoHandedSub.getState(), blockTwoHandedSub.buttonBounds, Color.White);
                spriteBatch.Draw(Warrior, new Vector2(620, 140), Color.White);
            
        }

        public void bonusW()
        {
            // bouns section
            spriteBatch.DrawString(font2, "Bonus", new Vector2(520, 50), Color.Navy);
            spriteBatch.DrawString(font2, bonusPoints + "", new Vector2(490, -6), Color.White);
            spriteBatch.DrawString(font2, tempSpeedOneH + "", new Vector2(560, 95), Color.Firebrick);
            spriteBatch.DrawString(font2, tempDamOneH + "", new Vector2(560, 155), Color.Firebrick);
            spriteBatch.DrawString(font2, tempBlockOneH + "", new Vector2(560, 215), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpeedTwoH + "", new Vector2(560, 285), Color.Firebrick);
            spriteBatch.DrawString(font2, tempDamTwoH + "", new Vector2(560, 345), Color.Firebrick);
            spriteBatch.DrawString(font2, tempBlockTwoH + "", new Vector2(560, 405), Color.Firebrick);

            if (swordSpeedAddOneH.isReleased() && oneHandSPoints != 0)
            {
                ++tempSpeedOneH;
                --bonusPoints;
                --oneHandSPoints;
            }
            else if (swordSpeedSubOneH.isReleased() && tempSpeedOneH > 0)
            {
                --tempSpeedOneH;
                ++bonusPoints;
                ++oneHandSPoints;
            }
            else if (swordDamageAddOneH.isReleased() && oneHandSPoints != 0)
            {
                ++tempDamOneH;
                --bonusPoints;
                --oneHandSPoints;
            }
            else if (swordDamageSubOneH.isReleased() && tempDamOneH > 0)
            {
                --tempDamOneH;
                ++bonusPoints;
                ++oneHandSPoints;
            }
            else if (blockOneHandAdd.isReleased() && oneHandSPoints != 0)
            {
                ++tempBlockOneH;
                --bonusPoints;
                --oneHandSPoints;
            }
            else if (blockOneHandSub.isReleased() && tempBlockOneH > 0)
            {
                --tempBlockOneH;
                ++bonusPoints;
                ++oneHandSPoints;
            }
            else if (swordSpeedAddTwoH.isReleased() && twoHandSPoints != 0)
            {
                ++tempSpeedTwoH;
                --bonusPoints;
                --twoHandSPoints;
            }
            else if (swordSpeedSubTwoH.isReleased() && tempSpeedTwoH > 0)
            {
                --tempSpeedTwoH;
                ++bonusPoints;
                ++twoHandSPoints;
            }
            else if (swordDamageAddTwoH.isReleased() && twoHandSPoints != 0)
            {
                ++tempDamTwoH;
                --bonusPoints;
                --twoHandSPoints;
            }
            else if (swordDamageSubTwoH.isReleased() && tempDamTwoH > 0)
            {
                --tempDamTwoH;
                ++bonusPoints;
                ++twoHandSPoints;
            }
            else if (blockTwoHandedAdd.isReleased() && twoHandSPoints != 0)
            {
                ++tempBlockTwoH;
                --bonusPoints;
                --twoHandSPoints;
            }
            else if (blockTwoHandedSub.isReleased() && tempBlockTwoH > 0)
            {
                --tempBlockTwoH;
                ++bonusPoints;
                ++twoHandSPoints;
            }
        }

        public override void Draw(GameTime gameTime)
        {
                spriteBatch.Draw(Back.image, Back.bounds, Color.White);
                spriteBatch.DrawString(font2, p1.getName(), new Vector2(0, 0), Color.White);
                bonusW();
                skillTreeW(gameTime);
            
            base.Draw(gameTime);
        }
    }
}
