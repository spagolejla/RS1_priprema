using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class CasIndexVM
    {

		public List<Row> Rows { get; set; }
		public class Row {
			public DateTime Datum { get; set; }
			public string AkademskaGodina { get; set; }
			public string Predmet { get; set; }
		    public int CasId { get; set; }
			public int BrojStudenata { get; set; }
			public int UkupnoStudenata { get; set; }
			public float? ProsjenaOcjena { get; set; }


		}
	}
}
