using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary.Models
{
    interface IModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        int id { get; set; }
    }
}
