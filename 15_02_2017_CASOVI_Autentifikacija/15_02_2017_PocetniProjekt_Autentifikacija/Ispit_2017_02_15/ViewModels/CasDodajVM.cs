using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class CasDodajVM
    {
		 public string	 Nastavnik { get; set; }
		[Required(ErrorMessage ="Datum je obavezan")]
		[DisplayFormat(DataFormatString="dd.MM.yyyy")]
		public DateTime? Datum { get; set; }
		public List<SelectListItem> AkPredmeti { get; set; }
		public int CasId { get; set; }
		[Required(ErrorMessage = "Obavezno polje")]
		public int? AkPredmetId { get; set; }

	}
}
