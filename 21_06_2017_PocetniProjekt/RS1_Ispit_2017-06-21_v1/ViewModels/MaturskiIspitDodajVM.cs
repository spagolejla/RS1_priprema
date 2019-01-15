using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class MaturskiIspitDodajVM
    {
		public List<SelectListItem> ispitivaci { get; set; }
		public int IspitivacId { get; set; }
		[Required(ErrorMessage="Morate unijeti datum")]
		[DisplayFormat(DataFormatString ="dd.MM.yyyy")]
		public DateTime? Datum { get; set; }
		public List<SelectListItem> odjeljenja { get; set; }
		public int odjeljenjeId { get; set; }


	}
}
