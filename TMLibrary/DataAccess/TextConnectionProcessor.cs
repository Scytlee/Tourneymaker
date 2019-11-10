using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Models;

// ReSharper disable once CheckNamespace
namespace TMLibrary.DataAccess.TextHelpers
{
    public static class TextConnectionProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.Nickname = cols[1];
                p.FirstName = cols[2];
                p.LastName = cols[3];
                p.DiscordTag = cols[4];
                p.EmailAddress = cols[5];

                output.Add(p);
            }

            return output;
        }

        public static void SaveToPersonModelsFile(this List<PersonModel> personModels)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel person in personModels)
            {
                lines.Add($"{ person.Id },{ person.Nickname },{ person.FirstName },{ person.LastName },{ person.DiscordTag },{ person.EmailAddress }");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }
    }
}
