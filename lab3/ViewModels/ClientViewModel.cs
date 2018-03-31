using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public Client Client { get; set; }
    }
}
