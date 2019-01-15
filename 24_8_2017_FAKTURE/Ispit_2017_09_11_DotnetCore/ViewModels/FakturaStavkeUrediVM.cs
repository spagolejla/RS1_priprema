using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
	public class FakturaStavkeUrediVM
	{
		public int FakturaStavkaID { get; set; }
		public List<SelectListItem> proizvodi {get; set;}
    }
}
