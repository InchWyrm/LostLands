using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace LostLands
{
    class TypeText
    {
        public String text = "", message;
        int timer, wait, count;
        public SpriteFont font;
        public int x, y;
        bool done = false;
        public Rectangle textBox;

        public TypeText(int x, int y, int timer, String message, SpriteFont font)
        {
            this.message = message;
            wait = timer;
            this.font = font;
            this.x = x;
            this.y = y;
        }
        public TypeText(int x, int y, String message, SpriteFont font)
        {
            this.message = message;
            wait = 0;
            this.font = font;
            this.x = x;
            this.y = y;
            int longestline=0, lines=0, last=0;

            for (int i = 0; i < message.Length; ++i)
            {
                if (message[i] == '\n')
                {
                    ++lines;
                    if (i - longestline > longestline)
                    {
                        if (last == 0)
                            longestline = i;
                        else
                            longestline = i - last;
                    }
                    last = i+1;
                }
            }
            textBox = new Rectangle(x-20, y-10, longestline * 10, lines*35);
        }

        public void setText(int x, int y, String message)
        {
            text = "";
            this.message = message;
            this.x = x;
            this.y = y;
            count = 0;
            setTextBox(x,y);
            done = false;

            
        }

        private void setTextBox(int x, int y)
        {
            int longestline = 0, lines = 0, last = 0;
            this.x = x;
            this.y = y;
            for (int i = 0; i < message.Length; ++i)
            {
                if (message[i] == '\n')
                {
                    ++lines;
                    if (i - last > longestline)
                    {
                        if (last == 0)
                            longestline = i;
                        else
                            longestline = i - last;
                    }
                    last = i + 1;
                }
            }
            textBox = new Rectangle(x - 20, y - 10, longestline * 10, lines * 35);
        }

        public void resetPosition(int X, int Y)
        {
            this.x = X;
            this.y = Y;
            setTextBox(x, y);
        }

        public bool textDone
        {
            get { return done; }
        }
        private void addCharacter()
        {
            if (count < message.Length)// Check to see if we are done copying
                text += message[count++];// copy character by character including what Text originally had
            else
                done = true;
        }
        public void forceDone()
        {
            text = message;
            count = message.Length;
            done = true;
        }
        public void addText()
        {
            if (timer == wait)
            {
                timer = 0;
                addCharacter();
            }
            else
            {
                ++timer;
            }
        }
    }
}
