using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3.ViewModels
{
    public class TourKindViewModel
    {
        public IEnumerable<TourKind> TourKinds { get; set; }
        public TourKind TourKind { get; set; }
    }
}
