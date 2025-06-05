using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Backery : FoodProduct
    {
        public bool IsSweet { get; private set; }
        public bool IsBread { get; private set; }
        public string Type { get; private set; }

        public Backery(string name, int daysUntilBad, int maxLifeDays, bool isSweet, bool isBread)
            : base(name, daysUntilBad, maxLifeDays)
        {
            IsSweet = isSweet;
            IsBread = isBread;
            Type = nameof(Backery);
        }

        public override double GetQuality() //переопределение 1
        {
            double quality = DaysUntilBad / (double)MaxLifeDays * 100;
            return Math.Round(quality, 2);
        }
    }
}
