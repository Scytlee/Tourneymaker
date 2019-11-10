using System.Collections.Generic;
using System.Linq;
using TMLibrary.DataAccess.TextHelpers;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public class TextConnection : IDataConnection
    {
        public void CreatePerson(PersonModel newPerson)
        {
            // Load the PersonModels text file and convert the text to List<PersonModel>
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            newPerson.Id = currentId;

            people.Add(newPerson);

            people.SaveToPersonModelsFile();
        }
    }
}
