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
    class MageSkillSet : Screen
    {
        int tempSpellSpeedC, tempSpellPowerC, tempSpellCostC, tempSpellSpeedH, tempSpellPowerH,
            tempSpellCostH, combatMagicSPoints, healMagicSPoints, bonusPoints;

        button spellSpeedAddC, spellSpeedSubC, spellPowerAddC, spellPowerSubC, spellCostAddC, spellCostSubC,
            spellSpeedAddH, spellSpeedSubH, spellPowerAddH, spellPowerSubH, spellCostAddH, spellCostSubH;

        Player p1;
        Texture2D mage;
        Texture2D skillBox;
        Texture2D skillBoxFill;

        public MageSkillSet(Game game, ref Player p1)
            : base(game)
        {

        }

        private void setPicOfClass()
        {
            mage = Content.Load<Texture2D>(@"CharPics\Mage(2)");
        }

        private void createButtons()
        {
            Texture2D add = Content.Load<Texture2D>(@"Buttons\add"), add2 = Content.Load<Texture2D>(@"Buttons\add(2)"), add3 = Content.Load<Texture2D>(@"Buttons\add(3)");
            Texture2D sub = Content.Load<Texture2D>(@"Buttons\subtract"), sub2 = Content.Load<Texture2D>(@"Buttons\subtract(2)"), sub3 = Content.Load<Texture2D>(@"Buttons\subtract(3)");

            // magic combat 
            spellSpeedSubC = new button(195, 100, sub, sub2, sub3);
            spellSpeedAddC = new button(455, 100, add, add2, add3);
            spellPowerSubC = new button(195, 160, sub, sub2, sub3);
            spellPowerAddC = new button(455, 160, add, add2, add3);
            spellCostSubC = new button(195, 220, sub, sub2, sub3);
            spellCostAddC = new button(455, 220, add, add2, add3);

            // magic heal
            spellSpeedSubH = new button(195, 290, sub, sub2, sub3);
            spellSpeedAddH = new button(455, 290, add, add2, add3);
            spellPowerSubH = new button(195, 350, sub, sub2, sub3);
            spellPowerAddH = new button(455, 350, add, add2, add3);
            spellCostSubH = new button(195, 410, sub, sub2, sub3);
            spellCostAddH = new button(455, 410, add, add2, add3);

            // skill tree box to show your progression on that skill
            skillBox = Content.Load<Texture2D>(@"Box");
            skillBoxFill = Content.Load<Texture2D>(@"Box");
        }

        public int combatMagicSkillPoints()
        {
            combatMagicSPoints = p1.combatMagicSkillPoints();
            return combatMagicSPoints;
        }

        public int healingMagicSkillPoints()
        {
            healMagicSPoints = p1.healingMagicSkillPoints();
            return healMagicSPoints;
        }

        public void skillTreeM()
        {
            // spell speed combat         
            spriteBatch.DrawString(font2, "Spell  Combat", new Vector2(10, 160), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 120, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 120, tempSpellSpeedC * 20, 10), Color.Red);
            if (tempSpellSpeedC == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 120, tempSpellSpeedC + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "Spell  Speed", new Vector2(240, 80), Color.Firebrick);
            spriteBatch.Draw(spellSpeedAddC.getState(), spellSpeedAddC.buttonBounds, Color.White);
            spriteBatch.Draw(spellSpeedSubC.getState(), spellSpeedSubC.buttonBounds, Color.White);
            // spell power combat
            spriteBatch.Draw(skillBox, new Rectangle(240, 180, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 180, tempSpellPowerC * 20, 10), Color.Red);
            if (tempSpellPowerC == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 180, tempSpellPowerC + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "Spell Power", new Vector2(240, 140), Color.Firebrick);
            spriteBatch.Draw(spellPowerAddC.getState(), spellPowerAddC.buttonBounds, Color.White);
            spriteBatch.Draw(spellPowerSubC.getState(), spellPowerSubC.buttonBounds, Color.White);
            // spell cost combat
            spriteBatch.Draw(skillBox, new Rectangle(240, 240, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 240, tempSpellCostC * 20, 10), Color.Red);
            if (tempSpellCostC == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 240, tempSpellCostC + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "Spell  Cost", new Vector2(240, 200), Color.Firebrick);
            spriteBatch.Draw(spellCostAddC.getState(), spellCostAddC.buttonBounds, Color.White);
            spriteBatch.Draw(spellCostSubC.getState(), spellCostSubC.buttonBounds, Color.White);

            // spell speed health
            spriteBatch.DrawString(font2, "Spell Healing", new Vector2(10, 350), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 310, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 310, tempSpellSpeedH * 20, 10), Color.Red);
            if (tempSpellSpeedH == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 310, tempSpellSpeedH + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "Spell Speed", new Vector2(240, 270), Color.Firebrick);
            spriteBatch.Draw(spellSpeedAddH.getState(), spellSpeedAddH.buttonBounds, Color.White);
            spriteBatch.Draw(spellSpeedSubH.getState(), spellSpeedSubH.buttonBounds, Color.White);
            // spell power health
            spriteBatch.DrawString(font2, "Spell Power", new Vector2(240, 330), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 370, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 370, tempSpellPowerH * 20, 10), Color.Red);
            if (tempSpellPowerH == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 370, tempSpellPowerH + 2, 10), Color.Gold);
            }
            spriteBatch.Draw(spellPowerAddH.getState(), spellPowerAddH.buttonBounds, Color.White);
            spriteBatch.Draw(spellPowerSubH.getState(), spellPowerSubH.buttonBounds, Color.White);
            // spell cost health
            spriteBatch.DrawString(font2, "Spell  Cost", new Vector2(240, 390), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 430, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 430, tempSpellCostH * 20, 10), Color.Red);
            if (tempSpellCostH == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 430, tempSpellCostH + 2, 10), Color.Gold);
            }
            spriteBatch.Draw(spellCostAddH.getState(), spellCostAddH.buttonBounds, Color.White);
            spriteBatch.Draw(spellCostSubH.getState(), spellCostSubH.buttonBounds, Color.White);
            spriteBatch.Draw(mage, new Vector2(620, 140), Color.White);
        }
        public void bonusM()
        {
            // bouns section
            spriteBatch.DrawString(font2, "Bonus", new Vector2(520, 50), Color.Navy);
            spriteBatch.DrawString(font2, bonusPoints + "", new Vector2(490, -6), Color.White);
            spriteBatch.DrawString(font2, tempSpellSpeedC + "", new Vector2(560, 95), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpellPowerC + "", new Vector2(560, 155), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpellCostC + "", new Vector2(560, 215), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpellSpeedH + "", new Vector2(560, 285), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpellPowerH + "", new Vector2(560, 345), Color.Firebrick);
            spriteBatch.DrawString(font2, tempSpellCostH + "", new Vector2(560, 405), Color.Firebrick);

            if (spellSpeedAddC.isReleased() && combatMagicSPoints != 0)
            {
                ++tempSpellSpeedC;
                --bonusPoints;
                --combatMagicSPoints;
            }
            else if (spellSpeedSubC.isReleased() && tempSpellSpeedC > 0)
            {
                --tempSpellSpeedC;
                ++bonusPoints;
                ++combatMagicSPoints;
            }
            else if (spellPowerAddC.isReleased() && combatMagicSPoints != 0)
            {
                ++tempSpellPowerC;
                --bonusPoints;
                --combatMagicSPoints;
            }
            else if (spellPowerSubC.isReleased() && tempSpellPowerC > 0)
            {
                --tempSpellPowerC;
                ++bonusPoints;
                ++combatMagicSPoints;
            }
            else if (spellCostAddC.isReleased() && combatMagicSPoints != 0)
            {
                ++tempSpellCostC;
                --bonusPoints;
                --combatMagicSPoints;
            }
            else if (spellCostSubC.isReleased() && tempSpellCostC > 0)
            {
                --tempSpellCostC;
                ++bonusPoints;
                ++combatMagicSPoints;
            }
            else if (spellSpeedAddH.isReleased() && healMagicSPoints != 0)
            {
                ++tempSpellSpeedH;
                --bonusPoints;
                --healMagicSPoints;
            }
            else if (spellSpeedSubH.isReleased() && tempSpellSpeedH > 0)
            {
                --tempSpellSpeedH;
                ++bonusPoints;
                ++healMagicSPoints;
            }
            else if (spellPowerAddH.isReleased() && healMagicSPoints != 0)
            {
                ++tempSpellPowerH;
                --bonusPoints;
                --healMagicSPoints;
            }
            else if (spellPowerSubH.isReleased() && tempSpellPowerH > 0)
            {
                --tempSpellPowerH;
                ++bonusPoints;
                ++healMagicSPoints;
            }
            else if (spellCostAddH.isReleased() && healMagicSPoints != 0)
            {
                ++tempSpellCostH;
                --bonusPoints;
                --healMagicSPoints;
            }
            else if (spellCostSubH.isReleased() && tempSpellCostH > 0)
            {
                --tempSpellCostH;
                ++bonusPoints;
                ++healMagicSPoints;
            }
        }
    }
}
