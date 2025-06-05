using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Fruit : FoodProduct
    {
        public bool IsPopular { get; private set; }
        public bool IsExhotic { get; private set; }
        public string Type { get; private set; }

        public Fruit(string name, int daysUntilBad, int maxLifeDays, bool isPopular, bool isExhotic)
            : base(name, daysUntilBad, maxLifeDays)
        {
            IsPopular = isPopular;
            IsExhotic = isExhotic;
            Type = nameof(Fruit);
        }

        public override double GetQuality()
        {
            double quality = DaysUntilBad / (double)MaxLifeDays * 100;
            if (IsPopular) quality *= 1.1;
            return Math.Min(100, Math.Round(quality, 2));
        }
    }
}