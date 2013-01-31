using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LostLands
{
    class UsableItem : Item
    {

        public UsableItem(Game game, int id)
            : base(game, 3, id)
        {
            type = 3;
            this.id = id;
            setItem();
        }

        public void setItem()
        {
            switch (id)
            {
                case 1:
                    ItemPic = Content.Load<Texture2D>(@"items/HPotion");
                    Name = "My first potion";
                    value = 1;
                    potionType = 1;
                    stacks = 1;
                    heal = 7;
                    break;
                case 2:
                    ItemPic = Content.Load<Texture2D>(@"items/SPotion");
                    Name = "**WAPOW**";
                    value = 1;
                    potionType = 2;
                    stacks = 1;
                    heal = 35;
                    break;
                case 3:
                    ItemPic = Content.Load<Texture2D>(@"items/HPotion");
                    Name = "Chugalug";
                    value = 1;
                    potionType = 1;
                    stacks = 1;
                    heal = 15;
                    break;
                case 4:
                    ItemPic = Content.Load<Texture2D>(@"items/SPotion");
                    Name = "!Stam Up!";
                    value = 5;
                    potionType = 2;
                    stacks = 1;
                    heal = 20;
                    break;
                case 5:
                    ItemPic = Content.Load<Texture2D>(@"items/HPotion");
                    Name = "Nuka";
                    value = 1;
                    potionType = 1;
                    stacks = 2;
                    heal = 50;
                    break;
            }
            setDesc();
        }

        public void setDesc()
        {
            if (potionType == 1)
                itemDescription = itemDescription = getName() + "\nType: " + getWordType() + " S:" + stacks + "\nHeals: " + heal+"%";
            else if(potionType == 2)
                itemDescription = itemDescription = getName() + "\nType: " + getWordType() + " S:" + stacks + "\nStam: " + heal;
        }

    }
}
