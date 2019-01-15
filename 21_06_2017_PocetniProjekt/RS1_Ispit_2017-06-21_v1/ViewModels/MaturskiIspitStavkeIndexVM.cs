using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class MaturskiIspitStavkeIndexVM
    {
		public List<Row> Rows { get; set; }
		public int MaturskiIspitId { get; set; }
		public class Row {
			public string Ucenik { get; set; }
			public int OpciUspjeh { get; set; }
			public float? Bodovi { get; set; }
			public bool Oslobodjen { get; set; }
			public int MaturskiIspitStavkaId { get; set; }

		}
	}
}
