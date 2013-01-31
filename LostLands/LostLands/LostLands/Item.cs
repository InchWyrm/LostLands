using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LostLands
{
    class Item : OurGamesDarwableComponent
    {
        public Texture2D ItemPic;
        public int type, id, value, damage, defense, heal, Speed;
        public int potionType, stacks;
        protected string Name, itemDescription;
        protected bool used= false;

        public Item(Game game, int type, int id) : base(game)
        {
            this.type = type;
            this.id = id;
            Name = "";
        }

        public void instantiateItem()
        {
            switch (type)
            {
                case 1:
                    this.setItem(new LClickItem(game, id));
                    break;
                case 2:
                    this.setItem(new RClickItem(game, id));
                    break;
                case 3:
                    this.setItem(new UsableItem(game, id));
                    break;
            }
        }

        public void resetUsableOutput()
        {
            if(potionType == 1)
                itemDescription = itemDescription = getName() + "\nType: " + getWordType() + " S:" + stacks + "\nHeals: " + heal + "%";
            else if (potionType == 2)
                itemDescription = itemDescription = getName() + "\nType: " + getWordType() + " S:" + stacks + "\nStam: " + heal;
        }

        public void addStack(int num)
        {
            stacks += num;
            resetUsableOutput();
        }

        public void useUsable(ref Player p1)
        {
            if (type == 3)
            {
                switch (potionType)
                {
                    case 1:
                        if (p1.getCurentHealth() < p1.getMaxHealth() && usable())
                        {
                            --stacks;
                            p1.heal(heal);
                            p1.setHPCounter();
                            resetUsableOutput();
                        }
                        break;
                    case 2:
                        if (p1.getCurentStam() < p1.getMaxStam() && usable())
                        {
                            --stacks;
                            p1.drinkStamPot(heal);
                            p1.setStamPotionCounter();
                            resetUsableOutput();
                        }
                        break;
                }
            }
        }

        public bool usable() { return !used; }

        public void use()
        {
            used = true;
        }

        public void makeUsable()
        {
            used = false;
        }

        public void setUsed(bool use) { used = use; }

        public void checkPotion(ref Player player)
        {
            switch(potionType){
                case 1:
                    if (player.healthPotionCounter > 0)
                        use();
                    else
                        makeUsable();
                break;
                case 2:
                if (player.stamPotionCounter > 0)
                    use();
                else
                    makeUsable();
                break;
            }
            
        }

        public void setItem(Item item)
        {
            ItemPic = item.ItemPic;
            type = item.type;
            id = item.id;
            value = item.value; 
            damage = item.damage;
            defense = item.defense;
            heal = item.heal;
            potionType = item.potionType;
            stacks = item.stacks;
            Name = item.Name;
            itemDescription = item.itemDescription;
        }

        public int getType() { return type; }
        public string getWordType()
        {
            if (type == 1)
                return "Left Click";
            else if (type == 2)
                return "Right Click";
            else
                return "Usable";
        }
        public string getName() { return Name; }
        public int getID() { return id; }
        public Texture2D image() { return ItemPic; }
        public override string ToString()
        {

            return itemDescription;
        }
    }
}
