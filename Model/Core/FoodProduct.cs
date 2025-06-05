using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    //Свойства для названия
    //количества дней до истечения срока
    //максимального срока годности
    public abstract class FoodProduct
    {
        public string Name { get; private set; }
        public int DaysUntilBad { get; private set; }
        public int MaxLifeDays { get; private set; }

        protected FoodProduct(string name, int daysUntilBad, int maxLifeDays)
        {
            Name = name;
            DaysUntilBad = daysUntilBad;
            MaxLifeDays = maxLifeDays;
        }

        public abstract double GetQuality();
    }
}
