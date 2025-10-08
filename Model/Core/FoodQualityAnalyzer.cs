﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model.Core;
using Newtonsoft.Json;
using System.IO;

namespace Model.Core
{
    public partial class FoodQualityAnalyzer : ISpreadable
    {
        public static FoodProduct[] Products { get; private set; }

        static FoodQualityAnalyzer()
        {
            Products = new FoodProduct[0];
        }

        public void Add(FoodProduct product)
        {
            Products = Products + product;
        }

        public void Add(FoodProduct[] products) //Перегрузка 1
        {
            if (products != null)
            {
                foreach (var p in products) Add(p);
            }
        }
    }

    public partial class FoodQualityAnalyzer : IShrinkable
    {
        public void Remove(FoodProduct product) //удаление конкретного продукта
        {
            Products = Products - product;
        }

        public void Remove(int index) //удаление продукта под номером
        {
            if (index < 0 || index > Products.Length) return;

            var arr = new FoodProduct[Products.Length - 1];
            for (int i = 0; i < Products.Length; i++)
            {
                if (i < index - 1) arr[i] = Products[i];
                else if (i > index - 1) arr[i] = Products[i - 1];
            }
            Products = arr;
        }
        public void Remove() //удаление последнего продукта
        {
            FoodProduct[] _products = new FoodProduct[0];
            if (Products != null)
            {
                _products = new FoodProduct[Products.Length - 1];
                Array.Copy(Products, _products, Products.Length - 1);
            }
            Products = _products;
        }
    }

    public partial class FoodQualityAnalyzer : IStatistic
    {
        public double MaxQuality(FoodProduct[] products) => products.Select(p => p.GetQuality()).Max();

        public double MinQuality(FoodProduct[] products) => products.Select(p => p.GetQuality()).Min();

        public double AverageQuality(FoodProduct[] products) => products.Select(p => p.GetQuality()).Average();

        public double MedianQuality(FoodProduct[] products)
        {
            var sorted = products.Select(p => p.GetQuality()).OrderBy(q => q).ToList();
            int count = sorted.Count;
            if (count == 0) return 0;
            if (count % 2 == 1) return sorted[count / 2];
            return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
        }
    }
}
