using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LostLands
{
    class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        ContentManager Content;
        SpriteFont desc;
        string Name;

        int str = 0, cons = 0, dex = 0, wis = 0, spi = 0, attackCounter, Xp, playerLevel, prevLevel, pSkillPoints, oneHandSPoints, twoHandSPoints, combatMagicSPoints, healingMagicSPoints,
            longBowSPoints, shortBowSPoints, twoHandSSkill, twoHandDamSkill, twoHandBlockSSKill, oneSwordBurdenSkill, oneSwordDamageSkill, OneSwordACCSkill, combatMagicDamageSkill,
            combatMagicSpeedSkill, CombatMagicCostSkill, healthMagicPowerSkill, healthMagicSpeedSkill, healthMagicCostSkill;

        int damaged, numCompletedQuests=0;
        int timer = 10, horz, vert, lvlCounter, ohCounter, maxCost= 13, costPerSwing, stamSubCounter; //oh is oneHand
        int movementSpeed = 2;
        float x, y, mapX, mapY;
        float Rotation = 0, Scale = 1, Depth = .5f;
        Rectangle PicRect = new Rectangle(0, 0, 32, 48);
        Vector2 screenPos, lvlText, OHText;// One hand text
        public Vector2 stamSubText;
        Texture2D myTexture;
        Vector2 Origin;
        short skillPoints = 0;
        public bool inStory = false, walking = false, walkingVert, walkingHor;
        public bool isAttacking, doesDamage, lvled, incOH, uiDown = false, pickupItem, inCombat, hasMoved;
        double cHealth, maxHealth, cMana, maxMana, cStam, maxStam;
        string startingClass;

        public int healthPotionCounter, stamPotionCounter, speedPotionCounter;

        public double angle;

        public AnimatedSprite Slash;

        MouseState old;

        public Quest currentQuest;

        Game game;

        public inventorySlot[] onCharItems = new inventorySlot[5];
        public List<inventorySlot> Items = new List<inventorySlot>();

        public Player(Game game, Texture2D pic)
            : base(game)
        {
            this.game = game;
            myTexture = pic;
            Origin = new Vector2(0, 0);

            screenPos = new Vector2(100, 100);
            x = screenPos.X;
            y = screenPos.Y;
            mapX = 0;
            mapY = 0;
            setName("Test");
            setClass("Warrior");
            setVitalities();

            costPerSwing = maxCost;

            spriteBatch =
                (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            Content =
                (ContentManager)Game.Services.GetService(typeof(ContentManager));

            Slash = new AnimatedSprite(game, Content.Load<Texture2D>(@"slash"), new Vector2(300 + 32 / 2, 300 + 35 / 2), 4, 5, this, 0);
            desc = Content.Load<SpriteFont>("Description");
            
        }

        public Rectangle getBounds() { return new Rectangle((int)screenPos.X, (int)screenPos.Y, PicRect.Width, PicRect.Height); }

        public void questLine(AnimatedSprite Sarge, ref Monsters mobs)
        {

            if (currentQuest.rewardGiven)
            {
                switch (numCompletedQuests)
                {
                    //Skels 1
                    //Mummys 2
                    //Scorps 3
                    //Salaman 4
                    //Flan 5
                    //Plants 6
                    //Worms 7
                    //Full Flan 8
                    //Evil Purple Chick 9
                    case 1:
                        //        game, kill this type, kill this many, completion text, reward exp
                        currentQuest = new Quest(game, 1, 5, "Looks like we need you,\nGo kill som skelly's for me.  \n","Sarge:\nGood Job!!!   \nHave some stam :)    \n", 20, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        for (int i = 0; i < 4; ++i)
                            currentQuest.addItemToReward(new UsableItem(game, 4));
                        break;
                    case 2:
                        currentQuest = new Quest(game, 3, 3, "Sarge:\nWhat your a soldier who followed orders.   \nYou want a parade in you honor?  \n", 140, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        break;
                    case 3:
                        currentQuest = new Quest(game, 4, 4, "Go take out these salamders for me.\nJust do it!\n","Sarge:\nWall come on they were slamanders...   \n", 380, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        break;
                    case 4:
                        currentQuest = new Quest(game, 5, 6, "Even you could do this one.\nKill those wierd moving puddles...\nNo tougher then a mudcrab.    \n","Sarge:\nWhoooo!!!   \n", 660, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        currentQuest.addItemToReward(new UsableItem(game, 2));
                        break;
                    case 5:
                        currentQuest = new Quest(game, 2, 9, "The dead are starting to raise from thier graves!\nKill those mummies, trust me they aren't yours    \n", "Sarge:\nThis might be out of character but...\nFor the EMPIRE!   \n", 1162, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        currentQuest.addItemToReward(new UsableItem(game, 2));
                        break;
                    case 6:
                        currentQuest = new Quest(game, 7, 8, "Your getting good.\nMaybe you can do this for me.\nTake out those worms near the docks.    \n", "Sarge:\nHey I picked this up have it.   \n", 2324, ref Sarge);
                        currentQuest.addItemToReward(new LClickItem(game, 2));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        break;
                    case 7:
                        currentQuest = new Quest(game, 8, 6, "Looks like theres more of them. Watchout they are stronger this time!    \n", "Sarge:\nThey never stood a chance.\nIf you are reading this here's a tip press 'O'  \n", 4648, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 1));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        currentQuest.addItemToReward(new UsableItem(game, 4));
                        break;
                    case 8:
                        currentQuest = new Quest(game, 8, 6, "Go annihilate the plants.\nDamn vegans.\nThey tried to grow the perfect specimen.    \n", "Sarge:\nThat was almost the last of them!\nHere is some items to help you out.   \n", 9296, ref Sarge);
                        currentQuest.addItemToReward(new UsableItem(game, 5));
                        currentQuest.addItemToReward(new UsableItem(game, 5));
                        currentQuest.addItemToReward(new UsableItem(game, 5));
                        currentQuest.addItemToReward(new LClickItem(game, 2));
                        currentQuest.addItemToReward(new RClickItem(game, 2));
                        currentQuest.addItemToReward(new UsableItem(game, 5));
                        break;
                    case 9:
                        currentQuest = new Quest(game, 9, 1, "A nagostian necromancer is lurking in the forest.\nThis is your final task.\nYour family will be avanged!     \n", "Sarge:\nYou beat the game!!!   \n", 20, ref Sarge);
                        currentQuest.addItemToReward(new LClickItem(game, 3));
                        currentQuest.addItemToReward(new RClickItem(game, 3));
                        break;
                }
                mobs.addMonsters(currentQuest.MonsterType, currentQuest.killThisMany);
            }
        }

        public void aQuestWasCompleted() { ++numCompletedQuests; }

        public void monsterKilled(Mob mob) { currentQuest.incKilled(mob); }

        public int getCostPerSwing() { return costPerSwing; }

        public void setCurrentQuest(Quest newQuest)
        {
            currentQuest = newQuest;
        }

        public void startQuestLine(AnimatedSprite Sarge, ref Monsters mobs)
        {
            setCurrentQuest(new Quest(game, "Go to Sarge!", "Sarge:\nHere some stuff to get you started\nIf this is Prof Graham \nthen there is probably some spelling errors in here...   \n", ref Sarge));
            currentQuest.addItemToReward(new LClickItem(game, 1));
            currentQuest.addItemToReward(new UsableItem(game, 1));
            currentQuest.addItemToReward(new UsableItem(game, 4));
            mobs.addMonsters(currentQuest.MonsterType, currentQuest.killThisMany);
        }

        /// <summary>
        /// Resets health, mana, and Stamana
        /// </summary>
        public void setVitalities()
        {
            maxHealth = (cons * 5) + (str * .5);
            maxStam = (cons * 5) + (dex * .5);
            maxMana = (spi * 5) + (wis * .5);

            cHealth = maxHealth;
            cStam = maxStam;
            cMana = maxMana;
        }

        public void takeDamage(int dam)
        {
            cHealth -= dam;
        }

        public void takeDamage(int dam, Rectangle attacker)
        {
            if (dam - defenseRating() / 10 > 0)
                cHealth -= dam - defenseRating() / 10;

            damaged = 25;

            //if (this.X + mapX < attacker.X)
            //    screenPos.X -= 1;
            //else if (this.X + mapX > attacker.X)
            //    screenPos.X +=1;

            //if (this.Y + mapY < attacker.Y)
            //{
            //    screenPos.Y -= movementSpeed;
            //}
            //else if (this.Y + mapY > attacker.Y)
            //{
            //    screenPos.Y += movementSpeed;
            //}
        }

        public int meleeDamage()
        {
            if (onCharItems[0] != null)
                return (int)(str + (onCharItems[0].item.damage * 1.1));
            return 0;
        }

        public int defenseRating()
        {
            if (onCharItems[1] != null)
                return (int)((dex / 2) + (onCharItems[1].item.defense * 1.1));
            return (dex / 2);
        }

        public double getCurentHealth() { return cHealth; }
        public double getCurentMana() { return cMana; }
        public double getCurentStam() { return cStam; }
        public double getMaxHealth() { return maxHealth; }
        public double getMaxMana() { return maxMana; }
        public double getMaxStam() { return maxStam; }
        /// <summary>
        /// Returns a static number of skill points
        /// </summary>
        /// <returns></returns>
        public short getSkillPoints() { return skillPoints; }
        public void setSkillPoints(int spent) { skillPoints += (short)spent; }
        public string getClass() { return startingClass; }
        public void setClass(string startingClass)
        {
            this.startingClass = startingClass;
        }

        public void setName(String newName)
        {
            Name = newName;
        }

        public void changeStats(int tstr, int tcons, int tdex, int twis, int tspi)
        {
            str += tstr;
            cons += tcons;
            dex += tdex;
            wis += twis;
            spi += tspi;
        }
        public void levelStat(int tstr1, int tcons1, int tdex1, int twis1, int tspi1)
        {
            str = tstr1;
            cons = tcons1;
            dex = tdex1;
            wis = twis1;
            spi = tspi1;
        }

        public int X
        {
            get { return (int)x; }
            set { screenPos.X = (float)value; }
        }
        public int Y
        {
            get { return (int)y; }
            set { screenPos.Y = (float)value; }
        }

        /// <summary>
        /// X offset
        /// </summary>
        public int MapX { get { return (int)mapX; } }
        /// <summary>
        /// Y offset
        /// </summary>
        public int MapY { get { return (int)mapY; } }

        public string getName() { return Name; }

        public int getStr() { return str; }
        public int getDex() { return dex; }
        public int getCons() { return cons; }
        public int getWis() { return wis; }
        public int getSpi() { return spi; }

        public void walkRight()
        {
            PicRect.Y = 48 * 2;
            ++screenPos.X;
            x = screenPos.X;
            y = screenPos.Y;
        }

        public void walkLeft()
        {
            PicRect.Y = 48;
            --screenPos.X;
            x = screenPos.X;
            y = screenPos.Y;
        }

        public void heal(int heal)
        {
            cHealth += (double)maxHealth * (double)((double)heal / (double)100);
            if (cHealth > maxHealth)
                cHealth = maxHealth;
        }

        public void speed(int speedModifier)
        {
            movementSpeed += speedModifier;
        }

        public void drinkStamPot(int extraStam) { cStam += extraStam; if (cStam > maxStam) cStam = maxStam; }
        public void resetSpeed()
        {
            movementSpeed = 2;
        }

        public void setHPCounter() { healthPotionCounter = 250; }
        public void setSpeedPCounter() { speedPotionCounter = 1000; }
        public void setStamPotionCounter() { stamPotionCounter = 125; }

        public void faceLeft() { PicRect.Y = 48; }
        public void faceRight() { PicRect.Y = 48 * 2; }
        public void faceNorth() { PicRect.Y = 48 * 3; }
        public void faceSouth() { PicRect.Y = 0; }

        public void moveCharacter()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (Slash.animationOver)
                    PicRect.Y = 48 * 3;
                if (y > 200 || mapY == 0 && y > 0)
                    screenPos.Y -= movementSpeed;
                else if (mapY != 0)
                    mapY -= movementSpeed;
                vert = -1;
                walkingVert = true;
                hasMoved = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Slash.animationOver)
                    PicRect.Y = 0;
                if (y + 48 < 400 || mapY == 2560 - 600 && y + 48 < 600)
                    screenPos.Y += movementSpeed;
                else if (mapY < 2560 - 600)
                    mapY += movementSpeed;
                vert = 1;
                walkingVert = true;
                hasMoved = true;
            }
            else
            {
                vert = 0;
                walkingVert = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Slash.animationOver)
                    PicRect.Y = 48;
                if (x > 200 || mapX == 0 && x > 0)
                    screenPos.X -= movementSpeed;
                else if (mapX != 0)
                    mapX -= movementSpeed;
                horz = -1;
                walkingHor = true;
                hasMoved = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (Slash.animationOver)
                    PicRect.Y = 48 * 2;
                if (x + 32 < 600 || mapX == 2560 - 800 && x + 32 < 800)
                    screenPos.X += movementSpeed;
                else if (mapX < 2560 - 800)
                    mapX += movementSpeed;
                horz = 1;
                walkingHor = true;
                hasMoved = true;
            }
            else
            {
                horz = 0;
                walkingHor = false;
            }

            x = screenPos.X;
            y = screenPos.Y;
        }

        public void loopAnim()
        {
            if (walking || walkingHor || walkingVert)
            {
                if (timer > 0)
                    --timer;
                else
                {
                    timer = 10;
                    if (PicRect.X < 32 * 3)
                        PicRect.X += 32;
                    else
                        PicRect.X = 0;
                }
            }
            else
            {
                timer = 0;
                PicRect.X = 0;
            }
        }

        public void checkTileCollision(TileSprite[,] landScape)
        {

            // Loops through LandScape array
            for (short r = 0; r < 35; ++r)
            {
                for (short c = 0; c < 35; ++c)
                {
                    if (landScape[r, c].getID() != 0 && !landScape[r, c].isWalkable)// Check if the position is empty

                        // the different numbers like +22 +8 or whatever are for his walking buffer
                        if (x + 22 + 1 > landScape[r, c].getX() && x + 8 < landScape[r, c].getX() + landScape[r, c].getWidth())// check if we are horizontally inside object
                            if (y + 32 < landScape[r, c].getY() + landScape[r, c].getHeight() && y + 40 + 3 > landScape[r, c].getY())
                            {// Check if we are vertically inside object

                                    //nudges the player the opposite way he is facing to release collision flag
                                    if (vert == -1)
                                        screenPos.Y += movementSpeed;
                                    if (horz == 1)
                                        screenPos.X -= movementSpeed;
                                    if (vert == 1)
                                        screenPos.Y -= movementSpeed;
                                    if (horz == -1)
                                        screenPos.X += movementSpeed;
                            }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            loopAnim();
            if (!inStory)
                moveCharacter();

            base.Update(gameTime);
        }

        public bool mouseInPlay()
        {
            bool result = false;

            if (!pickupItem && Mouse.GetState().LeftButton == ButtonState.Released && old.LeftButton == ButtonState.Pressed)
            {

                if(Mouse.GetState().X > 0 && Mouse.GetState().X < 800)
                    if (uiDown)
                    {
                        if (Mouse.GetState().Y > 0 && Mouse.GetState().Y < 600)
                            result = true;
                    } else if (Mouse.GetState().Y > 0 && Mouse.GetState().Y < 560)
                        result = true;

            }

            return result;
        }

        public void checkAttack()
        {
            if (mouseInPlay() && !isAttacking && Slash.animationOver && cStam >= costPerSwing)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                double theta = Math.Atan2((y - screenPos.Y - 48 / 2), (x - screenPos.X - 32 / 2));

                angle = theta * (180.0 / Math.PI);

                if (angle > 0 && angle < 180)
                    faceSouth();
                else
                    faceNorth();

                if (angle > -30 && angle < 30)
                    faceRight();
                else if (angle > 130 || angle < -130)
                    faceLeft();

                Slash.setRotation((float)theta);
                Slash.drawAnim();

                // take stam away from stam bar on ui screen
                cStam -= costPerSwing;
                stamSubCounter = 40;
                stamSubText.X = 117;

                //Deciding if the player "hits"
                Random rand = new Random();
                if (rand.Next(0, 100) < 50 + OneSwordACCSkill * 5)
                {
                    doesDamage = true;
                }else
                {
                    doesDamage = false;
                }

                isAttacking = true;
            }
            else
            {
                if (attackCounter > 0 && isAttacking)
                {
                    --attackCounter;
                    onCharItems[0].item.use();
                }
                else
                {
                    onCharItems[0].item.makeUsable();
                    isAttacking = false;
                    if (cStam < costPerSwing)
                    {
                        isAttacking = true;
                        onCharItems[0].item.use();
                    }
                    attackCounter = 65 - dex;
                }
            }

            if (cStam < costPerSwing && mouseInPlay())
            {
                stamSubCounter = 40;
                stamSubText = screenPos;
                stamSubText.X -= 50;
            }
        }

        public bool usingOneHandSword()
        {
            if (onCharItems[0].item.type == 1 && onCharItems[0].item.type < 20)
                return true;
            else
                return false;
        }

        public void incOneHand() {
            Random ran = new Random();
            if (ran.Next(0, 100) <= 20)
            {
                ++oneHandSPoints;
                incOH = true;
                OHText = new Vector2(screenPos.X - 50, screenPos.Y);
            }
        }
        public void incTwoHand() { ++twoHandSPoints; }
        public void incCombMagicHand() { ++healingMagicSPoints; }
        public void incHealingMagicHand() { ++combatMagicSPoints; }
        public void incLongBowHand() { ++longBowSPoints; }
        public void incShortBowHand() { ++shortBowSPoints; }
        public void gainExp(int exp) { Xp += exp; }

        # region one handed set and get and main method
        public void setOneSwordSpeedSKill(int spent)
        {
            oneHandSPoints -= spent;// main method
            oneSwordBurdenSkill += spent; // get method Burden
            costPerSwing = maxCost - oneSwordBurdenSkill;
        }
        public void setOneSwordDamSkill(int spent)
        {
            oneHandSPoints -= spent;// main method
            oneSwordDamageSkill += spent;//get method Damage
        }
        public void setOneSwordBlockSkill(int spent)
        {
            oneHandSPoints -= spent;// main method
            OneSwordACCSkill += spent;// get method Accuracy
        }

        public int getOneSwordSpeed() { return oneSwordBurdenSkill; }
        public int getOneSwordDam() { return oneSwordDamageSkill; }
        public int getOneSwordBlock() { return OneSwordACCSkill; }

        public int oneHandSkillPoints()
        {
            // oneHandSPoints = 1;
            return oneHandSPoints;
        }
        #endregion

        #region two handed set and get and main method
        public void setTwoDamSkill(int spent)
        {
            twoHandSPoints -= spent;
            twoHandDamSkill += spent;
        }
        public void settwoHandSpeedSkill(int spent)
        {
            twoHandSPoints -= spent;
            twoHandSSkill += spent;
        }
        public void setTwoHandBlockSkill(int spent)
        {
            twoHandSPoints -= spent;
            twoHandBlockSSKill += spent;
        }

        public int getTwoDamSkill() { return twoHandDamSkill; }
        public int getTwoHandSpeedSkill() { return twoHandSSkill; }
        public int getTwoHandBlockSkill() { return twoHandBlockSSKill; }

        public int twoHandSkillPoints()
        {
            //twoHandSPoints = 1;
            return twoHandSPoints;
        }
        # endregion

        #region combat magic set and get and main method
        public void setCombatMagicDamageSkill(int spent)
        {
            combatMagicSPoints -= spent;
            combatMagicDamageSkill += spent;
        }

        public void setCombatMagicSpeedSkill(int spent)
        {
            combatMagicSPoints -= spent;
            combatMagicSpeedSkill += spent;
        }

        public void setCombatMagicCostSkill(int spent)
        {
            combatMagicSPoints -= spent;
            CombatMagicCostSkill += spent;
        }

        public int getCombatMagicDamageSkill() { return combatMagicDamageSkill; }
        public int getCombatMagicSpeedSkill() { return combatMagicSpeedSkill; }
        public int getCombatMagicCostSkill() { return CombatMagicCostSkill; }

        public int combatMagicSkillPoints()
        {
            //combatMagicSPoints = 1;
            return combatMagicSPoints;
        }
        #endregion

        #region health magic set and get and main method

        public void setHealthMagicPowerSkill(int spent)
        {
            healingMagicSPoints -= spent;
            healthMagicPowerSkill += spent;
        }

        public void setHealthMagicSpeedSkill(int spent)
        {
            healingMagicSPoints -= spent;
            healthMagicSpeedSkill += spent;
        }

        public void setHealthMagicCostSkill(int spent)
        {
            healingMagicSPoints -= spent;
            healthMagicCostSkill += spent;
        }

        public int healingMagicSkillPoints()
        {
            //healingMagicSPoints = 1;
            return healingMagicSPoints;
        }

        public int getHealthMagicPowerSkill() { return healthMagicPowerSkill; }
        public int getHealthMagicSpeedSkill() { return healthMagicSpeedSkill; }
        public int getHealthMagicCostSkill()  { return healthMagicCostSkill;  }

        #endregion

        #region long bow set and get and main method

        public void setLongBowDamage(int spent)
        {

        }

        public void setLongBowSpeed(int spent)
        {

        }

        public void setLongBowDodge(int spent)
        {

        }

        public int longBowSkillPoints()
        {
            //longBowSPoints = 1;
            return longBowSPoints;
        }

        #endregion

        #region short bow set and get and main method

        public void setShortBowDamage(int spent)
        {

        }

        public void setShortBowSpeed(int spent)
        {

        }

        public void setShortBowDodge(int spent)
        {

        }

        public int shortBowSkillPoints()
        {
            //shortBowSPoints = 1;
            return shortBowSPoints;
        }

        #endregion

        public int playerSkillPoints()
        {
            pSkillPoints = oneHandSkillPoints() + twoHandSkillPoints() + combatMagicSkillPoints() + healingMagicSkillPoints() + longBowSkillPoints() + shortBowSkillPoints();
            return pSkillPoints;
        }

        public int Xplayer()
        {
            //Xp = 127999;
            return Xp;
        }

        public int pLevel()
        {
            if (Xp <= 10) { playerLevel = 1; prevLevel = 1; } else
            if (Xp > 11 && Xp <= 500) { playerLevel = 2; } else
                //if (Xp <= 250) { playerLevel = 1; prevLevel = 1; } else
                //if (Xp > 251 && Xp <= 500) { playerLevel = 2; } else
                if (Xp > 501 && Xp <= 1000) { playerLevel = 3; } else
                if (Xp > 1001 && Xp <= 2000) { playerLevel = 4; } else
                if (Xp > 2001 && Xp <= 4000) { playerLevel = 5; } else
                if (Xp > 4001 && Xp <= 8000) { playerLevel = 6; } else
                if (Xp > 8001 && Xp <= 16000) { playerLevel = 7; } else
                if (Xp > 16001 && Xp <= 32000) { playerLevel = 8; } else
                if(Xp > 32001 && Xp <= 64000) { playerLevel = 9; } else
                if (Xp > 64001 && Xp <= 128000) { playerLevel = 10; }
            whenLeveled();
            return playerLevel;
        }

        private void whenLeveled()
        {
            if (prevLevel < playerLevel)
            {
                lvled = true;
                if (incOH)
                {
                    lvlText = new Vector2(screenPos.X, screenPos.Y-20);
                }else
                    lvlText = new Vector2(screenPos.X, screenPos.Y);
                str += 2;
                dex += 1;
                cons += 2;
                wis += 1;
                spi = 1;
                skillPoints += 2;
                setVitalities();
                prevLevel = playerLevel;
            }
        }

        public void setPotionCounters()
        {

            if (healthPotionCounter > 0)
                --healthPotionCounter;

            if (stamPotionCounter > 0)
                --stamPotionCounter;

            if (speedPotionCounter > 0)
                --speedPotionCounter;
            else
                resetSpeed();
        }

        public void statusText()
        {
            if (lvled && lvlCounter > 0)
            {
                spriteBatch.DrawString(desc, "Level UP", lvlText, Color.Yellow);
                lvlText.Y -= 2;
                --lvlCounter;
            }
            else
            {
                lvlCounter = 50;
                lvled = false;
            }

            if (incOH && ohCounter > 0)
            {
                spriteBatch.DrawString(desc, "One Hand Increased", OHText, Color.MediumSpringGreen);
                OHText.Y -= 2;
                --ohCounter;
            }
            else
            {
                ohCounter = 50;
                incOH = false;
            }

            if (stamSubCounter > 0)
            {
                --stamSubCounter;
                if (cStam > costPerSwing)
                {
                    ++stamSubText.X;
                    spriteBatch.DrawString(desc, "-" + costPerSwing, stamSubText, Color.White);
                }
                else
                {
                    --stamSubText.Y;
                    spriteBatch.DrawString(desc, "Not enough stamin", stamSubText, Color.Red);
                }

            }
        }

        public void stamRegen()
        {
            if (inCombat)
            {
                if (cStam < maxStam)
                    cStam += .01 + ((float)(cons / 1000.0) / 5);
            }else if (cStam < maxStam)
                cStam += .05 + (((float)cons / 1000.0) / 5);//Cons is dex

            //spriteBatch.DrawString(desc, inCombat + "", new Vector2(0, 0), Color.White);
        }

        public void healthRegen()
        {
            if (inCombat)
            {
                if (cHealth < maxHealth)
                    cHealth += .001;
            }
            else if (cHealth < maxHealth)
                cHealth += .01;
        }

        public override void Draw(GameTime gameTime)
        {
            Update(gameTime);

            if (damaged == 0)
                spriteBatch.Draw(myTexture, screenPos, PicRect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
            else
            {
                spriteBatch.Draw(myTexture, screenPos, PicRect, Color.Red, Rotation, Origin, Scale, SpriteEffects.None, Depth);
                --damaged;
            }

            if (!inStory)
            {
                if (onCharItems[0] != null)
                {
                    checkAttack();
                    if (!Slash.animationOver && onCharItems[0] != null && onCharItems[0].item.type == 1)
                    {
                        Slash.Draw(gameTime);
                    }
                }

                if (!hasMoved)
                {
                    spriteBatch.DrawString(desc, "To Move:", new Vector2(590, 80), Color.Yellow);
                    spriteBatch.Draw(Content.Load<Texture2D>("WASD"), new Rectangle(600, 100, 100, 100), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("Arrows"), new Rectangle(600, 160, 100, 100), Color.White);
                }
            }

            stamRegen();

            healthRegen();

            setPotionCounters();

            statusText();

            if (currentQuest != null && hasMoved)
                currentQuest.Draw(gameTime);

            Xplayer();
            playerSkillPoints();
            base.Draw(gameTime);
            old = Mouse.GetState();
            pickupItem = false;
        }
    }
}
