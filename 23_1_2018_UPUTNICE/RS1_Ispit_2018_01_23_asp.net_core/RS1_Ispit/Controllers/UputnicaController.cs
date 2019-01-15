using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_09_11_DotnetCore.EF;
using RS1_Ispit_asp.net_core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1.Ispit.Web.Models;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class UputnicaController : Controller
    {
		MojContext _context;
		public UputnicaController(MojContext db) {
			_context = db;
		}
        public IActionResult Index()
        {
			UputnicaIndexVM model = new UputnicaIndexVM
			{
				Rows = _context.Uputnica.Select(x => new UputnicaIndexVM.Row
				{
					uputnicaID = x.Id,
					uputio = x.DatumUputnice.ToString("dd.MM.yyyy") + " " + x.UputioLjekar.Ime,
					pacijent = x.Pacijent.Ime,
					vrstaPretrage=x.VrstaPretrage.Naziv,
					datumRezultata=x.DatumRezultata.ToString()

				}).ToList()
			};

            return View("Index",model);
        }

		public IActionResult Dodaj()
		{
			UputnicaDodajVM model = new UputnicaDodajVM();
			model.ljekari = _context.Ljekar.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = "Dr."+x.Ime
			}).ToList();

			model.pacijenti = _context.Pacijent.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Ime
			}).ToList();

			model.pretrage = _context.VrstaPretrage.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv
			}).ToList();

		
			return View("Dodaj", model);
		}

		public IActionResult Snimi(int ljekar, DateTime Datum,int pacijent,int pretraga)
		{
			Uputnica novaUputnica = new Uputnica();
			novaUputnica.UputioLjekarId = ljekar;
			novaUputnica.DatumUputnice = Datum;
			novaUputnica.PacijentId = pacijent;
			novaUputnica.VrstaPretrageId = pretraga;
			novaUputnica.IsGotovNalaz = false;
			_context.Uputnica.Add(novaUputnica);
			_context.SaveChanges();

			

			List<RezultatPretrage> rezultatiPretrage = _context.LabPretraga.Where(x => x.VrstaPretrageId == pretraga).Select(z => new RezultatPretrage
			{
				LabPretragaId = z.Id,
				ModalitetId = null,
				NumerickaVrijednost = null,
				UputnicaId = novaUputnica.Id
			}).ToList();

			_context.RezultatPretrage.AddRange(rezultatiPretrage);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Detalji(int id)
		{
			UputnicaDetaljiVM model = _context.Uputnica.Where(s => s.Id == id).Select(x => new UputnicaDetaljiVM {
				pacijent=x.Pacijent.Ime,
				datumUputnice=x.DatumUputnice.ToString("dd.MM.yyyy"),
				datumRezultata=x.DatumRezultata.ToString(),
				uputnicaId=x.Id,
				jelzakljucano=x.IsGotovNalaz
			}).FirstOrDefault();
			

			return View("Detalji",model);
		}

		public IActionResult Zakljucaj(int id)
		{
			DateTime date = DateTime.Now; //kako trenutno vrijeme
			// zasto kad koristim .Find(id) imam runtime
			Uputnica up = _context.Uputnica.Where(x=>x.Id==id).FirstOrDefault();
			up.DatumRezultata = date;
			up.IsGotovNalaz = true;
			_context.SaveChanges();

			string idd = id.ToString();

			return Redirect("/Uputnica/Detalji?id="+idd);
		}
	}
}