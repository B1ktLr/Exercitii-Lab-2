using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagemantBiblioteca
{
    public class SituatieImprumut
    {
        public int MembruID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Carte { get; set; }
        public DateTime? DataImprumut { get; set; }
        public DateTime? DataScadenta { get; set; }
        public DateTime? DataReturnare { get; set; }
        public string Observatii { get; set; }
    }
}
