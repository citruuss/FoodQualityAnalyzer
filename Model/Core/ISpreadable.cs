using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ISpreadable
    {
        public static FoodProduct[] Products { get; }
        static void Add(FoodProduct product) { }
        static void Add(FoodProduct[] products) { }
    }
}