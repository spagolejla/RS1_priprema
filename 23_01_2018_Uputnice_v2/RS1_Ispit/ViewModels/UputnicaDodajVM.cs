using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class UputnicaDodajVM
    {
		public List<SelectListItem>	 ljekari { get; set; }
		public int ljekarId { get; set; }
		public List<SelectListItem> pacijenti { get; set; }
		public int pacijentId { get; set; }

		public List<SelectListItem> vrstePretrage { get; set; }
		public int vrstaPretrageId { get; set; }

		[Required(ErrorMessage ="Datum je obavezan!")]
		[DisplayFormat(DataFormatString ="dd.MM.yyyy")]
		public DateTime? DatumUputnie { get; set; }
	}
}
