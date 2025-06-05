﻿using Model.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class Serialize //абстрактный класс 2
    {
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }
        public abstract string Extension { get; }

        public void SelectFolder(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            FolderPath = path;
        }

        public void SelectFile(string name)
        {
            string path = Path.Combine(FolderPath, name + "." + Extension);
            if (!File.Exists(path)) File.Create(path).Close();
            FilePath = path;
        }
        public abstract void Ser(FoodProduct product);

        public abstract void GenerateReport(FoodProduct[] products);
        public abstract void GenerateReport(FoodProduct[] products, string filename);

        protected int GetLastReportNumber()
        {
            var files = Directory.GetFiles(FolderPath);
            return files.Length;
        }
    }
}