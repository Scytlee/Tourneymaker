using System.Collections.Generic;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePerson(PersonModel personModel);
        List<PersonModel> LoadPersonModels();
    }
}
