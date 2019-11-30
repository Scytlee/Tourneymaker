using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace TMWPFUI
{
    public static class EventAggregationProvider
    {
        public static EventAggregator TMEventAggregator { get; set; } = new EventAggregator();
    }
}
