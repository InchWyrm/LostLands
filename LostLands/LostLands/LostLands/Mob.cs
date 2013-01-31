using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostLands
{
    /// <summary>
    /// Is a monster
    /// </summary>
    class Mob : OurGamesDarwableComponent
    {

        public AnimatedSprite AS;
        Texture2D pic;
        Player player;
        int x, y, txtCounter, hitCounter;
        Vector2 missText;
        bool islinked = false, hidden = false, hasBeenHit= false;
        public Rectangle bounds;
        public int health;
        int str, con;
        //int wis, spi, dex;
        float range, speedModifier, detectRange;
        double exp;
        List<itemDrop> possibleDrops;
        String name = "";
        int Monster;

        public Mob(Game game, int x, int y, int Monster, ref Player p1)
            : base(game)
        {
            this.x = x;
            this.y = y;
            player = p1;
            possibleDrops = new List<itemDrop>();

            setupMonster(Monster);

            this.Monster = Monster;

            setAllItems();
            AS = new AnimatedSprite(game, pic, new Vector2(x, y),ref player);
            AS.stopAnimating();
            bounds = new Rectangle((int)x, (int)y, 32, 32);
        }

        public Mob(Game game, int Monster)
            : base(game)
        {
            this.Monster = Monster;
            possibleDrops = new List<itemDrop>();

            setupMonster(Monster);

            setAllItems();
            //AS = new AnimatedSprite(game, pic, new Vector2(x, y), ref player);
            //AS.stopAnimating();
            //bounds = new Rectangle((int)x, (int)y, 32, 32);
        }

        public int getMonsterType() { return Monster; }

        public void setupMonster(int Monster)
        {
            #region Monsters
            switch (Monster)
            {
                case 1:
                    pic = Content.Load<Texture2D>(@"NPC\skeleton");
                    name = "Skeleton";
                    str = 1;
                    //dex = 0;
                    con = 5;
                    //wis = 0;
                    //spi = 0;
                    exp = 15;
                    range = 1;
                    speedModifier = .2f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 1), 80));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 2), 20));
                    health = (int)((con * 5) + (str * .5));
                    hidden = true;
                    break;

                case 2:
                    pic = Content.Load<Texture2D>(@"NPC\mummy");
                    name = "Mummy";
                    str = 2;
                    //dex = 1;
                    con = 30;
                    //wis = 0;
                    //spi = 0;
                    exp = 50;
                    range = 2;
                    speedModifier = .25f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 2), 40));
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 1), 30));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 3:
                    pic = Content.Load<Texture2D>(@"NPC\Scorpion");
                    name = "Scorpion";
                    str = 2;
                    //dex = 1;
                    con = 10;
                    //wis = 0;
                    //spi = 0;
                    exp = 25;
                    range = 0f;
                    speedModifier = .5f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 1), 50));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 1), 80));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 4), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 4:
                    pic = Content.Load<Texture2D>(@"NPC\Salamander");
                    name = "Salamander";
                    str = 4;
                    //dex = 1;
                    con = 10;
                    //wis = 0;
                    //spi = 0;
                    exp = 50;
                    range = 0f;
                    detectRange = 100;
                    speedModifier = .5f;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 2), 30));
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 2), 10));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 4), 40));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 2), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 5:
                    pic = Content.Load<Texture2D>(@"NPC\Flan");
                    name = "Flan";
                    str = 15;
                    //dex = 1;
                    con = 20;
                    //wis = 0;
                    //spi = 0;
                    exp = 450;
                    range = 1f;
                    speedModifier = .35f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 2), 50));
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 2), 50));
                    possibleDrops.Add(new itemDrop(new Item(game, 3, 3), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 6:
                    pic = Content.Load<Texture2D>(@"NPC\Plant");
                    name = "Plant";
                    str = 10;
                    //dex = 1;
                    con = 50;
                    //wis = 0;
                    //spi = 0;
                    exp = 2000;
                    range = 1f;
                    speedModifier = .89f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 3), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 7:
                    pic = Content.Load<Texture2D>(@"NPC\Worm");
                    name = "Worm";
                    str = 4;
                    //dex = 1;
                    con = 40;
                    //wis = 0;
                    //spi = 0;
                    exp = 800;
                    range = 1f;
                    speedModifier = .35f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 2), 40));
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 2), 20));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 8:
                    pic = Content.Load<Texture2D>(@"NPC\fullFlan");
                    name = "fullFlan";
                    str = 20;
                    //dex = 1;
                    con = 70;
                    //wis = 0;
                    //spi = 0;
                    exp = 1000;
                    range = 1f;
                    speedModifier = .35f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 2), 40));
                    possibleDrops.Add(new itemDrop(new Item(game, 1, 3), 35));
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 3), 25));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 9:
                    pic = Content.Load<Texture2D>(@"NPC\evilPurpleChick");
                    name = "Evil Mistress";
                    str = 40;
                    //dex = 1;
                    con = 300;
                    //wis = 0;
                    //spi = 0;
                    exp = 10000;
                    range = 1f;
                    speedModifier = .9f;
                    detectRange = 150;
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 3), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                case 10:
                    pic = Content.Load<Texture2D>(@"NPC\shroomDude");
                    name = "ShroomDude";
                    str = 200000;
                    //dex = 1;
                    con = 40;
                    //wis = 0;
                    //spi = 0;
                    exp = 35;
                    range = 1f;
                    speedModifier = .35f;
                    detectRange = 100;
                    possibleDrops.Add(new itemDrop(new Item(game, 2, 3), 100));
                    health = (int)((con * 5) + (str * .5));
                    hidden = false;
                    break;
                default:
                    pic = null;
                    break;
            }
            #endregion
        }

        public String getName() { return name; }

        private double getDistance()
        {
            double distance = 0;

            //distance = Math.Sqrt(Math.Pow(AS.screenPos.X - player.X, 2) + Math.Pow(AS.screenPos.Y - player.Y, 2));
            distance = (Math.Sqrt(Math.Pow(Math.Abs(AS.screenPos.X + 32 / 2 - player.X - 32 / 2), 2) + Math.Pow(Math.Abs(AS.screenPos.Y + 32 / 2 - player.Y - 48 / 2), 2)));

            return distance;
        }

        public bool inCombat()
        {
            if (getDistance() <= detectRange)
                return true;
            else
                return false;
        }

        public void chase()
        {
            if (getDistance() <= detectRange)
            {
                AS.startAnimating();
                player.inCombat = true;
                if (AS.screenPos.X + 3 <= player.X)
                {
                    faceRight();
                    AS.ax += speedModifier;
                }
                else if (AS.screenPos.X - 3 > player.X)
                {
                    faceLeft();
                    AS.ax -= speedModifier;
                }

                if (AS.screenPos.Y >= player.Y + 30)
                {
                    faceNorth();
                    AS.ay -= speedModifier;
                }
                else if (AS.screenPos.Y < player.Y + 24)
                {
                    faceSouth();
                    AS.ay += speedModifier;
                }

            }
            else if (getDistance() <= (detectRange + 50))
            {
                AS.stopAnimating();
                hidden = false;
                faceSouth();
                AS.ax = (int)AS.ax;
                AS.ay = (int)AS.ay;
            }
        }

        public void faceLeft()
        {
            AS.source.Y = 32;
        }

        public void faceRight()
        {
            AS.source.Y = 32 * 2;
        }

        public void faceNorth()
        {
            AS.source.Y = 32 * 3;
        }

        public void faceSouth()
        {
            AS.source.Y = 0;
        }

        /// <summary>
        /// follow function
        /// </summary>
        public void linkToKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                faceNorth();
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                faceSouth();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                faceLeft();
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                faceRight();
        }

        public void checkMobCollision(Mob target)
        {
            if (bounds.Intersects(target.bounds) && this != target)
            {
                //Fancy specific collisionng giving a problem
                /*
                if (bounds.X + bounds.Width <= target.bounds.X + 1)
                {
                    AS.ax -= .2f;
                }
                else if (bounds.X < target.bounds.X+target.bounds.Width)
                {
                    AS.ax += .2f;
                }*/

                //Unfancy collision wroking perfectly
                if (bounds.X < target.bounds.X)
                    AS.ax -= speedModifier;
                else if(bounds.X > target.bounds.X)
                    AS.ax += speedModifier;

                if(bounds.Y < target.bounds.Y){
                    AS.ay -= speedModifier;
                }else if(bounds.Y > target.bounds.Y){
                    AS.ay += speedModifier;
                }
            }
        }

        public bool checkHit()
        {
            if (player.isAttacking && !hasBeenHit && player.onCharItems[0] != null)
            {
                double theta = Math.Atan2((AS.screenPos.Y + 32/2 - player.Y - 48 / 2), (AS.screenPos.X + 32/2 - player.X - 32 / 2));

                double angle = theta * (180.0 / Math.PI);

                if (player.onCharItems[0].item.type == 1 && getDistance() < 50 && !hasBeenHit)
                    if (Math.Abs(angle - player.angle) <= 90)
                    {
                        hasBeenHit = true;
                        return true;
                    }
                return false;
            }
            return false;
        }

        public void miss()
        {
            missText = AS.screenPos;
            txtCounter = 20;
        }

        public void hit()
        {
            hitCounter = 20;
            health -= player.meleeDamage();

            //Give the player a chance to increase skill
            if(player.usingOneHandSword())//If the player is using One handed sword
                player.incOneHand();

            //Bump the enemy away from player
            bump();
        }

        /// <summary>
        /// Bump enemy away from player
        /// </summary>
        public void bump()
        {
            if (AS.screenPos.X < player.X)
                AS.ax -= 10f;
            else if (bounds.X > player.X)
                AS.ax += 10f;

            if (AS.screenPos.Y < player.Y)
            {
                AS.ay -= 10f;
            }
            else if (AS.screenPos.Y > player.Y)
            {
                AS.ay += 10f;
            }
        }

        public void attack()
        {
            if (getDistance() < 30 + range)
            {
                bump();
                player.takeDamage(str * 2, bounds);
            }
        }

        public void setAllItems()
        {
            foreach (itemDrop droppable in possibleDrops)
            {
                droppable.getItem().instantiateItem();
            }
        }

        public void kill(ref List<LootableItem> onScreenItems)
        {

            foreach (itemDrop droppable in possibleDrops)
            {
                player.gainExp((int)exp);
                if (droppable.isdropped())
                {
                    onScreenItems.Add(new LootableItem(game, bounds.X, bounds.Y, droppable.getItem()));
                    break;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            bounds = new Rectangle((int)AS.ax, (int)AS.ay, 32, 32);

            if (hitCounter > 0)
            {
                --hitCounter;
                AS.tint = Color.Red;
            }else
                AS.tint = Color.White;

            chase();
            attack();
            if (!player.isAttacking)
                hasBeenHit = false;
            if (!hidden)
            {
                if (islinked)
                    linkToKeyboard();
                    AS.Draw(gameTime);
            }

            if (txtCounter > 0)
            {
                spriteBatch.DrawString(Content.Load<SpriteFont>("Description"), "Miss", missText, Color.White);
                --txtCounter;
                --missText.Y;
            }
        }
    }
}
