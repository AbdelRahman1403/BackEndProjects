using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.SeedingData
{
    public static class SeedingData
    {
        public static bool SeedData(GymDbContext gymContext)
        {
            var AnyPlanes = gymContext.Plans.Any();
            var AnyCategories = gymContext.Categories.Any();
            if (AnyCategories && AnyPlanes)
            {
                return false;
            }

            if (!AnyCategories)
            {
                var categories = LoadDataFromJsonFile<Category>("categories.json");
                gymContext.Categories.AddRange(categories);
            }
            if (!AnyPlanes)
            {
                var plans = LoadDataFromJsonFile<Plan>("plans.json");
                gymContext.Plans.AddRange(plans);
            }

            return gymContext.SaveChanges() > 0;

        }

        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "files" , fileName);

            var jsonData = File.ReadAllText(filePath);
            if(!File.Exists(filePath) || string.IsNullOrEmpty(jsonData))
            {
                throw new FileNotFoundException();
            }

            return JsonSerializer.Deserialize<List<T>>(jsonData , 
                   new JsonSerializerOptions() {PropertyNameCaseInsensitive = true }) ?? new List<T>();
        }
    }
}
