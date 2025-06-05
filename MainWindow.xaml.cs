using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Model.Core;
using Model.Data;
using System.IO;

namespace FoodQualityAnalyzer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void UpdateAnalyzeButtonState()
        {
            AnalyzeButton.IsEnabled = VegetablePanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    FruitPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    MeatPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    BackeryPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true);
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = new FoodProduct[0];

            foreach (var checkBox in FindVisualChildren<CheckBox>(this))
            {
                if (checkBox.IsChecked == true)
                {
                    foreach (var p in Model.Core.FoodQualityAnalyzer.Products)
                    {
                        if (p.Name == checkBox.Content.ToString())
                        {
                            Array.Resize(ref selectedProducts, selectedProducts.Length + 1);
                            selectedProducts[selectedProducts.Length - 1] = p;
                        }
                    }
                }
            }

            if (selectedProducts.Length > 0)
            {
                var chartWindow = new ChartWindow(selectedProducts, this);
                chartWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите хотя бы один продукт", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public CheckBox FindCheckBox(string content)
        {
            foreach (var child in FindVisualChildren<CheckBox>(this))
            {
                if (child.Content?.ToString() == content)
                    return child;
            }
            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ProductAdd_Json.LoadProducts();
            foreach (var p in Model.Core.FoodQualityAnalyzer.Products)
            {
                AddProduct(p);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var add = new AddProductWindow();
            add.Show();
        }

        public void AddProduct(FoodProduct product)
        {
            CheckBox NewCheckBox = new CheckBox()
            {
                Content = product.Name,
                Margin = new Thickness(20, 2, 0, 2),
                IsChecked = false
            };

            NewCheckBox.Checked += (s, e) => UpdateAnalyzeButtonState();
            NewCheckBox.Unchecked += (s, e) => UpdateAnalyzeButtonState();

            NewCheckBox.Checked += (s, e) => UpdateDeleteButtonState();
            NewCheckBox.Unchecked += (s, e) => UpdateDeleteButtonState();

            if (product is Vegetable) VegetablePanel.Children.Add(NewCheckBox);
            if (product is Fruit) FruitPanel.Children.Add(NewCheckBox);
            if (product is Meat) MeatPanel.Children.Add(NewCheckBox);
            if (product is Backery) BackeryPanel.Children.Add(NewCheckBox);
        }

        private void UpdateDeleteButtonState()
        {
            DeleteButton.IsEnabled = VegetablePanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    FruitPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    MeatPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true) ||
                                    BackeryPanel.Children.OfType<CheckBox>().Any(c => c.IsChecked == true);
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Уверены, что хотите удалить выбранные продукты?",
                   "Предупреждение",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            var prods = new List<FoodProduct>();
            var cb = new List<CheckBox>();
            foreach (var checkBox in FindVisualChildren<CheckBox>(this))
            {
                if (checkBox.IsChecked == true)
                {
                    foreach (var p in Model.Core.FoodQualityAnalyzer.Products)
                    {
                        if (p.Name == checkBox.Content.ToString())
                        {
                            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProductsJson", p.Name + ".json");
                            File.Delete(filepath);

                            filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProductsXML", p.Name + ".xml");
                            File.Delete(filepath);

                            prods.Add(p);
                            cb.Add(checkBox);

                            break;
                        }
                    }
                }
            }
            
            for (int i = 0; i < prods.Count; i++)
            {
                var p = prods[i];
                Model.Core.FoodQualityAnalyzer.Remove(p);
                cb[i].IsChecked = false;
                if (p is Vegetable) VegetablePanel.Children.Remove(cb[i]);
                if (p is Fruit) FruitPanel.Children.Remove(cb[i]);
                if (p is Meat) MeatPanel.Children.Remove(cb[i]);
                if (p is Backery) BackeryPanel.Children.Remove(cb[i]);
            }
        }
    }
}
