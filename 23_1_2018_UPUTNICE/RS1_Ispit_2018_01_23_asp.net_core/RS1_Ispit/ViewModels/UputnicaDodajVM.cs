using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class UputnicaDodajVM
    {
		public List<SelectListItem> ljekari { get; set; }
		public List<SelectListItem> pacijenti { get; set; }
		public List<SelectListItem> pretrage { get; set; }
		public DateTime Datum { get; set; } 
    }
}
