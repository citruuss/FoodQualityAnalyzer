using Model.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model.Data
{
    public class SerializeJson : Serialize
    {
        private static int _lastReportNumber = 0;
        public override string Extension => "json";

        public SerializeJson() { }

        public override void Ser(FoodProduct product)
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProductsJson"));
            SelectFile(product.Name);

            var json = JObject.FromObject(product);
            File.WriteAllText(FilePath, json.ToString());
        }

        public override void GenerateReport(FoodProduct[] products)
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReportsJson"));

            _lastReportNumber = GetLastReportNumber();
            _lastReportNumber++;

            string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string FileName = $"Отчет_№{_lastReportNumber}_от_{time}";

            GenerateReport(products, FileName);
        }

        public override void GenerateReport(FoodProduct[] products, string filename)
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReportsJson"));

            SelectFile(filename);

            // Сохраняем данные в JSON
            var data = new
            {
                Products = products
            };
            var json = JObject.FromObject(data);
            json.Add("MaxQuality", FoodQualityAnalyzer.MaxQuality(products));
            json.Add("MinQuality", FoodQualityAnalyzer.MinQuality(products));
            json.Add("AverageQuality", FoodQualityAnalyzer.AverageQuality(products));
            json.Add("MedianQuality", FoodQualityAnalyzer.MedianQuality(products));
            File.WriteAllText(FilePath, json.ToString());
        }

        public static FoodProduct Deser(JToken json)
        {
            FoodProduct NewProduct = null;

            switch (json["Type"].ToString())
            {
                case nameof(Vegetable):
                    NewProduct = new Vegetable(json["Name"].ToString(),
                        int.Parse(json["DaysUntilBad"].ToString()),
                        int.Parse(json["MaxLifeDays"].ToString()),
                        bool.Parse(json["NeedToCook"].ToString()),
                        bool.Parse(json["IsHard"].ToString()));
                    break;
                case nameof(Fruit):
                    NewProduct = new Fruit(json["Name"].ToString(),
                        int.Parse(json["DaysUntilBad"].ToString()),
                        int.Parse(json["MaxLifeDays"].ToString()),
                        bool.Parse(json["IsPopular"].ToString()),
                        bool.Parse(json["IsExhotic"].ToString()));
                    break;
                case nameof(Meat):
                    NewProduct = new Meat(json["Name"].ToString(),
                        int.Parse(json["DaysUntilBad"].ToString()),
                        int.Parse(json["MaxLifeDays"].ToString()),
                        bool.Parse(json["IsRed"].ToString()),
                        bool.Parse(json["WithBones"].ToString()));
                    break;
                case nameof(Backery):
                    NewProduct = new Backery(json["Name"].ToString(),
                        int.Parse(json["DaysUntilBad"].ToString()),
                        int.Parse(json["MaxLifeDays"].ToString()),
                        bool.Parse(json["IsSweet"].ToString()),
                        bool.Parse(json["IsBread"].ToString()));
                    break;
            }

            return NewProduct;
        }
    }
}
