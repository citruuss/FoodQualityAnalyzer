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
    public abstract class FoodProduct //абстрактный класс 1
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
        public static FoodProduct[] operator +(FoodProduct[] products, FoodProduct product) //перегрузка оператора 1
                                                                                            //добавляет пробукт к массиву
        {
            if (products == null) return new FoodProduct[] { product };
            var pr = products.ToList();
            pr.Add(product);
            return pr.ToArray();
        }

        public static FoodProduct[] operator -(FoodProduct[] products, FoodProduct product) //перегрузка оператора 2
        {
            if (products == null) return new FoodProduct[0];
            var pr = products.ToList();
            pr.Remove(product);
            return pr.ToArray();
        }

        public abstract double GetQuality();
    }
}
