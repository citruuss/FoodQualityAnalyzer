using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Meat : FoodProduct
    {
        public bool IsRed { get; private set; }
        public bool WithBones { get; private set; }
        public string Type { get; private set; }

        public Meat(string name, int daysUntilBad, int maxLifeDays, bool isRed, bool withBones)
            : base(name, daysUntilBad, maxLifeDays)
        {
            IsRed = isRed;
            WithBones = withBones;
            Type = nameof(Meat);
        }

        public override double GetQuality()
        {
            double quality = DaysUntilBad / (double)MaxLifeDays * 100;
            if (quality <= 10) return 0; //Если мясу осталось жить меньше дня, оно испортилось
            return Math.Round(quality, 2);
        }
    }
}