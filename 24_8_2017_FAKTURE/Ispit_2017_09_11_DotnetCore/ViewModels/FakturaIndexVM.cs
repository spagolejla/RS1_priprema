using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class FakturaIndexVM
    {
		public List<Row> Rows { get; set; }
	
		public class Row {
			public string datum { get; set; }
			public string klijent { get; set; }
			public int FakturaID { get; set; }
		}
    }
}
