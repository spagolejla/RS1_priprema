using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class UputnicaDetaljiVM
    {
		public bool jelzakljucano { get; set; }

		public string datumUputnice { get; set; }
		public string pacijent { get; set; }
		public string datumRezultata { get; set; }
		public int uputnicaId { get; set; }
    }
}
