using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagemantBiblioteca
{
    public class Imprumut
    {
        public int ImprumutID { get; set; }
        public int MembruID { get; set; }
        public int CarteID { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime? DataScadenta { get; set; }
    }
}
