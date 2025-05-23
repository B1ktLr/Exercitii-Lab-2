using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagemantBiblioteca
{
    public class Returnare
    {
        public int ReturnareID { get; set; }
        public int ImprumutID { get; set; }
        public DateTime DataReturnare { get; set; }
        public string Observatii { get; set; }
    }
}
