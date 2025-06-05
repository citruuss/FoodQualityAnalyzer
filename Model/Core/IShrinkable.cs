using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IShrinkable
    {
        static void Remove(FoodProduct product) { }
        static void Remove(int index) { }
        static void Remove() { }
    }
}
