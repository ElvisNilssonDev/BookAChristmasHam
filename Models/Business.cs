using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAChristmasHam.Models
{
    internal class Business : User//Arv från User-klassen
    {
        private int Id { get; set; }//Företagets unika "ID"
        public string CompanyName { get; set; }//Företagets namn t.ex Pågen AB
    }
}
