using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostLands
{
    class StoryScreen : Screen
    {
        Player player;
        Texture2D Sarge;
        Texture2D Merc;
        TypeText dialog;
        int dnum = 0;
        button revenge, honor, riches, personal, skip;

        public StoryScreen(Game game, ref Player p1)
            : base(game)
        {
            player = p1;
            player.X = 100;
            player.Y = 450;
            player.walking = true;
            player.inStory = true;
            Sarge = Content.Load<Texture2D>(@"NPC\Sarge");
            Merc = Content.Load<Texture2D>(@"MercRoom1");
            // buttons for choice of why here
            revenge = new button(90, 600 - 50, Content.Load<Texture2D>(@"buttons/revenge"), Content.Load<Texture2D>(@"buttons/revenge(2)"), Content.Load<Texture2D>(@"buttons/revenge(3)"));
            honor = new button(239, 600 - 50, Content.Load<Texture2D>(@"buttons/honor"), Content.Load<Texture2D>(@"buttons/honor(2)"), Content.Load<Texture2D>(@"buttons/honor(3)"));
            riches = new button(388, 600 - 50, Content.Load<Texture2D>(@"buttons/riches"), Content.Load<Texture2D>(@"buttons/riches(2)"), Content.Load<Texture2D>(@"buttons/riches(3)"));
            personal = new button(537, 600 - 50, Content.Load<Texture2D>(@"buttons/personal"), Content.Load<Texture2D>(@"buttons/personal(2)"), Content.Load<Texture2D>(@"buttons/personal(3)"));
            skip = new button(657, 32, Content.Load<Texture2D>(@"buttons/skip"), Content.Load<Texture2D>(@"buttons/skip"), Content.Load<Texture2D>(@"buttons/skip"));
        }

        public void setPlayer(ref Player p1)
        {
            player = p1;
            dialog = new TypeText(250, 330, "Sarge:\nLong time no see " + player.getName() + "\n", description);
        }

        public Player getPlayer()
        {
            return player;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void movePlayer(GameTime gameTime)
        {
            if (player.X < 230 && dnum == 0)
            {
                player.walkRight();
                if (player.X > 200)
                    Sarge = Content.Load<Texture2D>(@"NPC\Sarge(L)");
            }
            else
            {

                if (dnum >= 10 && dialog.textDone)
                {
                    player.walking = true;
                    player.walkLeft();
                }
                else
                {
                    player.walking = false;

                    doDialog();
                }

            }
            player.Draw(gameTime);
        }

        public void doDialog()
        {
            dialog.addText();
            spriteBatch.Draw(Merc, dialog.textBox, Color.Black);
            spriteBatch.DrawString(description, dialog.text, new Vector2(dialog.x, dialog.y), Color.Silver);
            if (dialog.textDone && Mouse.GetState().LeftButton == ButtonState.Released && old.LeftButton == ButtonState.Pressed)
            {
                switch (dnum)
                {
                    case 0:
                        dialog.setText(dialog.x, dialog.y + 20, "Sarge:\nSo you know without a doubt that Nagoste is approaching.\n");
                        break;
                    case 1:
                        dialog.setText(dialog.x - 150, dialog.y - 20, player.getName() + ":\nThey killed my family.\nI vowed that they would pay!\n");
                        break;
                    case 2:
                        dialog.setText(dialog.x + 20, dialog.y + 20, "Sarge:\nI still remember the day Nagoste attacked Maran.\n");
                        break;
                    case 3:
                        dialog.setText(dialog.x, dialog.y + 20, "Sarge:\nThey surprised us all and took many lives.\n");
                        break;
                    case 4:
                        dialog.setText(dialog.x, dialog.y - 90, "Sarge:\nBut why did you come to the mercenaries?\nI thought you were still with Pherom's military  \nAnd that sorry excuse for a commander.\n");
                        break;
                    case 5:
                        dialog.setText(dialog.x - 50, dialog.y + 20, player.getName() + ":\nHe forced me out.\nI can almost believe he works for the Nagoste\nwith the stupid decisions he makes.\n");
                        break;
                    case 6:
                        dialog.setText(dialog.x - 25, dialog.y + 20, "Sarge:\nHa! I agree! " + player.getName() + ".\nIf you do not mind my asking you\nwhat is your motivation for joining the Merc Unit?\n");
                        break;
                    case 8:
                        dialog.setText(dialog.x, dialog.y, "Sarge:\nYour journey begins after you walk back out that door!!\n");
                        break;
                    case 9:
                        dialog.setText(dialog.x, dialog.y, player.getName() + ":\nI will see you later Sarge...\n");
                        break;
                }
                if (dnum != 7)
                    ++dnum;
            }
        }

        public void getChoice()
        {
            // answer to Sarge question
            if (revenge.isReleased())
            {
                ++dnum;
                dialog.setText(dialog.x, dialog.y, "Sarge:\nI like that, a simple answer from a combat soldier\n");
            }
            else if (honor.isReleased())
            {
                ++dnum;
                dialog.setText(dialog.x, dialog.y, "Sarge:\nI like that, a simple answer from a combat soldier\n");
            }
            else if (riches.isReleased())
            {
                ++dnum;
                dialog.setText(dialog.x, dialog.y, "Sarge:\nI like that, a simple answer from a combat soldier\n");
            }
            else if (personal.isReleased())
            {
                ++dnum;
                dialog.setText(dialog.x, dialog.y, "Sarge:\nI like that, a simple answer from a combat soldier\n");
            }
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(Merc, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(font2, "Lost Lands", new Vector2(130, 80), Color.Brown);

            spriteBatch.Draw(skip.getState(), skip.buttonBounds, Color.White);
            if (skip.isReleased())
            {
                dnum = 9;
            }

            movePlayer(gameTime);
            if (dnum == 7 && dialog.textDone)
            {
                spriteBatch.Draw(revenge.getState(), revenge.buttonBounds, Color.White);
                spriteBatch.Draw(honor.getState(), honor.buttonBounds, Color.White);
                spriteBatch.Draw(riches.getState(), riches.buttonBounds, Color.White);
                spriteBatch.Draw(personal.getState(), personal.buttonBounds, Color.White);
                getChoice();
            }
            if (player.X <= 5)
            {
                player.walking = false;
                player.inStory = false;
                active = false;
            }

            spriteBatch.Draw(Sarge, new Vector2(300, 430), Color.White);

            spriteBatch.End();

            old = Mouse.GetState();
            base.Draw(gameTime);
        }

    }
}
