using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
	public class FakturaDodajVM
	{
		public List<SelectListItem> klijenti {get;set;}
		public List<SelectListItem> ponude { get; set; }

	}
}
