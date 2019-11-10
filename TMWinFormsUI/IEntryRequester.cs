using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Models;

namespace TMWinFormsUI
{
    public interface IEntryRequester
    {
        void EntryComplete(EntryModel entry);
    }
}
