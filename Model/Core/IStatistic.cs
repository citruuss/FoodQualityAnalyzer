using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IStatistic
    {
        static double MaxQuality(FoodProduct[] products) { return 0; }
        static double MinQuality(FoodProduct[] products) { return 0; }
        static double AverageQuality(FoodProduct[] products) { return 0; }
        static double MedianQuality(FoodProduct[] products) { return 0; }
    }
}