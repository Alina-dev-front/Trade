using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Trade.Models;


namespace Trade.Repositories
{
    public class FileHandler
    {
        public string GetUserFileDirectory()
        {
            static string GetUserHomePath()
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify);
            }

            var homePath = GetUserHomePath();
            var programPath = Path.Combine(homePath, ".TradeProject");

            if (!Directory.Exists(programPath))
            {
                Directory.CreateDirectory(programPath);
            }

            var csvPath = Path.Combine(programPath, "ProductData.csv");
            return csvPath;
        }

        public string GetUserJsonPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var programPath = Path.Combine(path, ".TradeProject");
            var jsonPath = Path.Combine(programPath, "Products.json");
            return jsonPath;
        }
    }
}
