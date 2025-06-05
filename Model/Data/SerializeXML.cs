using Microsoft.Windows.Themes;
using Model.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model.Data
{
    public class SerializeXML : Serialize
    {
        private static int _lastReportNumber = 0;
        public override string Extension => "xml"; //переопределение 9

        [XmlInclude(typeof(VegetableDTO))]
        [XmlInclude(typeof(FruitDTO))]
        [XmlInclude(typeof(MeatDTO))]
        [XmlInclude(typeof(BackeryDTO))]

        public class ProductDTO
        {
            public string Name { get; set; }
            public int DaysUntilBad { get; set; }
            public int MaxLifeDays { get; set; }
            public string Type { get; set; }

            public ProductDTO(FoodProduct p)
            {
                Name = p.Name;
                DaysUntilBad = p.DaysUntilBad;
                MaxLifeDays = p.MaxLifeDays;
                Type = nameof(FoodProduct);
            }
            public ProductDTO() { }
        }

        public class VegetableDTO : ProductDTO
        {
            public bool NeedToCook { get; set; }
            public bool IsHard { get; set; }

            public VegetableDTO(Vegetable p)
            {
                Name = p.Name;
                DaysUntilBad = p.DaysUntilBad;
                MaxLifeDays = p.MaxLifeDays;
                NeedToCook = p.NeedToCook;
                IsHard = p.IsHard;
                Type = nameof(Vegetable);
            }
            public VegetableDTO() { }
        }

        public class FruitDTO : ProductDTO
        {
            public bool IsPopular { get; set; }
            public bool IsExhotic { get; set; }

            public FruitDTO(Fruit p)
            {
                Name = p.Name;
                DaysUntilBad = p.DaysUntilBad;
                MaxLifeDays = p.MaxLifeDays;
                IsPopular = p.IsPopular;
                IsExhotic = p.IsExhotic;
                Type = nameof(Fruit);
            }
            public FruitDTO() { }
        }

        public class MeatDTO : ProductDTO
        {
            public bool IsRed { get; set; }
            public bool WithBones { get; set; }

            public MeatDTO(Meat p)
            {
                Name = p.Name;
                DaysUntilBad = p.DaysUntilBad;
                MaxLifeDays = p.MaxLifeDays;
                IsRed = p.IsRed;
                WithBones = p.WithBones;
                Type = nameof(Meat);
            }
            public MeatDTO() { }
        }
        public class BackeryDTO : ProductDTO
        {
            public bool IsSweet { get; set; }
            public bool IsBread { get; set; }

            public BackeryDTO(Backery p)
            {
                Name = p.Name;
                DaysUntilBad = p.DaysUntilBad;
                MaxLifeDays = p.MaxLifeDays;
                IsSweet = p.IsSweet;
                IsBread = p.IsBread;
                Type = nameof(Backery);
            }
            public BackeryDTO() { }
        }

        public class ReportDTO
        {
            public ProductDTO[] Products { get; set; }
            public double MaxQuality { get; set; }
            public double MinQuality { get; set; }
            public double AverageQuality { get; set; }
            public double MedianQuality { get; set; }

            public ReportDTO(FoodProduct[] products)
            {
                Products = products.Select(p => p switch
                {
                    Vegetable v => new VegetableDTO(v),
                    Fruit f => new FruitDTO(f),
                    Meat m => new MeatDTO(m),
                    Backery b => new BackeryDTO(b),
                    _ => new ProductDTO(p)
                }).ToArray();
                var analyzer = new FoodQualityAnalyzer();
                MaxQuality = analyzer.MaxQuality(products);
                MinQuality = analyzer.MinQuality(products);
                AverageQuality = analyzer.AverageQuality(products);
                MedianQuality = analyzer.MedianQuality(products);
            }
            public ReportDTO() { }
        }

        private void Ser1<T>(T obj) //обобщенный тип данных 2
        {
            using var writer = new StreamWriter(FilePath);
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(writer, obj);
            writer.Close();
        }
        public override void Ser(FoodProduct product) //переопределение 10
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProductsXML"));
            if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
            SelectFile(product.Name);
            if (product is Vegetable v)
            {
                var prod = new VegetableDTO(v);
                Ser1<VegetableDTO>(prod);
            }
            else if (product is Fruit f)
            {
                var prod = new FruitDTO(f);
                Ser1<FruitDTO>(prod);
            }
            else if (product is Meat m)
            {
                var prod = new MeatDTO(m);
                Ser1<MeatDTO>(prod);
            }
            else if (product is Backery b)
            {
                var prod = new BackeryDTO(b);
                Ser1<BackeryDTO>(prod);
            }

        }

        public override void GenerateReport(FoodProduct[] products) //переопределение 11
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReportsXML"));

            _lastReportNumber = GetLastReportNumber();
            _lastReportNumber++;

            string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string FileName = $"Отчет_№{_lastReportNumber}_от_{time}";

            GenerateReport(products, FileName);
        }

        public override void GenerateReport(FoodProduct[] products, string filename) //переопределение 12
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReportsXML"));

            SelectFile(filename);

            // Сохраняем данные в XML
            var prod = new ReportDTO(products);
            using var writer = new StreamWriter(FilePath);
            var ser = new XmlSerializer(typeof(ReportDTO));
            ser.Serialize(writer, prod);
            writer.Close();
        }
    }
}
