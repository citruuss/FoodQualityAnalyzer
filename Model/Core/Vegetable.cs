﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Vegetable : FoodProduct
    {
        public bool NeedToCook { get; private set; }
        public bool IsHard { get; private set; }
        public string Type { get; private set; }
        public Vegetable(string name, int daysUntilBad, int maxLifeDays, bool needToCook, bool isHard)
            : base(name, daysUntilBad, maxLifeDays)
        {
            NeedToCook = needToCook;
            IsHard = isHard;
            Type = nameof(Vegetable);
        }

        public override double GetQuality() //переопределение 4
        {
            double quality = DaysUntilBad / (double)MaxLifeDays * 100;
            return Math.Round(quality, 2);
        }
    }
}
