using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Delivery
{
    public class Item
    {
        private readonly int Id;
        private readonly string Name;
        private readonly int GroupId;
        private readonly string Group;
        private readonly int SubgroupId;
        private readonly string Subgroup;
        private readonly int Quantity;
        private readonly string Unit;
        private readonly float PriceNoTva;
        private readonly int Tva;
        private readonly float PriceWithTva;
        private readonly string Provider;

        public Item(int id, string name, int groupId, string group, int subgroupId, string subgroup, int quantity, string unit,
            float pnTva, int tva, float pwTva, string provider)
        {
            this.Id = id;
            this.Name = name;
            this.GroupId = groupId;
            this.Group = group;
            this.SubgroupId = subgroupId;
            this.Subgroup = subgroup;
            this.Quantity = quantity;
            this.Unit = unit;
            this.PriceNoTva = pnTva;
            this.Tva = tva;
            this.PriceWithTva = pwTva;
            this.Provider = provider;
        }

        public int GetId()
        {
            return this.Id;
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetGroupId()
        {
            return this.GroupId;
        }

        public string GetGroup()
        {
            return this.Group;
        }

        public int GetSubgroupId()
        {
            return this.SubgroupId;
        }

        public string GetSubgroup()
        {
            return this.Subgroup;
        }

        public int GetQuantity()
        {
            return this.Quantity;
        }

        public string GetUnit()
        {
            return this.Unit;
        }

        public float GetPriceNoTva()
        {
            return this.PriceNoTva;
        }

        public int GetTva()
        {
            return this.Tva;
        }

        public float GetPriceWithTva()
        {
            return this.PriceWithTva;
        }

        public string GetProvider()
        {
            return this.Provider;
        }
    }
}
