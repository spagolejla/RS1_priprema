using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;
namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class MaturskiIspitStavkeController : Controller
    {
		MojContext _context;
		public MaturskiIspitStavkeController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index(int id)
        {
			MaturskiIspitStavkeIndexVM model = new MaturskiIspitStavkeIndexVM {
				MaturskiIspitId=id,
				Rows=_context.MaturskiIspitStavka.Where(x=>x.MaturskiIspitId==id).Select(x=>new MaturskiIspitStavkeIndexVM.Row {
					Ucenik=x.UpisUOdjeljenje.Ucenik.ImePrezime,
					Bodovi=x.Bodovi,
					OpciUspjeh=x.UpisUOdjeljenje.OpciUspjeh,
					Oslobodjen=x.Oslobodjen,
					MaturskiIspitStavkaId=x.Id

				}).ToList()
			};

            return PartialView("Index",model);
        }

		public IActionResult Uredi(int id)
		{
			MaturskiIspitStavkeUrediVM model = _context.MaturskiIspitStavka.Where(x => x.Id == id).Select(x => new MaturskiIspitStavkeUrediVM
			{
				Ucenik=x.UpisUOdjeljenje.Ucenik.ImePrezime,
				Bodovi=x.Bodovi,
				MaturskiIspitStavkeId=id,
				MaturskiIspitId=x.MaturskiIspitId
			}).FirstOrDefault();

			return PartialView("Uredi", model);
		}

		public IActionResult SnimiPromjene(MaturskiIspitStavkeUrediVM model)
		{
			MaturskiIspitStavka stavka = _context.MaturskiIspitStavka.Where(x => x.Id == model.MaturskiIspitStavkeId).FirstOrDefault();
			stavka.Bodovi = model.Bodovi;
			_context.MaturskiIspitStavka.Update(stavka);
			_context.SaveChanges();

			return Redirect("/MaturskiIspit/Detalji?id="+model.MaturskiIspitId);
		}
	}
}