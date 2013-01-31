using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class TileSprite : OurGamesDarwableComponent
    {
        Texture2D Texture;
        int Number, aX, aY, Xoffset, Yoffset, width, height;
        bool walkable;

        public bool isWalkable { get { return walkable; } }

        public TileSprite(Game game, int aX, int aY, int Number)
            : base(game)
        {
            this.Number = Number;
            this.aX = aX;
            this.aY = aY;
            setImage();
        }

        public void setTile(int aX, int aY, int Number)
        {
            this.Number = Number;
            this.aX = aX;
            this.aY = aY;

            setImage();
        }

        public int getID()
        {
            return Number;
        }

        public void setXoffset(int Xoffset)
        {
            this.Xoffset = Xoffset;
        }

        public void setYoffset(int Yoffset)
        {
            this.Yoffset = Yoffset;
        }

        public void setImage()
        {
            switch (Number)
            {
                case 7100:
                    //dbLoadImage("assets\\landScape\\house.png", 7100);
                    Texture = Content.Load<Texture2D>(@"landScape\house");
                    walkable = false;
                    break;
                case 7102:
                    //dbLoadImage("assets\\landScape\\full tree1.png", 7102);
                    Texture = Content.Load<Texture2D>(@"landScape\full tree1");
                    walkable = true;
                    break;
                case 7103:
                    //dbLoadImage("assets\\landScape\\fattree.png", 7103);
                    Texture = Content.Load<Texture2D>(@"landScape\fattree");
                    walkable = false;
                    break;
                case 7104:
                    //dbLoadImage("assets\\landScape\\straightRockWall.png", 7104);
                    Texture = Content.Load<Texture2D>(@"landScape\straightRockWall");
                    walkable = false;
                    break;
                case 7105:
                    //dbLoadImage("assets\\landScape\\nsRockWall.png", 7105);
                    Texture = Content.Load<Texture2D>(@"landScape\nsRockWall");
                    walkable = false;
                    break;
                case 7106:
                    //dbLoadImage("assets\\smoke1.png", 7106);
                    //Texture = Content.Load<Texture2D>(@"landScape\smoke1");
                    break;
                case 7107:
                    //dbLoadImage("assets\\smoke2.png", 7107);
                    //Texture = Content.Load<Texture2D>(@"landScape\smoke2");
                    break;
                case 7101:
                    //dbLoadImage("assets\\landScape\\tree.png", 7101);
                    Texture = Content.Load<Texture2D>(@"landScape\tree");
                    walkable = false;
                    break;
                case 7108:
                    //dbLoadImage("assets\\landScape\\talltree.png", 7108);
                    Texture = Content.Load<Texture2D>(@"landScape\talltree");
                    walkable = false;
                    break;
                case 7109:
                    //dbLoadImage("assets\\landScape\\straightRockWallShort2.png", 7109);
                    Texture = Content.Load<Texture2D>(@"landScape\straightRockWallShort2");
                    walkable = false;
                    break;
                case 7110:
                    //dbLoadImage("assets\\landScape\\waterLine.png", 7110);
                    Texture = Content.Load<Texture2D>(@"landScape\waterLine");
                    walkable = false;
                    break;
                case 7111:
                    //dbLoadImage("assets\\landScape\\sandLine.png", 7111);
                    Texture = Content.Load<Texture2D>(@"landScape\sandLine");
                    walkable = false;
                    break;
                case 7112:
                    //dbLoadImage("assets\\landScape\\straightRockWallShortR.png", 7112);
                    Texture = Content.Load<Texture2D>(@"landScape\straightRockWallShortR");
                    walkable = false;
                    break;
                case 7113:
                    //dbLoadImage("assets\\landScape\\DeadTree.png", 7113);
                    Texture = Content.Load<Texture2D>(@"landScape\DeadTree");
                    walkable = false;
                    break;
                case 7114:
                    //dbLoadImage("assets\\landScape\\waterLineFlat.png", 7114);
                    Texture = Content.Load<Texture2D>(@"landScape\waterLineFlat");
                    walkable = false;
                    break;
                case 7115:
                    Texture = Content.Load<Texture2D>(@"landScape\hay");
                    walkable = false;
                    break;
                case 7116:
                    Texture = Content.Load<Texture2D>(@"landScape\bag");
                    walkable = false;
                    break;
                case 7117:
                    Texture = Content.Load<Texture2D>(@"landScape\shed");
                    walkable = false;
                    break;
                case 7118:
                    Texture = Content.Load<Texture2D>(@"landScape\hay1");
                    walkable = false;
                    break;
                case 7119:
                    Texture = Content.Load<Texture2D>(@"landScape\handWagon");
                    walkable = false;
                    break;
                case 7120:
                    Texture = Content.Load<Texture2D>(@"landScape\bucketWater");
                    walkable = false;
                    break;
                case 7121:
                    Texture = Content.Load<Texture2D>(@"landScape\beeHive");
                    walkable = false;
                    break;
                case 7122:
                    Texture = Content.Load<Texture2D>(@"landScape\Sign");
                    walkable = false;
                    break;
                default:
                    Texture = null;
                    walkable = true;
                    break;
            }
            if (Texture != null)
            {
                width = Texture.Bounds.Width;
                height = Texture.Bounds.Height;
            }
        }

        public int getX()
        {
            return aY * 32 - Xoffset;
        }
        public int getY()
        {
            return aX * 32 - Yoffset;
        }

        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, new Vector2(aY * 32 - Xoffset, aX * 32 - Yoffset), Color.White);
            base.Draw(gameTime);
        }
    }
}
