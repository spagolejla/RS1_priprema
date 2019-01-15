using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RS1.Ispit.Web.Models;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class RezultatPretrageIndexVM
    {
		public bool jelzakljucano { get; set; }
		public List<Row> Rows { get; set; }
		public class Row {
			public string modalitet;
			public VrstaVrijednosti vrstaVr;

			public string pretraga { get; set; }
			public string izmjerenaVrijednost { get; set; }
			public string JMJ { get; set; }
			public int rezultatPretrageId { get; set; }
		}
    }
}
