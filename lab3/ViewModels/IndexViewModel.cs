using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<TourKind> TourKinds { get; set; }
    }
}
