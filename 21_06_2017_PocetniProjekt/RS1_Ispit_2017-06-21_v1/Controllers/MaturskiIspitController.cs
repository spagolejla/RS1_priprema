using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;

namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class MaturskiIspitController : Controller
    {
		private MojContext _context;

		public MaturskiIspitController (MojContext db)
		{
			_context = db;
		}

        public IActionResult Index()
        {
			MaturskiIspitIndexVM model = new MaturskiIspitIndexVM {
				Rows=_context.MaturskiIspit.Select(x=>new MaturskiIspitIndexVM.Row {
					Datum=x.Datum,
					Odjeljenje=x.Odjeljenje.Naziv,
					Ispitivac=x.Nastavnik.ImePrezime,
					ProsjecniBodovi=0,
					MaturskiIspitId=x.Id
				}).ToList()
			};
            return View("Index",model);
        }

		public IActionResult Dodaj()
		{
			MaturskiIspitDodajVM model = new MaturskiIspitDodajVM();
			model.ispitivaci = _context.Nastavnik.Select(x => new SelectListItem {
				Value=x.Id.ToString(),
				Text=x.ImePrezime
			}).ToList();

			model.odjeljenja = _context.Odjeljenje.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv
			}).ToList();

			model.Datum = DateTime.Now;
			return View("Dodaj", model);
		}

		public IActionResult Snimi(MaturskiIspitDodajVM model)
		{
			if (!ModelState.IsValid)
			{
				model.ispitivaci = _context.Nastavnik.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.ImePrezime
				}).ToList();

				model.odjeljenja = _context.Odjeljenje.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv
				}).ToList();

				return View("Dodaj", model);
			}


			MaturskiIspit novi = new MaturskiIspit();
			novi.NastavnikId = model.IspitivacId;
			novi.OdjeljenjeId = model.odjeljenjeId;
			novi.Datum = (DateTime)model.Datum;
			_context.MaturskiIspit.Add(novi);
			_context.SaveChanges();

			List<MaturskiIspitStavka> stavke = _context.UpisUOdjeljenje.Where(x => x.OdjeljenjeId == model.odjeljenjeId && x.OpciUspjeh > 1).Select(s => new MaturskiIspitStavka
			{
				MaturskiIspitId = novi.Id,
				UpisUOdjeljenjeId = s.Id,
				Oslobodjen = s.OpciUspjeh == 5 ? true : false,
				Bodovi = null
			}).ToList();

			_context.MaturskiIspitStavka.AddRange(stavke);
			_context.SaveChanges();

			return Redirect("/MaturskiIspit/Index");
		}

		public IActionResult Detalji(int id)
		{
			MaturskiIspitDetaljiVM model = _context.MaturskiIspit.Where(x=>x.Id==id).Select(x => new MaturskiIspitDetaljiVM {
				Ispitivac=x.Nastavnik.ImePrezime,
				Datum=x.Datum,
				odjeljenje=x.Odjeljenje.Naziv,
				MaturskiIspitId=x.Id
			}).FirstOrDefault();

			return View("Detalji",model);
		}
	}
}