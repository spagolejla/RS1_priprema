using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1.Ispit.Web.Models;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class UputnicaController : Controller
    {
		MojContext _context;
		public UputnicaController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index()
        {
			UputnicaIndexVM model = new UputnicaIndexVM {
				Rows = _context.Uputnica.Select(x => new UputnicaIndexVM.Row {
					Uputio=x.DatumUputnice.ToString("dd.MM.yyyy")+" | Dr."+x.UputioLjekar.Ime,
					Pacijent=x.Pacijent.Ime,
					DatumEvidentiranja=x.DatumRezultata,
					UputnicaId=x.Id,
					VrstaPretrage=x.VrstaPretrage.Naziv
				}).ToList()

			};

			
            return View("Index",model);
        }
		public IActionResult Dodaj()
		{
			UputnicaDodajVM model = new UputnicaDodajVM();
			model.ljekari = _context.Ljekar.Select(x => new SelectListItem
			{
				Value=x.Id.ToString(),
				Text="Dr."+x.Ime
			}).ToList();
			model.pacijenti = _context.Pacijent.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Ime
			}).ToList();
			model.vrstePretrage = _context.VrstaPretrage.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv
			}).ToList();
			
			
			return View("Dodaj", model);
		}

		public IActionResult Snimi(UputnicaDodajVM model)
		{
			if (!ModelState.IsValid)
			{
				model.ljekari = _context.Ljekar.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = "Dr." + x.Ime
				}).ToList();
				model.pacijenti = _context.Pacijent.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Ime
				}).ToList();
				model.vrstePretrage = _context.VrstaPretrage.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Naziv
				}).ToList();

				return View("Dodaj", model);
			}

			Uputnica novaUputnica = new Uputnica()
			{
				PacijentId = model.pacijentId,
				UputioLjekarId = model.ljekarId,
				VrstaPretrageId = model.vrstaPretrageId,
				DatumUputnice = (DateTime)model.DatumUputnie
			};

			_context.Uputnica.Add(novaUputnica);
			_context.SaveChanges();
			List<RezultatPretrage> listaPretraga = _context.LabPretraga.Where(x => x.VrstaPretrageId == model.vrstaPretrageId).Select(x => new RezultatPretrage
			{
				LabPretragaId=x.Id,
				UputnicaId=novaUputnica.Id,
				ModalitetId=null,
				NumerickaVrijednost=null,

			}).ToList();

			_context.AddRange(listaPretraga);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Detalji(int id)
		{
			UputnicaDetaljiVM model = _context.Uputnica.Where(x => x.Id == id).Select(x => new UputnicaDetaljiVM
			{
				DatumUputnice = x.DatumUputnice,
				DatumRezultata = x.DatumRezultata,
				Pacijent =x.Pacijent.Ime,
				UputnicaId=x.Id
			}).FirstOrDefault();
			return View("Detalji",model);
		}

		public IActionResult Zakljucaj(int id)
		{
			DateTime date = DateTime.Now; //kako trenutno vrijeme
										  // zasto kad koristim .Find(id) imam runtime
			Uputnica up = _context.Uputnica.Where(x => x.Id == id).FirstOrDefault();
			up.DatumRezultata = date;
			up.IsGotovNalaz = true;
			_context.Uputnica.Update(up);
			_context.SaveChanges();

			string idd = id.ToString();

			return Redirect("/Uputnica/Detalji?id=" + idd);
		}
	}
}