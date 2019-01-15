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
    public class FakturaController : Controller
    {
		MojContext _context;

		public FakturaController(MojContext context) {
			_context = context;
		}

		public IActionResult Index()
        {
			FakturaIndexVM model = new FakturaIndexVM
			{
				Rows = _context.Faktura.Select(x => new FakturaIndexVM.Row
				{
					datum = x.Datum.ToString("dd.MM.yyyy"),
					klijent = x.Klijent.ImePrezime,
					FakturaID=x.Id
				}).ToList(),
				
			};

            return View("Index",model);
        }
		public IActionResult Dodaj()
		{
			FakturaDodajVM model = new FakturaDodajVM();
			model.klijenti = _context.Klijent.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.ImePrezime
			}).ToList();

			model.ponude = _context.Ponuda.Where(x=>x.FakturaID==null).Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Klijent.ImePrezime+ " "+x.Datum.ToString("dd.MM.yyyy")

			}).ToList();

			return View("Dodaj",model);
		}

		public IActionResult Snimi(int Klijent,DateTime Datum,int? Ponuda)
		{
			Faktura nova = new Faktura();
			nova.KlijentID = Klijent;
			nova.Datum = Datum;
			_context.Faktura.Add(nova);
			_context.SaveChanges();
			if (Ponuda!=null)
			{
				Ponuda odbPonuda = _context.Ponuda.Where(x => x.Id == Ponuda).FirstOrDefault();
				odbPonuda.FakturaID = nova.Id;


				List<FakturaStavka> stavke = _context.PonudaStavka.Where(x => x.PonudaId == Ponuda).Select(s => new FakturaStavka
				{
					FakturaID=nova.Id,
					Kolicina=s.Kolicina,
					ProizvodID=s.ProizvodId,
					PopustProcenat=5
				}).ToList();

				_context.AddRange(stavke);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		public IActionResult Detalji(int id)
		{
			FakturaDetaljiVM model = _context.Faktura.Where(x => x.Id == id).Select(s => new FakturaDetaljiVM
			{
				Klijent=s.Klijent.ImePrezime,
				Datum=s.Datum.ToString("dd.MM.yyyy"),
				FakturaID=s.Id
			}).FirstOrDefault();




			return View("Detalji",model);
		}
	}
}