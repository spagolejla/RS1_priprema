using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class RezultatiPretrageUrediVM
    {
		public string Pretraga { get; set; }
		public double? IzmjerenaVrijednost { get; set; }
		public int? ModalitetId { get; set; }
		public List<SelectListItem> Modaliteti { get; set; }
		public int RezultatPretrageId { get; set; }
		public int VrstaVr { get; set; }
		public int UputnicaId { get; set; }




	}
}
