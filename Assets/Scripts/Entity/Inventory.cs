using System.Collections.Generic;
using System.Linq;

namespace Game.Model
{
    public class Inventory
    {
        public List<Slot> Items = new List<Slot>();

        public class Slot 
        {
            public string Id;
            public int Quantity;

            public void Init() 
            {
                Id = "";
                Quantity = 0;
            }

            public void Init(string id, int quantity)
            {
                Id = id;
                Quantity = quantity;
            }

            public Slot() 
            {
                Id = "";
                Quantity = 0;
            }

            public Slot(string id, int quantity)
            {
                Id = id;
                Quantity = quantity;
            }
        }

        public void UpdateInventory(Slot slot) 
        {
            Slot data = Items.FirstOrDefault(x => x.Id == slot.Id);
            if (data == null)
            {
                Items.Add(slot);
            }
            else 
            {
                int curr = data.Quantity + slot.Quantity;

                if (curr == 0)
                {
                    Items.Remove(data);
                    return;
                }

                if (curr < 0)
                {
                    return;
                }

                if(curr > 0)
                {
                    data.Quantity += slot.Quantity;
                    return;
                }
            }

        }
    }
}
