using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3.ViewModels
{
    public class TourViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public Tour Tour { get; set; }
    }
}
