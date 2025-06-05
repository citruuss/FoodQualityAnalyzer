using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IShrinkable //2 интерфейс
    {
        void Remove(FoodProduct product) { } 
        void Remove(int index) { }
        void Remove() { } //перегрузка 2
    }
}
