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
    public class RezultatiPretrageController : Controller
    {
		MojContext _context;
		public RezultatiPretrageController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index(int id)
        {
			RezultatiPretrageIndexVM model = new RezultatiPretrageIndexVM {
				Rows = _context.RezultatPretrage.Where(x => x.UputnicaId == id).Select(x => new RezultatiPretrageIndexVM.Row {
					Pretraga = x.LabPretraga.Naziv,
					JMJ = x.LabPretraga.MjernaJedinica,
					NumerickaVrijednost = x.NumerickaVrijednost,
					modalitet = x.Modalitet.Opis,
					RezultatPretrageId = x.Id,


				}).ToList(),
				jelZakljucano = _context.Uputnica.Where(x => x.Id == id).FirstOrDefault().IsGotovNalaz

			};
            return PartialView("Index",model);
        }

		public IActionResult Uredi(int id)
		{
			RezultatiPretrageUrediVM model = _context.RezultatPretrage.Where(x => x.Id == id).Select(x => new RezultatiPretrageUrediVM
			{
				Modaliteti=_context.Modalitet.Where(s=>s.LabPretragaId==x.LabPretragaId).Select(s=>new SelectListItem {
					Value=s.Id.ToString(),
					Text=s.Opis

				}).ToList(),
				Pretraga=x.LabPretraga.Naziv,
				IzmjerenaVrijednost=x.NumerickaVrijednost,
				RezultatPretrageId=x.Id,
				ModalitetId=x.ModalitetId,
				VrstaVr=(int)x.LabPretraga.VrstaVr,
				UputnicaId=x.UputnicaId
			}).FirstOrDefault();
			return PartialView("Uredi", model);
		}

		public IActionResult Snimi(RezultatiPretrageUrediVM model)
		{
		

			RezultatPretrage rez = _context.RezultatPretrage.Where(x => x.Id == model.RezultatPretrageId).FirstOrDefault();

			if (model.VrstaVr == 0) {
				rez.NumerickaVrijednost = model.IzmjerenaVrijednost;
			}
			if (model.VrstaVr == 1)
			{
				rez.ModalitetId = model.ModalitetId;
			}
			_context.RezultatPretrage.Update(rez);
			_context.SaveChanges();
			return Redirect("/Uputnica/Detalji?id="+model.UputnicaId);
		}
	}
}