using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_2017_02_15.Controllers
{
    public class OdrzaniCasDetaljiController : Controller
    {
		MojContext _context;
		public OdrzaniCasDetaljiController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index(int id)
        {
			OdrzaniCasDetaljiIndexVM model = new OdrzaniCasDetaljiIndexVM {
				Rows=_context.OdrzaniCasDetalji.Where(x=>x.OdrzaniCasId==id).Select(x=>new OdrzaniCasDetaljiIndexVM.Row {
					Student = x.SlusaPredmet.UpisGodine.Student.Ime + " " + x.SlusaPredmet.UpisGodine.Student.Prezime,
					Prisutan = x.Prisutan,
					Bodovi=x.BodoviNaCasu,
					OdrzaniCasDetaljiId = x.Id
				}).ToList()
			};
            return PartialView("Index",model);
        }

		public IActionResult UcenikJePrisutan(int id)
		{
			OdrzaniCasDetalji casDetalji = _context.OdrzaniCasDetalji.Where(x => x.Id == id).FirstOrDefault();
			casDetalji.Prisutan = true;
			_context.OdrzaniCasDetalji.Update(casDetalji);
			_context.SaveChanges();
			return Redirect("/Cas/Uredi?id="+casDetalji.OdrzaniCasId);
		}

		public IActionResult UcenikJeOdsutan(int id)
		{
			OdrzaniCasDetalji casDetalji = _context.OdrzaniCasDetalji.Where(x => x.Id == id).FirstOrDefault();
			casDetalji.Prisutan = false;
			_context.OdrzaniCasDetalji.Update(casDetalji);
			_context.SaveChanges();
			return Redirect("/Cas/Uredi?id=" + casDetalji.OdrzaniCasId);

		}

		public IActionResult Uredi(int id)
		{
			OdrzaniCasDetaljiUrediVM model = _context.OdrzaniCasDetalji.Where(x => x.Id == id).Select(x => new OdrzaniCasDetaljiUrediVM {
				Student=x.SlusaPredmet.UpisGodine.Student.Ime+" "+ x.SlusaPredmet.UpisGodine.Student.Prezime,
				Bodovi=x.BodoviNaCasu,
				OdrzaniCasDetaljiId=x.Id,
				CasId=x.OdrzaniCasId
			}).FirstOrDefault();

			return PartialView("Uredi", model);

		}

		public IActionResult SnimiPromjene(OdrzaniCasDetaljiUrediVM model)
		{
			if (!ModelState.IsValid)
			{
				return View("/Uredi/Index", model);
			}
			OdrzaniCasDetalji casDetalji = _context.OdrzaniCasDetalji.Where(x => x.Id == model.OdrzaniCasDetaljiId).FirstOrDefault();
			casDetalji.BodoviNaCasu = model.Bodovi;
			_context.OdrzaniCasDetalji.Update(casDetalji);
			_context.SaveChanges();

			return Redirect("/Cas/Uredi?id="+model.CasId);
		}
	}
}