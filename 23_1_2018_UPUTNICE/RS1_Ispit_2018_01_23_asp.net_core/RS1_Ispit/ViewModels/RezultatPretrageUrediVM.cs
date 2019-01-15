using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class RezultatPretrageUrediVM
    {
		public string vrstaVrijednosti { get; set; }
		public string pretraga { get; set; }
		public List<SelectListItem> vrijednostiPretrage { get; set; }
		public double? izmjerenaVrijednost { get; set; }
		public int rezultatPretrageId { get; set; }
    }
}
