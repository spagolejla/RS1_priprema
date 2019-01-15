using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class FakturaStavkeController : Controller
    {
		MojContext _context;

		public FakturaStavkeController(MojContext context)
		{
			_context = context;
		}

		public IActionResult Index(int id)
        {
			FakturaStavkeIndexVM model = new FakturaStavkeIndexVM
			{
				Rows = _context.FakturaStavka.Where(x => x.FakturaID == id).Select(x => new FakturaStavkeIndexVM.Row {
					proizvod = x.Prozivod.Naziv,
					kolicina = x.Kolicina,
					cijena = x.Prozivod.Cijena,
					iznosSaPopustom = (x.Prozivod.Cijena * x.Kolicina)  * (1-x.PopustProcenat/100),
					fakturaStavkaID = x.Id,
					popust = x.PopustProcenat
				}).ToList(),
				FakturaID = id
			};
            return PartialView("Index",model);
        }

		public IActionResult Uredi(int id)
		{
			FakturaStavkeUrediVM model = new FakturaStavkeUrediVM();
			model.proizvodi = _context.Proizvod.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value=x.Id.ToString(),
				Text=x.Naziv+x.Cijena.ToString(),

			}).ToList();
			model.FakturaStavkaID = id;
			return PartialView("Uredi", model);
		}

		public IActionResult SnimiPromjene(int Proizvod,float Kolicina,int  FakturaStavka)
		{
			FakturaStavka fak = _context.FakturaStavka.Where(x => x.Id == FakturaStavka).FirstOrDefault();
			fak.ProizvodID = Proizvod;
			fak.Kolicina = Kolicina;
			_context.FakturaStavka.Update(fak);
			_context.SaveChanges();
			return Redirect("/Faktura/Detalji?="+fak.FakturaID);
		}
	}
}