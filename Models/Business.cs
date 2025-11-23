using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAChristmasHam.Models
{
    public class Business : User       
    {
        public Business()
        {
            Type = UserType.Business; //  Sätts automatiskt
        }
    }
}

