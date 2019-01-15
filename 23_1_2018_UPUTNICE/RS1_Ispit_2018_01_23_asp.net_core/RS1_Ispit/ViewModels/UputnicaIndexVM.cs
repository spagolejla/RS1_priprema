using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class UputnicaIndexVM
    {
		public List<Row> Rows { get; set; }

		public class Row {
			public string uputio { get; set; }
			public string pacijent { get; set; }
			public string vrstaPretrage { get; set; }
			public string datumRezultata { get; set; }
			public int uputnicaID { get; set; }

		}
    }
}
