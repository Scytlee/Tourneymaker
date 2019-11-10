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
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel person = new PersonModel
                {
                    Id = int.Parse(cols[0]),
                    Nickname = cols[1],
                    FirstName = cols[2],
                    LastName = cols[3],
                    DiscordTag = cols[4],
                    EmailAddress = cols[5]
                };

                output.Add(person);
            }

            return output;
        }

        public static List<EntryModel> ConvertToEntryModels(this List<string> lines)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            List<EntryModel> output = new List<EntryModel>();

            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                EntryModel entry = new EntryModel
                {
                    Id = int.Parse(cols[0]),
                    EntryName = cols[1]
                };

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    entry.EntryMembers.Add(people.First(x => x.Id == int.Parse(id)));
                }

                output.Add(entry);
            }

            return output;
        }

        public static void SaveToPersonModelsFile(this List<PersonModel> people)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            List<string> lines = new List<string>();

            foreach (PersonModel person in people)
            {
                lines.Add($"{ person.Id },{ person.Nickname },{ person.FirstName },{ person.LastName },{ person.DiscordTag },{ person.EmailAddress }");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }

        public static void SaveToEntryModelsFile(this List<EntryModel> entries)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            List<string> lines = new List<string>();

            foreach (EntryModel entry in entries)
            {
                lines.Add($"{ entry.Id },{ entry.EntryName },{ ConvertPeopleListToString(entry.EntryMembers) }");
            }

            File.WriteAllLines(GlobalConfig.EntriesFile.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            foreach (PersonModel person in people)
            {
                output += $"{ person.Id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
