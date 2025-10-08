using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Media;
using FoodQualityAnalyzer;

namespace WpfLibrary1
{
    public class Class1
    {
        public class ReportWindowViewModel
        {
            public string[] ProductNames { get; }
            public SeriesCollection ColumnValues { get; }
            public SeriesCollection PieValues { get; }
            public SeriesCollection LineValues { get; }

            public double AverageQuality { get; }
            public double MaxQuality { get; }
            public double MinQuality { get; }
            public int ProductsCount { get; }

            public Func<ChartPoint, string> PointLabel { get; }

            public ReportWindowViewModel(FoodProduct[] products)
            {
                // Данные продуктов
                ProductNames = products.Select(p => p.Name).ToArray();
                var qualities = products.Select(p => p.GetQuality()).ToArray();

                // Статистика
                ProductsCount = products.Length;
                AverageQuality = Math.Round(qualities.Average(), 2);
                MaxQuality = Math.Round(qualities.Max(), 2);
                MinQuality = Math.Round(qualities.Min(), 2);

                // Настройка столбчатой диаграммы
                ColumnValues = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Качество",
                Values = new ChartValues<double>(qualities),
                DataLabels = true,
                Fill = Brushes.Green
            }
        };

                // Настройка круговой диаграммы
                PieValues = new SeriesCollection();
                var random = new Random();
                foreach (var product in products)
                {
                    PieValues.Add(new PieSeries
                    {
                        Title = product.Name,
                        Values = new ChartValues<double> { product.GetQuality() },
                        DataLabels = true,
                        Fill = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(150, 250),
                            (byte)random.Next(150, 250),
                            (byte)random.Next(150, 250)))
                    });
                }

                // Настройка линейного графика
                LineValues = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Качество",
                Values = new ChartValues<double>(qualities),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 15,
                Stroke = Brushes.Blue
            }
        };

                // Формат подписей для круговой диаграммы
                PointLabel = chartPoint =>
                    $"{chartPoint.SeriesView.Title}: {chartPoint.Y:P}";
            }
        }
    }

}
