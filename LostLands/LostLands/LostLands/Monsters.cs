using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LostLands
{
    /// <summary>
    /// Handler of all monsters
    /// </summary>
    class Monsters
    {

        Player player;
        List<Mob> Mobs = new List<Mob>();
        List<LootableItem> onScreenItems;
        Game game;

        public Monsters(Game game, ref Player p1, ref List<LootableItem> onScreenItems)
        {
            this.game = game;
            player = p1;

            #region Put monsters in their zones

            //Skels
            Mobs.Add(new Mob(game, 280, 600, 1, ref player));
            Mobs.Add(new Mob(game, 220, 550, 1, ref player));
            //Mobs.Add(new Mob(game, 25, 610, 1, ref player));
            //Mobs.Add(new Mob(game, 75, 620, 1, ref player));
            //Mobs.Add(new Mob(game, 200, 600, 1, ref player));

            //Scorps
            Random r = new Random();
            for (int i = 0; i < 5; ++i)
            {
                int x = r.Next(616, 1025), y = r.Next(619, 776); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 3, ref player));
            }

            //Salaman
            for (int i = 0; i < 5; ++i)
            {
                int x = r.Next(623, 1053), y = r.Next(772, 1154); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 4, ref player));
            }

            ///Flan
            for (int i = 0; i < 7; ++i)
            {
                int x = r.Next(1318, 1750), y = r.Next(594, 776); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 5, ref player));
            }

            ///Plants
            for (int i = 0; i < 3; ++i)
            {
                int x = r.Next(1274, 1693), y = r.Next(897, 1122); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 6, ref player));
            }

            ///Worms
            for (int i = 0; i < 10; ++i)
            {
                int x = r.Next(1849, 2179), y = r.Next(125, 273); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 7, ref player));
            }

            ///Full Flan
            for (int i = 0; i < 4; ++i)
            {
                int x = r.Next(1929, 2197), y = r.Next(1240, 1378); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 8, ref player));
            }

            ///Evil Purple Chick
            for (int i = 0; i < 1; ++i)
            {
                int x = r.Next(135, 239), y = r.Next(1204, 1242); // Returns a random number from 0-99
                Mobs.Add(new Mob(game, x, y, 9, ref player));
            }
            ///Mummys
            Mobs.Add(new Mob(game, 2000, 1004, 2, ref player));
            Mobs.Add(new Mob(game, 2045, 1045, 2, ref player));
            Mobs.Add(new Mob(game, 1980, 1080, 2, ref player));

            #endregion

            this.onScreenItems = onScreenItems;
        }

        public void checkMonsters(int Type, ref int HowMany)
        {
            int counter=0;
            foreach (Mob mob in Mobs)
            {
                if (mob.getMonsterType() == Type)
                    ++counter;
            }

            if (counter < HowMany)
            {
                HowMany = HowMany - counter;
            }
            else
                HowMany = 0;
        }

        public void addMonsters(int Type, int HowMany)
        {
            Random r = new Random();

            checkMonsters(Type, ref HowMany);

            switch (Type)
            {
                case 1:
                    //Skels 1
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(25, 280), y = r.Next(600, 620); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 1, ref player));
                    }
                    break;
                case 2:
                    ///Mummys 2
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(1980, 2045), y = r.Next(1004, 1080); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 2, ref player));
                    }
                    break;
                case 3:
                    //Scorps 3
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(616, 1025), y = r.Next(619, 776); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 3, ref player));
                    }
                    break;
                case 4:
                    //Salaman 4
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(623, 1053), y = r.Next(772, 1154); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 4, ref player));
                    }
                    break;
                case 5:
                    ///Flan 5
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(1318, 1750), y = r.Next(594, 776); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 5, ref player));
                    }
                    break;
                case 6:
                    ///Plants 6
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(1274, 1693), y = r.Next(897, 1122); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 6, ref player));
                    }
                    break;
                case 7:
                    ///Worms 7
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(1849, 2179), y = r.Next(125, 273); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 7, ref player));
                    }
                    break;
                case 8:
                    ///Full Flan 8
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(1929, 2197), y = r.Next(1240, 1378); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 8, ref player));
                    }
                    break;
                case 9:
                    ///Evil Purple Chick 9
                    for (int i = 0; i < HowMany; ++i)
                    {
                        int x = r.Next(135, 239), y = r.Next(1204, 1242); // Returns a random number from 0-99
                        Mobs.Add(new Mob(game, x, y, 9, ref player));
                    }
                    break;
            }
        }

        public void drawMonster(GameTime gameTime)
        {
            player.inCombat = false;
            //loop through mobs
            foreach (Mob mob in Mobs)
            {
                //check every other mob for collision
                foreach (Mob otherMob in Mobs)
                {
                    mob.checkMobCollision(otherMob);
                }
                mob.AS.update();
                mob.Draw(gameTime);
                if (!player.Slash.animationOver && player.isAttacking && player.doesDamage)
                {
                    if (mob.checkHit())
                    {
                        mob.hit();
                        if (mob.health <= 0)
                        {
                            mob.kill(ref onScreenItems);
                            player.monsterKilled(mob);
                            Mobs.Remove(mob);
                            break;
                        }
                    }
                }
                else if (!player.doesDamage && mob.checkHit())
                {
                    mob.miss();
                    mob.AS.update();
                    mob.Draw(gameTime);
                }
            }
        }
    }
}
