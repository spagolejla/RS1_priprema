using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class RezultatiPretrageIndexVM
    {
		public List<Row> Rows { get; set; }
		public bool jelZakljucano { get; set; }


		public class Row
		{
			public string Pretraga { get; set; }
			public int RezultatPretrageId { get; set; }

			public double? NumerickaVrijednost { get; set; }
			public string modalitet { get; set; }
			public string JMJ { get; set; }
			
		}

	}
}
