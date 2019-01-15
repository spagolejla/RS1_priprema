using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
	public class FakturaStavkeIndexVM
	{
		public List<Row> Rows {get;set;}
		public int FakturaID { get; set; }
		public class Row {
			public string proizvod { get; set; }
			public double cijena { get; set; }
			public double kolicina { get; set; }
			public double popust { get; set; }
			public double iznosSaPopustom { get; set; }
		    public int fakturaStavkaID { get; set; }

		}
	}
}
