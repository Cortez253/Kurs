using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    public partial class Avto
    {
        public override string ToString()
        {
            string FullName = Car_brand + " " + Model;
            return FullName;
        }
    }

    public partial class Clients
    {
        public override string ToString()
        {
            string FullName = Surname + " " + Name;
            return FullName;
        }
    }

    public partial class Services
    {
        public override string ToString()
        {

            return Name_service;
        }
    }
}
