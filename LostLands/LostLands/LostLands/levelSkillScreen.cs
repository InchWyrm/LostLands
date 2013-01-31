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
    class levelSkillScreen : Screen
    {
        short points;
        // to many int lol
        int tempstr, tempcon, tempdex, tempwis, tempspr, bonusPoints, tempSpeedOneH, tempDamOneH, tempBlockOneH, tempSpeedTwoH, tempDamTwoH, tempBlockTwoH, tempSpellSpeedC, tempSpellPowerC,
            tempSpellCostC, tempSpellSpeedH, tempSpellPowerH, tempSpellCostH, tempLongBowDam, tempLongBowSpe, tempLongBowdodge, tempShortBowDam, tempShortBowSpe, tempShortBowDodge, oneHandSPoints,
            twoHandSPoints, combatMagicSPoints, healMagicSPoints, longBowSPoints, shortBowSPoints, 
            // count timer for how long the commitment button will stay highlighted
            count = 50, 
            // counters for giving the yellow cap to the skill bar after hitting 10 points in that skill
            oneHandedCounter, oneDamageCounter, oneBlockCounter, twoSpeedCounter, twoDamageCounter,twoBlockCounter;

        // and here we have lots of buttons and i think there will be more
        button strengthAdd, strengthSub, dexAdd, dexSub, constAdd, constSub, wisAdd, wisSub, spiritAdd, spiritSub, reset, skillExit, commit, backToLevel, skills,
            swordSpeedAddOneH, swordSpeedSubOneH, swordSpeedAddTwoH, swordSpeedSubTwoH, swordDamageSubOneH, swordDamageAddOneH, swordDamageAddTwoH, swordDamageSubTwoH,
            blockOneHandAdd, blockOneHandSub, blockTwoHandedAdd, blockTwoHandedSub, spellSpeedAddC, spellSpeedSubC, spellPowerAddC, spellPowerSubC, spellCostAddC, spellCostSubC,
            spellSpeedAddH, spellSpeedSubH, spellPowerAddH, spellPowerSubH, spellCostAddH, spellCostSubH, longBowSpeedAdd, longBowSpeedSub, longBowDamageAdd, longBowDamageSub,
            longBowDodgeAdd, longBowDodgeSub, shortBowSpeedAdd, shortBowSpeedSub, shortBowDamageAdd, shortBowDamageSub, shortBowDodgeAdd, shortBowDodgeSub;
        
        Player p1;
        BackgroundImage Back;
        KeyboardState   oldk;
        Rectangle strength, cons, dex, wis, spi;
        Texture2D warrior;
        Texture2D mage;
        Texture2D Archer;
        Texture2D skillBox;
        Texture2D skillBoxFill;
        SkillSelect skillMelee, skillMagic, skillArcher, CommitSkills;

        public bool hidenlSScreen = true, switchedTo;
        public bool hidenSkill = false;

        public levelSkillScreen(Game game, ref Player p1)
            : base(game)
        {
            // starting buttons for skill sets for melee 
            skillMelee = new SkillSelect(game, 25, 510, 210, 50, "Melee Ability");
            skillMelee.activate();
           // skillMagic = new SkillSelect(game, 255, 510, 210, 50, "Mage  Ability");
            //skillArcher = new SkillSelect(game, 485, 510, 210, 50, "Archer Ability");
            CommitSkills = new SkillSelect(game, 205, 452, 300, 50, "       Skill  Commit");// this has large space to that the words sit in middle of button
            Back = new BackgroundImage(Content.Load<Texture2D>(@"Backgrounds\CreationBackground"));
            createButtons();
            this.p1 = p1;
        }

        #region createButtons
        private void createButtons()
        {
            // setting up the buttons for adding and substracting for skills and stats
            Texture2D add = Content.Load<Texture2D>(@"Buttons\add"), add2 = Content.Load<Texture2D>(@"Buttons\add(2)"), add3 = Content.Load<Texture2D>(@"Buttons\add(3)");
            Texture2D sub = Content.Load<Texture2D>(@"Buttons\subtract"), sub2 = Content.Load<Texture2D>(@"Buttons\subtract(2)"), sub3 = Content.Load<Texture2D>(@"Buttons\subtract(3)");

            // stat buttons layout 
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

            // 1h sword add and sub buttons layout
            swordSpeedSubOneH = new button(195, 100, sub, sub2, sub3);
            swordSpeedAddOneH = new button(455, 100, add, add2, add3);
            swordDamageSubOneH = new button(195, 160, sub, sub2, sub3);
            swordDamageAddOneH = new button(455, 160, add, add2, add3);
            blockOneHandSub = new button(195, 220, sub, sub2, sub3);
            blockOneHandAdd = new button(455, 220, add, add2, add3);

            // 2h sword add and sub buttons layout
            //swordSpeedSubTwoH = new button(195, 290, sub, sub2, sub3);
            //swordSpeedAddTwoH = new button(455, 290, add, add2, add3);
            //swordDamageSubTwoH = new button(195, 350, sub, sub2, sub3);
            //swordDamageAddTwoH = new button(455, 350, add, add2, add3);
            //blockTwoHandedSub = new button(195, 410, sub, sub2, sub3);
            //blockTwoHandedAdd = new button(455, 410, add, add2, add3);

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

            // skill button on character stat screen
            skillExit = new button(800 - 129, 0, Content.Load<Texture2D>(@"Buttons\UIExit"), Content.Load<Texture2D>(@"Buttons\UIExit(2)"), Content.Load<Texture2D>(@"Buttons\UIExit(3)"));
            reset = new button(465, 490, Content.Load<Texture2D>(@"Buttons\resetStats"), Content.Load<Texture2D>(@"Buttons\resetStats(2)"), Content.Load<Texture2D>(@"Buttons\resetStats(3)"));
            commit = new button(160, 490, Content.Load<Texture2D>(@"Buttons\Commit"), Content.Load<Texture2D>(@"Buttons\Commit(2)"), Content.Load<Texture2D>(@"Buttons\Commit(3)"));
            skills = new button(640, 490, Content.Load<Texture2D>(@"buttons\UISkillButton"), Content.Load<Texture2D>(@"buttons\UISkillButton(2)"), Content.Load<Texture2D>(@"buttons\UISkillButton(3)"));

            // stat boxes for leveling up
            strength = new Rectangle(280, 285, 120, 40);
            cons = new Rectangle(280, 325, 150, 40);
            dex = new Rectangle(280, 365, 140, 40);
            wis = new Rectangle(280, 405, 120, 40);
            spi = new Rectangle(280, 445, 85, 40);

            // stat button on skill screen
            //backToLevel = new button(690, 517, Content.Load<Texture2D>(@"buttons\levelButton"), Content.Load<Texture2D>(@"buttons\levelButton(2)"), Content.Load<Texture2D>(@"buttons\levelButton(3)"));
            setPicOfClass();

            // skill tree box to show your progression on that skill
            skillBox = Content.Load<Texture2D>(@"Box");
            skillBoxFill = Content.Load<Texture2D>(@"Box");
        }

        public void classPic()
        {

        }
        #endregion

        public void skillTreeOption(GameTime gameTime)
        {

            // to switch between screens
            if (skillExit.isReleased() || Keyboard.GetState().IsKeyUp(Keys.L) && oldk.IsKeyDown(Keys.L))
            {
                hidenlSScreen = true;
                whenSwitched();
            }
            //if (backToLevel.isReleased() || Keyboard.GetState().IsKeyUp(Keys.O) && oldk.IsKeyDown(Keys.O))
            //   hidenSkill = !hidenSkill;

            //loading buttons and player name and general pic of class
            spriteBatch.Draw(Back.image, Back.bounds, Color.White);
            spriteBatch.Draw(skillExit.getState(), skillExit.buttonBounds, Color.White);
            //spriteBatch.Draw(backToLevel.getState(), backToLevel.buttonBounds, Color.White);
            spriteBatch.DrawString(font2, p1.getName(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font2, "Spend your skill points to get bonuses to your characters stats. ", new Vector2(0, 560), Color.White);
            spriteBatch.DrawString(font2, "Skill Points Total:", new Vector2(225, 0), Color.White);
            commitSkillPointsToPlayer();
            // starting buttons for skill sets for melee 
            skillMelee.Draw(gameTime);
            // spell button and skill tree for magic
            //skillMagic.Draw(gameTime);
            // acrher button and skill tree
           // skillArcher.Draw(gameTime);
            // commit button being drawn after points are spent
            if (skillPointSpent() == true)
            {
                CommitSkills.Draw(gameTime);
            }
        }

        public void commitSkillPointsToPlayer()
        {
            if (CommitSkills.isReleased()) { count = 50; }
            if (CommitSkills.isActive())
            {
                #region one handed speed, damage and block
                if (tempSpeedOneH >= 1 && tempSpeedOneH <=10)
                {
                    //p1.changeStats(0, 0, 1, 0, 0);
                    p1.setOneSwordSpeedSKill(tempSpeedOneH);//Burden
                    tempSpeedOneH = 0;
                }
                if (tempDamOneH >= 1)
                {
                    //p1.changeStats(1, 0, 0, 0, 0);
                    p1.setOneSwordDamSkill(tempDamOneH);//Damage
                    tempDamOneH = 0; 
                }
                if (tempBlockOneH >= 1)
                {
                    //p1.changeStats(0, 0, 1, 0, 0);
                    p1.setOneSwordBlockSkill(tempBlockOneH);//Accuracy
                    tempBlockOneH = 0;
                }
                # endregion

                #region two handed speed, damage and block
                if (tempSpeedTwoH >= 1)
                {
                    p1.changeStats(0, 0, 1, 0, 0);
                    p1.settwoHandSpeedSkill(tempSpeedTwoH);
                    tempSpeedTwoH = 0;
                }
                if (tempDamTwoH >= 1)
                {
                    p1.changeStats(1, 0, 0, 0, 0);
                    p1.setTwoDamSkill(tempDamTwoH);
                    tempDamTwoH = 0;
                }
                if (tempBlockTwoH >= 1)
                {
                    p1.changeStats(0, 0, 1, 0, 0);
                    p1.setTwoHandBlockSkill(tempBlockTwoH);
                    tempBlockTwoH = 0;
                }
                #endregion

                #region spell combat damage, speed, and cost
                if (tempSpellPowerC >= 1)
                {
                    p1.changeStats(0, 0, 0, 1, 0);
                    p1.setCombatMagicDamageSkill(tempSpellPowerC);
                    tempSpellPowerC = 0;
                }
                if (tempSpellSpeedC >= 1)
                {
                    // p1.changeStats(0, 0, 1, 0, 0);// just put this in so i remember it needs to be something different than stat points
                    p1.setCombatMagicSpeedSkill(tempSpellSpeedC);
                    tempSpellSpeedC = 0;
                }
                if (tempSpellCostC >= 1)
                {
                    p1.changeStats(0, 0, 0, 0, 1);
                    p1.setCombatMagicCostSkill(tempSpellCostC);
                    tempSpellCostC = 0;
                }
                #endregion

                #region spell health power, speed and cost
                if (tempSpellPowerH >= 1)
                {
                    p1.changeStats(0, 0, 0, 1, 0);
                    p1.setHealthMagicPowerSkill(tempSpellPowerH);
                    tempSpellPowerH = 0;
                }
                if (tempSpellSpeedH >= 1)
                {
                    // p1.changeStats(0, 0, 0, 0, 1);// just put this in so i remember it needs to be something different than stat points
                    p1.setHealthMagicSpeedSkill(tempSpellSpeedH);
                    tempSpellSpeedH = 0;
                }
                if (tempSpellCostH >= 1)
                {
                    p1.changeStats(0, 0, 0, 0, 1);
                    p1.setHealthMagicCostSkill(tempSpellCostH);
                    tempSpellCostH = 0;
                }

                #endregion

                #region long bow damage, speed and dodge

                #endregion

                #region short bow damage, speed and dodge

                #endregion

                CommitSkills.deActivate();
            }
        }

        public void skillTreeW()
        {
            // starting buttons for skill sets for melee 
            // melee combat buttons and skill bar for 1h sword           
            spriteBatch.DrawString(font2, "1h  Sword", new Vector2(30, 160), Color.Firebrick);
            spriteBatch.Draw(skillBox, new Rectangle(240, 120, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 120, (tempSpeedOneH + p1.getOneSwordSpeed()) * 20, 10), Color.Red);
            if (oneHandedCounter == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 120, oneHandedCounter + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "       Burden", new Vector2(240, 80), Color.Firebrick);
            spriteBatch.Draw(swordSpeedAddOneH.getState(), swordSpeedAddOneH.buttonBounds, Color.White);
            spriteBatch.Draw(swordSpeedSubOneH.getState(), swordSpeedSubOneH.buttonBounds, Color.White);
            spriteBatch.Draw(skillBox, new Rectangle(240, 180, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 180, (tempDamOneH + p1.getOneSwordDam()) * 20, 10), Color.Red);
            if (oneDamageCounter == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 180, oneDamageCounter + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "       Damage", new Vector2(240, 140), Color.Firebrick);
            spriteBatch.Draw(swordDamageAddOneH.getState(), swordDamageAddOneH.buttonBounds, Color.White);
            spriteBatch.Draw(swordDamageSubOneH.getState(), swordDamageSubOneH.buttonBounds, Color.White);
            spriteBatch.Draw(skillBox, new Rectangle(240, 240, 210, 10), Color.Black);
            spriteBatch.Draw(skillBoxFill, new Rectangle(240, 240, (tempBlockOneH + p1.getOneSwordBlock()) * 20, 10), Color.Red);
            if (oneBlockCounter == 10)
            {
                spriteBatch.Draw(skillBoxFill, new Rectangle(440, 240, oneBlockCounter + 2, 10), Color.Gold);
            }
            spriteBatch.DrawString(font2, "      Accuracy", new Vector2(240, 200), Color.Firebrick);
            spriteBatch.Draw(blockOneHandAdd.getState(), blockOneHandAdd.buttonBounds, Color.White);
            spriteBatch.Draw(blockOneHandSub.getState(), blockOneHandSub.buttonBounds, Color.White);

            // melee combat button and skill bars for 2h sword
            //spriteBatch.DrawString(font2, "2h Sword", new Vector2(30, 350), Color.Firebrick);
            //spriteBatch.Draw(skillBox, new Rectangle(240, 310, 210, 10), Color.Black);
            //spriteBatch.Draw(skillBoxFill, new Rectangle(240, 310, (tempSpeedTwoH + p1.getTwoHandSpeedSkill()) * 20, 10), Color.Red);
            //if (twoSpeedCounter == 10)
            //{
            //    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 310, twoSpeedCounter + 2, 10), Color.Gold);
            //}
            //spriteBatch.DrawString(font2, "Sword  Speed", new Vector2(240, 270), Color.Firebrick);
            //spriteBatch.Draw(swordSpeedAddTwoH.getState(), swordSpeedAddTwoH.buttonBounds, Color.White);
            //spriteBatch.Draw(swordSpeedSubTwoH.getState(), swordSpeedSubTwoH.buttonBounds, Color.White);

            //spriteBatch.DrawString(font2, "Sword Damage", new Vector2(240, 330), Color.Firebrick);
            //spriteBatch.Draw(skillBox, new Rectangle(240, 370, 210, 10), Color.Black);
            //spriteBatch.Draw(skillBoxFill, new Rectangle(240, 370, (tempDamTwoH + p1.getTwoDamSkill()) * 20, 10), Color.Red);
            //if (twoDamageCounter == 10)
            //{
            //    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 370, twoDamageCounter + 2, 10), Color.Gold);
            //}
            //spriteBatch.Draw(swordDamageAddTwoH.getState(), swordDamageAddTwoH.buttonBounds, Color.White);
            //spriteBatch.Draw(swordDamageSubTwoH.getState(), swordDamageSubTwoH.buttonBounds, Color.White);
            //spriteBatch.DrawString(font2, "Block  Chance", new Vector2(240, 390), Color.Firebrick);
            //spriteBatch.Draw(skillBox, new Rectangle(240, 430, 210, 10), Color.Black);
            //spriteBatch.Draw(skillBoxFill, new Rectangle(240, 430, (tempBlockTwoH + p1.getTwoHandBlockSkill()) * 20, 10), Color.Red);
            //if (twoBlockCounter == 10)
            //{
            //    spriteBatch.Draw(skillBoxFill, new Rectangle(440, 430, twoBlockCounter + 2, 10), Color.Gold);
            //}
            //spriteBatch.Draw(blockTwoHandedAdd.getState(), blockTwoHandedAdd.buttonBounds, Color.White);
            //spriteBatch.Draw(blockTwoHandedSub.getState(), blockTwoHandedSub.buttonBounds, Color.White);
            //spriteBatch.Draw(warrior, new Vector2(620, 140), Color.White);
        }

        #region UnusedSkillTrees

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

        #endregion

        public void bonusTemp()
        {
            bonusPoints = p1.playerSkillPoints();
            tempSpeedOneH = 0;
            tempDamOneH = 0;
            tempBlockOneH = 0;
            tempSpeedTwoH = 0;
            tempDamTwoH = 0;
            tempBlockTwoH = 0;
            tempSpellSpeedC = 0;
            tempSpellPowerC = 0;
            tempSpellCostC = 0;
            tempSpellSpeedH = 0;
            tempSpellPowerH = 0;
            tempSpellCostH = 0;
            tempLongBowDam = 0;
            tempLongBowSpe = 0;
            tempLongBowdodge = 0;
            tempShortBowDam = 0;
            tempShortBowSpe = 0;
            tempShortBowDodge = 0;
        }

        private bool skillPointSpent()
        {
            if (
            tempSpeedOneH != 0 ||
            tempDamOneH != 0 ||
            tempBlockOneH != 0 ||
            tempSpeedTwoH != 0 ||
            tempDamTwoH != 0 ||
            tempBlockTwoH != 0 ||
            tempSpellSpeedC != 0 ||
            tempSpellPowerC != 0 ||
            tempSpellCostC != 0 ||
            tempSpellSpeedH != 0 ||
            tempSpellPowerH != 0 ||
            tempSpellCostH != 0 ||
            tempLongBowDam != 0 ||
            tempLongBowSpe != 0 ||
            tempLongBowdodge != 0 ||
            tempShortBowDam != 0 ||
            tempShortBowSpe != 0 ||
            tempShortBowDodge != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int oneHandSwordSkillPoints()
        {
            oneHandSPoints = p1.oneHandSkillPoints();
            return oneHandSPoints;
        }

        #region UnusedSkillMethods

        public int twoHandedSwordSkillPoints()
        {
            twoHandSPoints = p1.twoHandSkillPoints();
            return twoHandSPoints;
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

        #endregion

        public void bonusW()
        {
            // bouns section
            spriteBatch.DrawString(font2, "Bonus", new Vector2(520, 50), Color.Navy);
            spriteBatch.DrawString(font2, bonusPoints + "", new Vector2(490, -6), Color.White);
            spriteBatch.DrawString(font2, tempSpeedOneH + "", new Vector2(560, 95), Color.Firebrick);
            spriteBatch.DrawString(font2, tempDamOneH + "", new Vector2(560, 155), Color.Firebrick);
            spriteBatch.DrawString(font2, tempBlockOneH + "", new Vector2(560, 215), Color.Firebrick);
            //spriteBatch.DrawString(font2, tempSpeedTwoH + "", new Vector2(560, 285), Color.Firebrick);
            //spriteBatch.DrawString(font2, tempDamTwoH + "", new Vector2(560, 345), Color.Firebrick);
            //spriteBatch.DrawString(font2, tempBlockTwoH + "", new Vector2(560, 405), Color.Firebrick);

            if (swordSpeedAddOneH.isReleased() && oneHandSPoints != 0 && oneHandedCounter != 10)
            {
                ++tempSpeedOneH;
                --bonusPoints;
                --oneHandSPoints;
                ++oneHandedCounter;
            }
            else if (swordSpeedSubOneH.isReleased() && tempSpeedOneH > 0 && oneHandedCounter !=0)
            {
                --tempSpeedOneH;
                ++bonusPoints;
                ++oneHandSPoints;
                --oneHandedCounter;
            }
            else if (swordDamageAddOneH.isReleased() && oneHandSPoints != 0 && oneDamageCounter != 10)
            {
                ++tempDamOneH;
                --bonusPoints;
                --oneHandSPoints;
                ++oneDamageCounter;
            }
            else if (swordDamageSubOneH.isReleased() && tempDamOneH > 0 && oneDamageCounter != 0)
            {
                --tempDamOneH;
                ++bonusPoints;
                ++oneHandSPoints;
                --oneDamageCounter;
            }
            else if (blockOneHandAdd.isReleased() && oneHandSPoints != 0 && oneBlockCounter != 10)
            {
                ++tempBlockOneH;
                --bonusPoints;
                --oneHandSPoints;
                ++oneBlockCounter;
            }
            else if (blockOneHandSub.isReleased() && tempBlockOneH > 0 && oneBlockCounter != 0)
            {
                --tempBlockOneH;
                ++bonusPoints;
                ++oneHandSPoints;
                --oneBlockCounter;
            }
            //else if (swordSpeedAddTwoH.isReleased() && twoHandSPoints != 0 && twoSpeedCounter != 10)
            //{
            //    ++tempSpeedTwoH;
            //    --bonusPoints;
            //    --twoHandSPoints;
            //    ++twoSpeedCounter;
            //}
            //else if (swordSpeedSubTwoH.isReleased() && tempSpeedTwoH > 0 && twoSpeedCounter != 0)
            //{
            //    --tempSpeedTwoH;
            //    ++bonusPoints;
            //    ++twoHandSPoints;
            //    --twoSpeedCounter;
            //}
            //else if (swordDamageAddTwoH.isReleased() && twoHandSPoints != 0 && twoDamageCounter != 10)
            //{
            //    ++tempDamTwoH;
            //    --bonusPoints;
            //    --twoHandSPoints;
            //    ++twoDamageCounter;
            //}
            //else if (swordDamageSubTwoH.isReleased() && tempDamTwoH > 0 && twoDamageCounter != 0)
            //{
            //    --tempDamTwoH;
            //    ++bonusPoints;
            //    ++twoHandSPoints;
            //    --twoDamageCounter;
            //}
            //else if (blockTwoHandedAdd.isReleased() && twoHandSPoints != 0 && twoBlockCounter != 10)
            //{
            //    ++tempBlockTwoH;
            //    --bonusPoints;
            //    --twoHandSPoints;
            //    ++twoBlockCounter;
            //}
            //else if (blockTwoHandedSub.isReleased() && tempBlockTwoH > 0 && twoBlockCounter != 0)
            //{
            //    --tempBlockTwoH;
            //    ++bonusPoints;
            //    ++twoHandSPoints;
            //    --twoBlockCounter;
            //}
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

        public void bonusA()
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
        }

        public void skillSellection()
        {
            if (skillMelee.isReleased())
            {
                //skillArcher.deActivate();
                //skillMagic.deActivate();
            }
            else if (skillMelee.isActive())
            {
                skillTreeW();
                bonusW();
            }
            /*if (skillMagic.isReleased())
            {
                skillMelee.deActivate();
                skillArcher.deActivate();
            }
            else if (skillMagic.isActive())
            {
                skillTreeM();
                bonusM();
            }
            if (skillArcher.isReleased())
            {
                skillMagic.deActivate();
                skillMelee.deActivate();
            }
            else if (skillArcher.isActive())
            {
                skillTreeA();
                bonusA();
            }*/
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

        private void setPicOfClass()
        {
            warrior = Content.Load<Texture2D>(@"CharPics\Warrior(2)");
            mage = Content.Load<Texture2D>(@"CharPics\Mage(2)");
            Archer = Content.Load<Texture2D>(@"CharPics\Archer(2)");
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

        public void setPlayerStats(int s, int d, int c, int w, int sp)
        {// this is for the reset to work
            p1.changeStats(s, d, c, w, sp);
        }

        public void resetVariables()
        {
            oneHandedCounter = p1.getOneSwordSpeed();
            oneDamageCounter = p1.getOneSwordDam();
            oneBlockCounter = p1.getOneSwordBlock();
        }

        public void whenSwitched()
        {
            Game.IsMouseVisible = true;
            oneHandSwordSkillPoints();
            twoHandedSwordSkillPoints();
            combatMagicSkillPoints();
            healingMagicSkillPoints();
            longBowSkillPoints();
            shortBowSKillPoints();
            resetVariables();
            tempStat();
            bonusTemp();
            points = p1.getSkillPoints();
        }

        #region DrawFunction
        public override void Draw(GameTime gameTime)
        {

            if (!hidenSkill)
            {
                if (skillExit.isReleased() || Keyboard.GetState().IsKeyUp(Keys.O) && oldk.IsKeyDown(Keys.O))
                {
                    hidenlSScreen = !hidenlSScreen;
                    whenSwitched();
                }
                if (skills.isReleased() || Keyboard.GetState().IsKeyUp(Keys.L) && oldk.IsKeyDown(Keys.L))
                    hidenSkill = true;
                spriteBatch.Draw(Back.image, Back.bounds, Color.White);
                statButtons();
                spriteBatch.Draw(warrior, new Vector2(300, 0), Color.White);
                spriteBatch.DrawString(font2, p1.getName(), new Vector2(0, 0), Color.White);
                drawVitalites();
                drawButtons();
                vitalDescription();
                if (commit.isReleased())
                {
                    p1.setSkillPoints((short)(p1.getStr() - tempstr + p1.getCons() - tempcon + p1.getDex() - tempdex + p1.getWis() - tempwis + p1.getSpi() - tempspr));
                    p1.levelStat(tempstr, tempcon, tempdex, tempwis, tempspr);
                    p1.setVitalities();
                    active = false;
                }
            }
            else
            {
                skillTreeOption(gameTime);
                skillSellection();

            }
            oldk = Keyboard.GetState();

            if (switchedTo)
            {
                whenSwitched();
                switchedTo = false;
            }
        }
        #endregion
    }
}