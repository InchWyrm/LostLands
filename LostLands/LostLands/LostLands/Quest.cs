using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class Quest : OurGamesDarwableComponent
    {

        public int MonsterType, killThisMany, killedThisMany, rewardEXP, questType;
        String goDoThis, questText, sargeTalk;
        Vector2 pos;
        Mob mobToKill;
        public bool completed, started, rewardGiven;
        SpriteFont desc;
        public TypeText endDialog, startDialog;
        AnimatedSprite Sarge;
        public List<Item> rewardItems = new List<Item>();
        public Rectangle miniDot = new Rectangle(-2,-2,3,3);

        public Quest(Game game, int Type, int killHowMany, String SargeTalk, int rewardXP, ref AnimatedSprite Sarge)
            : base(game)
        {
            questType = 2;
            pos = new Vector2(520, 110);
            MonsterType = Type;
            killThisMany = killHowMany;
            rewardEXP = rewardXP;
            mobToKill = new Mob(game, Type);
            desc = Content.Load<SpriteFont>("Description");
            sargeTalk = SargeTalk;
            endDialog = new TypeText((int)Sarge.ax - 50, (int)Sarge.ay - 60, sargeTalk, desc);
            startDialog = new TypeText((int)Sarge.ax - 50, (int)Sarge.ay - 60, "Sarge:\nThe "+mobToKill.getName()+"'s are infesting.\nKill "+killThisMany+" for me.\n", desc);
            started = true;
            this.Sarge = Sarge;
        }

        public Quest(Game game, int Type, int killHowMany, String opening, String SargeTalk, int rewardXP, ref AnimatedSprite Sarge)
            : base(game)
        {
            questType = 2;
            pos = new Vector2(520, 110);
            MonsterType = Type;
            killThisMany = killHowMany;
            rewardEXP = rewardXP;
            mobToKill = new Mob(game, Type);
            desc = Content.Load<SpriteFont>("Description");
            sargeTalk = SargeTalk;
            endDialog = new TypeText((int)Sarge.ax - 50, (int)Sarge.ay - 60, sargeTalk, desc);
            startDialog = new TypeText((int)Sarge.ax - 50, (int)Sarge.ay - 60, "Sarge:\n" + opening, desc);
            started = true;
            this.Sarge = Sarge;
        }

        public void setMiniMapDot()
        {
            float x = -2, y = -2;
            //710, 10
            switch (MonsterType)
            {
                case 1:
                    x = ((25 + 280)/2) / 32 + 710;
                    y = ((600 + 620)/2) / 32 + 10;
                    break;
                case 2:
                    x = ((1980 + 2045) /2) / 32 + 710;
                    y = ((1004 + 1080)/2) / 32 + 10;
                    break;
                case 3:
                    x = ((616+ 1025)/ 2) / 32 + 710;
                    y = ((619+ 776) / 2) / 32 + 10;
                    break;
                case 4:
                    x = ((623+ 1053) / 2) / 32 + 710;
                    y = ((772+ 1154) / 2) / 32 + 10;
                    break;
                case 5:
                    x = ((1318+ 1750) / 2) / 32 + 710;
                    y = ((594+ 776) / 2) / 32 + 10;
                    break;
                case 6:
                    x = ((1274+ 1693) / 2) / 32 + 710;
                    y = ((897+ 1122) / 2) / 32 + 10;
                    break;
                case 7:
                    x = ((1849+ 2179) / 2) / 32 + 710;
                    y = ((125+ 273) / 2) / 32 + 10;
                    break;
                case 8:
                    x = ((1929+ 2197) / 2) / 32 + 710;
                    y = ((1240+ 1378) / 2) / 32 + 10;
                    break;
                case 9:
                    x = ((135+ 239) / 2) / 32 + 710;
                    y = ((1204+ 1242) / 2) / 32 + 10;
                    break;
            }

            if (completed||started)
            {
                x = 400 / 32 + 710; 
                y = 300 / 32 + 10;
            }

            miniDot.X = (int)x;
            miniDot.Y = (int)y;
        }

        public Quest(Game game, String goDO, String SargeTalk, ref AnimatedSprite Sarge)
            : base(game)
        {
            questType = 1;
            pos = new Vector2(640, 110);
            goDoThis = goDO;
            desc = Content.Load<SpriteFont>("Description");
            completeQuest();
            sargeTalk = SargeTalk;
            endDialog = new TypeText((int)Sarge.ax - 50, (int)Sarge.ay - 60, sargeTalk, desc);
            startDialog = new TypeText(0, 0, "", desc);
            this.Sarge = Sarge;
            //rewardItems.Add(new LClickItem(game, 1));
            //dialog.setText(400 - 50, 300 - 30, sargeTalk);
        }

        public void addItemToReward(Item item)
        {
            rewardItems.Add(item);
        }

        //public void setReward(List<Item> rewardItems)
        //{
        //    this.rewardItems = rewardItems;
        //}

        public void incKilled(Mob mob) { 

            if(mob.getName().CompareTo(mobToKill.getName()) == 0){
                ++killedThisMany; 
                isComplete(); 
            }

        }

        public bool isStarted() { return started; }

        /// <summary>
        /// Decides wether quest is complete and returns bool
        /// </summary>
        /// <returns>If the quest objective has been finished</returns>
        public bool isComplete()
        {
            switch(questType){
                case 2:
                    if (killedThisMany >= killThisMany)
                    {
                        completed = true;
                    }
                break;
            }
            return completed;
        }

        public bool finishQuest()
        {
            if (endDialog.textDone && !rewardGiven)
            {
                rewardGiven = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Forces quest to be completed
        /// </summary>
        public void completeQuest() { completed = true; }

        public void updateQuestText()
        {
            switch (questType)
            {
                case 1:
                    questText = goDoThis;
                    break;
                case 2:
                    questText = "Defeated " + killedThisMany + " " + mobToKill.getName() + " out of " + killThisMany;
                    break;
            }
        }

        public String getLog()
        {
            updateQuestText();
            if (!completed)
                return questText;
            else
            {
                if (questType != 1)
                    return "Complete!";
                else
                    return questText;
            }
        }

        public void makeSargeTalk()
        {
            //dialog.forceDone();
            Sarge.update();

            if (completed)
            {
                endDialog.resetPosition((int)Sarge.screenPos.X - 50, (int)Sarge.screenPos.Y - 60);
                //endDialog.addText();
                endDialog.forceDone();
                spriteBatch.Draw(Content.Load<Texture2D>(@"MercRoom1"), endDialog.textBox, Color.Black);
                spriteBatch.DrawString(endDialog.font, endDialog.text, new Vector2(endDialog.x, endDialog.y), Color.Silver);
            }
            else if (started)
            {
                startDialog.resetPosition((int)Sarge.screenPos.X - 50, (int)Sarge.screenPos.Y - 60);
                //startDialog.addText();
                startDialog.forceDone();
                spriteBatch.Draw(Content.Load<Texture2D>(@"MercRoom1"), startDialog.textBox, Color.Black);
                spriteBatch.DrawString(startDialog.font, startDialog.text, new Vector2(startDialog.x, startDialog.y), Color.Silver);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(desc, "Quest:", new Vector2(pos.X, 90), Color.Yellow);
            setMiniMapDot();
            if (!started)
            {
                spriteBatch.DrawString(desc, getLog(), pos, Color.Yellow);
            }else
                spriteBatch.DrawString(desc, "Sarge has a new quest for you!", pos, Color.Yellow);
            base.Draw(gameTime);
        }

    }
}
