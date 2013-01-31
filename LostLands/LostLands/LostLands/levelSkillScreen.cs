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
        // to many int lol
        int tempSpellSpeedC, tempSpellPowerC,
            bonusPoints, tempSpeedOneH, tempDamOneH, tempBlockOneH, tempSpeedTwoH, tempDamTwoH,
            tempBlockTwoH, tempSpellCostC, tempSpellSpeedH, tempSpellPowerH, tempSpellCostH, 
            tempLongBowDam, tempLongBowSpe, tempLongBowdodge, tempShortBowDam, tempShortBowSpe, 
            tempShortBowDodge;

        // just setting button names to use 
        button  skillExit, warrior, mage, archer;

        Player p1;
        BackgroundImage Back;
        KeyboardState oldk;
        Texture2D warriorPick;
        Texture2D magePick;
        Texture2D ArcherPick;
        SkillSelect skillMelee, skillMagic, skillArcher, CommitSkills;
        MouseState oldM;
        TypeText SkillScreen;
        WarriorSkillSet WarriorSet;

        public bool hidenlSScreen = true;
        public bool hidenSkill = false;
        public bool drawWarriorSet = false;

        public levelSkillScreen(Game game, ref Player p1)
            : base(game)
        {
            // starting buttons for skill sets for melee 
            skillMelee = new SkillSelect(game, 25, 510, 210, 50, "Melee Ability");
            skillMelee.activate();
            skillMagic = new SkillSelect(game, 255, 510, 210, 50, "Mage  Ability");
            skillArcher = new SkillSelect(game, 485, 510, 210, 50, "Archer Ability");
            CommitSkills = new SkillSelect(game, 205, 452, 300, 50, "       Skill  Commit");
            Back = new BackgroundImage(Content.Load<Texture2D>(@"Backgrounds\CreationBackground"));
            WarriorSet = new WarriorSkillSet(game, ref p1, this);
            createButtons();
            this.p1 = p1;
        }

        private void createButtons()
        {
            // load the information to write text 
            SkillScreen = new TypeText(100, 490, 10, "Click on the name plate for which class you want.", Content.Load<SpriteFont>("PCfont"));
            // loading the buttons
            skillExit = new button(800 - 129, 0, Content.Load<Texture2D>(@"Buttons\UIExit"), 
                Content.Load<Texture2D>(@"Buttons\UIExit(2)"), Content.Load<Texture2D>(@"Buttons\UIExit(3)"));
            warrior = new button(80,450, Content.Load<Texture2D>(@"buttons\Warrior"),Content.Load<Texture2D>(@"buttons\Warrior(2)"),
                Content.Load<Texture2D>(@"buttons\Warrior(3)"));
            mage = new button(340, 450, Content.Load<Texture2D>(@"buttons\Mage"), Content.Load<Texture2D>(@"buttons\Mage(2)"),
                Content.Load<Texture2D>(@"buttons\Mage(3)"));
            archer = new button(600, 450, Content.Load<Texture2D>(@"buttons\Archer"), Content.Load<Texture2D>(@"buttons\Archer(2)"),
                Content.Load<Texture2D>(@"buttons\Archer(3)"));

            // pictures of classes over top of buttons
            warriorPick = Content.Load<Texture2D>(@"CharPics\Warrior(2)");
            magePick = Content.Load<Texture2D>(@"CharPics\Mage(2)");
            ArcherPick = Content.Load<Texture2D>(@"CharPics\Archer(2)");
        }

        public void skillTreeOption(GameTime gameTime)
        {
            // to switch between screens
            if (skillExit.isReleased() || Keyboard.GetState().IsKeyUp(Keys.L) && oldk.IsKeyDown(Keys.L))
                hidenlSScreen = true;
           
            //loading buttons and player name 
            spriteBatch.Draw(Back.image, Back.bounds, Color.White);
            spriteBatch.Draw(skillExit.getState(), skillExit.buttonBounds, Color.White);
            spriteBatch.Draw(warrior.getState(), warrior.buttonBounds, Color.White);
            spriteBatch.Draw(mage.getState(), mage.buttonBounds, Color.White);
            spriteBatch.Draw(archer.getState(), archer.buttonBounds, Color.White);

            // drawing the class pictures
            spriteBatch.Draw(warriorPick, new Vector2(50, 200), Color.White);
            spriteBatch.Draw(magePick, new Vector2(330, 200), Color.White);
            spriteBatch.Draw(ArcherPick, new Vector2(580, 200), Color.White);

            // coping players name to page and writing to screen
            spriteBatch.DrawString(font2, p1.getName(), new Vector2(0, 0), Color.Blue);
            spriteBatch.DrawString(font2, p1.getName(), new Vector2(0, 0), Color.Red);

            // just help information to tell players what to do
            spriteBatch.DrawString(font2, "Click on the name plate for which class you want.", new Vector2(100, 490), Color.Blue);
            SkillScreen.addText();// text writer, this is used to write one letter at a time
            spriteBatch.DrawString(SkillScreen.font, SkillScreen.text, new Vector2(SkillScreen.x, SkillScreen.y), Color.Red);
            
            // just added so players can see if they have points to spend and how many before going to the classes 
            spriteBatch.DrawString(font2, "Skill points: ", new Vector2(0, 45), Color.Blue);
            spriteBatch.DrawString(font2, "Skill points: ", new Vector2(0, 45), Color.Red);
         
               }

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
            tempSpeedOneH != 0 || tempDamOneH != 0 || tempBlockOneH != 0 ||
            tempSpeedTwoH != 0 || tempDamTwoH != 0 || tempBlockTwoH != 0 ||
            tempSpellSpeedC != 0 || tempSpellPowerC != 0 || tempSpellCostC != 0 ||
            tempSpellSpeedH != 0 || tempSpellPowerH != 0 || tempSpellCostH != 0 ||
            tempLongBowDam != 0 ||  tempLongBowSpe != 0 || tempLongBowdodge != 0 ||
            tempShortBowDam != 0 || tempShortBowSpe != 0 || tempShortBowDodge != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (warrior.isReleased())
            {
                //hidenlSScreen = true;
                drawWarriorSet = true;
            }
            if (mage.isReleased())
            {
                hidenlSScreen = true;
            }
            if (archer.isReleased())
            {
                hidenlSScreen = true;
            }
           
            base.Draw(gameTime);
            skillTreeOption(gameTime);
            oldM = Mouse.GetState();
            oldk = Keyboard.GetState();
        }
    }
}