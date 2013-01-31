using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class RClickItem : Item
    {

        public RClickItem(Game game, int id) : base(game, 1, id) {
            type = 2;
            this.id = id;
            setItem();
            itemDescription = getName() + "\nType: " + getWordType() + "\nGold: " + value + " Def: " + defense;
        }

        public void setItem()
        {
            switch(id){
                case 1:
                    ItemPic = Content.Load<Texture2D>(@"items/Sheild1");
                    Name = "The Door";
                    value = 5;
                    defense = 5;
                    break;
                case 2:
                    ItemPic = Content.Load<Texture2D>(@"items/shinySheild");
                    Name = "Shiny!";
                    value = 5;
                    defense = 15;
                    break;
                case 3:
                    ItemPic = Content.Load<Texture2D>(@"items/sheild(3)");
                    Name = "Royal Protection";
                    value = 5;
                    defense = 30;
                    break;
            }
        }

    }
}
