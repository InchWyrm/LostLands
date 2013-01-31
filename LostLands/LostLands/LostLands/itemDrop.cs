using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LostLands
{
    class itemDrop
    {

        Item item;
        int chance;

        public itemDrop(Item item, int chance)
        {
            this.item = item;
            this.chance = chance;
        }

        public bool isdropped()
        {
            Random r = new Random();
            int rand = r.Next(0, 100); // Returns a random number from 0-99
            if (rand <= chance)
                return true;
            return false;
        }

        public Item getItem()
        {
            return item;
        }

    }
}
