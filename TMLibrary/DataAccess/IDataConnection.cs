using System.Collections.Generic;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePerson(PersonModel newPerson);
        void CreateEntry(EntryModel newEntry);
        List<PersonModel> LoadPersonModels();
    }
}
