using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.Helper;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ispit_2017_02_15.Controllers
{
    public class CasController : Controller
    {
		MojContext _context;
		Nastavnik nastavnik
		{
			get { return Autentifikacija.GetLogiraniKorisnik(HttpContext); }

		}

		
		public CasController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index()
        {
			if (nastavnik==null)
			{
				Redirect("/Login/Index");
			}

			CasIndexVM model = new CasIndexVM
			{ //.Where(x=>x.Angazovan.NastavnikId==nastavnik.Id)
				Rows = _context.OdrzaniCasovi.Include(s => s.Angazovan).Select(x => new CasIndexVM.Row {
					Datum = x.Datum,
					AkademskaGodina = x.Angazovan.AkademskaGodina.Opis,
					Predmet = x.Angazovan.Predmet.Naziv,
					CasId = x.Id,
					BrojStudenata = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == x.Id && w.Prisutan).Count(),
					UkupnoStudenata = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == x.Id).Count(),
					ProsjenaOcjena = _context.SlusaPredmet.Where(z => z.Angazovan.PredmetId == x.Angazovan.PredmetId).Average(z=> (float?)z.Ocjena)??0

				}).ToList()
			};

            return View("Index",model);
        }

		public IActionResult Dodaj()
		{
			if (nastavnik == null)
			{
				Redirect("/Login/Index");
			}
			CasDodajVM model = new CasDodajVM(){
				AkPredmeti = _context.Angazovan.Where(x=>x.NastavnikId==nastavnik.Id).Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.AkademskaGodina.Opis + "/" + x.Predmet.Naziv,

				}).ToList(),
				Datum = DateTime.Now,
				Nastavnik=nastavnik.Ime+" "+nastavnik.Prezime
			};

			return View("Dodaj", model);
		}


		public IActionResult Snimi(CasDodajVM model)
		{
			if (nastavnik == null)
			{
				Redirect("/Login/Index");
			}
			if (!ModelState.IsValid)
			{
				model.AkPredmeti = _context.Angazovan.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.AkademskaGodina.Opis + "/" + x.Predmet.Naziv,

				}).ToList();

				return View("Dodaj", model);
			}

			OdrzaniCas noviCas = new OdrzaniCas
			{
				AngazovanId = (int)model.AkPredmetId,
				Datum = (DateTime)model.Datum

			};
			_context.OdrzaniCasovi.Add(noviCas);
			_context.SaveChanges();

			List<OdrzaniCasDetalji> noviDetalji = _context.SlusaPredmet.Where(x => x.AngazovanId == noviCas.AngazovanId).Select(x=>new OdrzaniCasDetalji {
				OdrzaniCasId=noviCas.Id,
				BodoviNaCasu=0,
				Prisutan=true,
				SlusaPredmetId=x.Id

			}).ToList();

			_context.OdrzaniCasDetalji.AddRange(noviDetalji);
			_context.SaveChanges();

			return Redirect("/Cas/Index");
		}

		public IActionResult Uredi(int id)
		{
			if (nastavnik == null)
			{
				Redirect("/Login/Index");
			}
			CasUrediVM model = _context.OdrzaniCasovi.Where(x => x.Id == id).Select(x => new CasUrediVM
			{
				Datum=x.Datum,
				Nastavnik=x.Angazovan.Nastavnik.Ime+" "+x.Angazovan.Nastavnik.Prezime,
				AkPredmet=x.Angazovan.AkademskaGodina.Opis+"/"+x.Angazovan.Predmet.Naziv,
				CasId=x.Id
			}).FirstOrDefault();
			return View("Uredi",model);
		}

		public IActionResult SnimiPromjene(CasUrediVM model)
		{
			if (nastavnik == null)
			{
				Redirect("/Login/Index");
			}
			if (!ModelState.IsValid)
			{
				return View("Uredi", model);
			}

			OdrzaniCas cas = _context.OdrzaniCasovi.Where(x => x.Id == model.CasId).FirstOrDefault();
			cas.Datum = (DateTime)model.Datum;
			_context.OdrzaniCasovi.Update(cas);
			_context.SaveChanges();
			return Redirect("/Cas/Index");
		}
	}
}