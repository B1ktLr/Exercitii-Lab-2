using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Membru
    {
        public int MembruID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataInscriere { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
    }
}
