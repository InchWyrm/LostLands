using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class LClickItem : Item
    {

        public LClickItem(Game game, int id) : base(game, 1, id) {
            type = 1;
            this.id = id;
            setItem();
            itemDescription = getName() + "\nType: " + getWordType() + "\nGold: " + value + " Dam: " + damage;
        }
        
        public void setItem()
        {
            switch(id){
                case 1:
                    ItemPic = Content.Load<Texture2D>(@"items/Sword1");
                    Name = "Dinky dagger";
                    value = 1;
                    damage = 1;
                    break;
                case 2:
                    ItemPic = Content.Load<Texture2D>(@"items/scimitar");
                    Name = "The Scimi'";
                    value = 3;
                    damage = 3;
                    break;
                case 3:
                    ItemPic = Content.Load<Texture2D>(@"items/Sword2");
                    Name = "Bronse Sword";
                    value = 3;
                    damage = 10;
                    break;
            }
        }
        
    }
}
