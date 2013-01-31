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
    class ArcherSkillSet : Screen
    {
        int tempLongBowDam, tempLongBowSpe, tempLongBowdodge, tempShortBowDam, tempShortBowSpe, tempShortBowDodge, oneHandSPoints,
            twoHandSPoints, combatMagicSPoints, healMagicSPoints, longBowSPoints, shortBowSPoints;

        button longBowSpeedAdd, longBowSpeedSub, longBowDamageAdd, longBowDamageSub,
            longBowDodgeAdd, longBowDodgeSub, shortBowSpeedAdd, shortBowSpeedSub, shortBowDamageAdd, shortBowDamageSub, shortBowDodgeAdd, shortBowDodgeSub;

        Player p1;
        Texture2D Archer;
        Texture2D skillBox;
        Texture2D skillBoxFill;

        public ArcherSkillSet(Game game, ref Player p1)
            : base(game)
        {

        }

        private void setPicOfClass()
        {
            Archer = Content.Load<Texture2D>(@"CharPics\Archer(2)");
        }

        private void createButtons()
        {
            // setting up the buttons for adding and substracting for skills and stats
            Texture2D add = Content.Load<Texture2D>(@"Buttons\add"), add2 = Content.Load<Texture2D>(@"Buttons\add(2)"), add3 = Content.Load<Texture2D>(@"Buttons\add(3)");
            Texture2D sub = Content.Load<Texture2D>(@"Buttons\subtract"), sub2 = Content.Load<Texture2D>(@"Buttons\subtract(2)"), sub3 = Content.Load<Texture2D>(@"Buttons\subtract(3)");

            // LongBow 
            longBowSpeedSub = new button(195, 100, sub, sub2, sub3);
            longBowSpeedAdd = new button(455, 100, add, add2, add3);
            longBowDamageSub = new button(195, 160, sub, sub2, sub3);
            longBowDamageAdd = new button(455, 160, add, add2, add3);
            longBowDodgeSub = new button(195, 220, sub, sub2, sub3);
            longBowDodgeAdd = new button(455, 220, add, add2, add3);

            // ShortBow
            shortBowSpeedSub = new button(195, 290, sub, sub2, sub3);
            shortBowSpeedAdd = new button(455, 290, add, add2, add3);
            shortBowDamageSub = new button(195, 350, sub, sub2, sub3);
            shortBowDamageAdd = new button(455, 350, add, add2, add3);
            shortBowDodgeSub = new button(195, 410, sub, sub2, sub3);
            shortBowDodgeAdd = new button(455, 410, add, add2, add3);

            // skill tree box to show your progression on that skill
            skillBox = Content.Load<Texture2D>(@"Box");
            skillBoxFill = Content.Load<Texture2D>(@"Box");
        }

        public int longBowSkillPoints()
        {
            longBowSPoints = p1.longBowSkillPoints();
            return longBowSPoints;
        }

        public int shortBowSKillPoints()
        {
            shortBowSPoints = p1.shortBowSkillPoints();
            return shortBowSPoints;
        }

        public void skillTreeA()
        {
            // Archer
            // long bow speed          
            spriteBatch.DrawString(font2, "Long  Bow", new Vector2(20, 160), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 120, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 120, tempLongBowSpe * 20, 10), Color.Red);
            if (tempLongBowSpe == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 120, tempLongBowSpe + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "LBow  Speed", new Vector2(240, 80), Color.Firebrick);
            spriteBatch.Draw(longBowSpeedAdd.getState(), longBowSpeedAdd.buttonBounds, Color.White);
            spriteBatch.Draw(longBowSpeedSub.getState(), longBowSpeedSub.buttonBounds, Color.White);

            // long bow damage
            spriteBatch.Draw(skillBox, new Rectangle(240, 180, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 180, tempLongBowDam * 20, 10), Color.Red);
            if (tempLongBowDam == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 180, tempLongBowDam + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "LBow Damage", new Vector2(240, 140), Color.Firebrick);
            spriteBatch.Draw(longBowDamageAdd.getState(), longBowDamageAdd.buttonBounds, Color.White);
            spriteBatch.Draw(longBowDamageSub.getState(), longBowDamageSub.buttonBounds, Color.White);

            // long bow dodge
            spriteBatch.Draw(skillBox, new Rectangle(240, 240, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 240, tempLongBowdodge * 20, 10), Color.Red);
            if (tempLongBowdodge == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 240, tempLongBowdodge + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "LBow  Dodge", new Vector2(240, 200), Color.Firebrick);
            spriteBatch.Draw(longBowDodgeAdd.getState(), longBowDodgeAdd.buttonBounds, Color.White);
            spriteBatch.Draw(longBowDodgeSub.getState(), longBowDodgeSub.buttonBounds, Color.White);

            // short bow speed
            spriteBatch.DrawString(font2, "Short Bow", new Vector2(20, 350), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 310, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 310, tempShortBowSpe * 20, 10), Color.Red);
            if (tempShortBowSpe == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 310, tempShortBowSpe + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "SBow  Speed", new Vector2(240, 270), Color.Firebrick);
            spriteBatch.Draw(shortBowSpeedAdd.getState(), shortBowSpeedAdd.buttonBounds, Color.White);
            spriteBatch.Draw(shortBowSpeedSub.getState(), shortBowSpeedSub.buttonBounds, Color.White);

            // short bow damage
            spriteBatch.DrawString(font2, "SBow Damage", new Vector2(240, 330), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 370, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 370, tempShortBowDam * 20, 10), Color.Red);
            if (tempShortBowDam == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 370, tempShortBowDam + 2, 10), Color.Gold);
            }
            spriteBatch.Draw(shortBowDamageAdd.getState(), shortBowDamageAdd.buttonBounds, Color.White);
            spriteBatch.Draw(shortBowDamageSub.getState(), shortBowDamageSub.buttonBounds, Color.White);

            // short bow dodge
            spriteBatch.DrawString(font2, "Block  Chance", new Vector2(240, 390), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 430, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 430, tempShortBowDodge * 20, 10), Color.Red);
            if (tempShortBowDodge == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 430, tempShortBowDodge + 2, 10), Color.Gold);
            }
            spriteBatch.Draw(shortBowDodgeAdd.getState(), shortBowDodgeAdd.buttonBounds, Color.White);
            spriteBatch.Draw(shortBowDodgeSub.getState(), shortBowDodgeSub.buttonBounds, Color.White);
            spriteBatch.Draw(Archer, new Vector2(620, 140), Color.White);
        }

   /*     public void bonusA()
        {
            // bouns section
            spriteBatch.DrawString(font2, "Bonus", new Vector2(520, 50), Color.Navy);
            spriteBatch.DrawString(font2, bonusPoints + "", new Vector2(490, -6), Color.White);
            spriteBatch.DrawString(font2, tempLongBowSpe + "", new Vector2(560, 95), Color.Firebrick);
            spriteBatch.DrawString(font2, tempLongBowDam + "", new Vector2(560, 155), Color.Firebrick);
            spriteBatch.DrawString(font2, tempLongBowdodge + "", new Vector2(560, 215), Color.Firebrick);
            spriteBatch.DrawString(font2, tempShortBowSpe + "", new Vector2(560, 285), Color.Firebrick);
            spriteBatch.DrawString(font2, tempShortBowDam + "", new Vector2(560, 345), Color.Firebrick);
            spriteBatch.DrawString(font2, tempShortBowDodge + "", new Vector2(560, 405), Color.Firebrick);

            if (longBowSpeedAdd.isReleased() && longBowSPoints != 0)
            {
                ++tempLongBowSpe;
                --bonusPoints;
                --longBowSPoints;
            }
            else if (longBowSpeedSub.isReleased() && tempLongBowSpe > 0)
            {
                --tempLongBowSpe;
                ++bonusPoints;
                ++longBowSPoints;
            }
            else if (longBowDamageAdd.isReleased() && longBowSPoints != 0)
            {
                ++tempLongBowDam;
                --bonusPoints;
                --longBowSPoints;
            }
            else if (longBowDamageSub.isReleased() && tempLongBowDam > 0)
            {
                --tempLongBowDam;
                ++bonusPoints;
                ++longBowSPoints;
            }
            else if (longBowDodgeAdd.isReleased() && longBowSPoints != 0)
            {
                ++tempLongBowdodge;
                --bonusPoints;
                --longBowSPoints;
            }
            else if (longBowDodgeSub.isReleased() && tempLongBowdodge > 0)
            {
                --tempLongBowdodge;
                ++bonusPoints;
                ++longBowSPoints;
            }
            else if (shortBowSpeedAdd.isReleased() && shortBowSPoints != 0)
            {
                ++tempShortBowSpe;
                --bonusPoints;
                --shortBowSPoints;
            }
            else if (shortBowSpeedSub.isReleased() && tempShortBowSpe > 0)
            {
                --tempShortBowSpe;
                ++bonusPoints;
                ++shortBowSPoints;
            }
            else if (shortBowDamageAdd.isReleased() && shortBowSPoints != 0)
            {
                ++tempShortBowDam;
                --bonusPoints;
                --shortBowSPoints;
            }
            else if (shortBowDamageSub.isReleased() && tempShortBowDam > 0)
            {
                --tempShortBowDam;
                ++bonusPoints;
                ++shortBowSPoints;
            }
            else if (shortBowDodgeAdd.isReleased() && shortBowSPoints != 0)
            {
                ++tempShortBowDodge;
                --bonusPoints;
                --shortBowSPoints;
            }
            else if (shortBowDodgeSub.isReleased() && tempShortBowDodge > 0)
            {
                --tempShortBowDodge;
                ++bonusPoints;
                ++shortBowSPoints;
            }
        }*/
    }
}
