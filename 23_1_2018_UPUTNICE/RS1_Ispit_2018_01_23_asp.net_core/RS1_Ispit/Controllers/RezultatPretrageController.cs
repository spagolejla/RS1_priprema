using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1.Ispit.Web.Models;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class RezultatPretrageController : Controller
    {
		MojContext _context;
		public RezultatPretrageController(MojContext db)
		{
			_context = db;
		}
		public IActionResult Index(int id)
        {
			RezultatPretrageIndexVM model = new RezultatPretrageIndexVM
			{
				jelzakljucano=_context.Uputnica.Where(x=>x.Id==id).FirstOrDefault().IsGotovNalaz,
				Rows = _context.RezultatPretrage.Include(l=>l.LabPretraga).Where(x=>x.UputnicaId==id).Select(x => new RezultatPretrageIndexVM.Row
				{
					rezultatPretrageId = x.Id,
					pretraga = x.LabPretraga.Naziv,
					izmjerenaVrijednost=x.NumerickaVrijednost.ToString(),
					modalitet=x.Modalitet.Opis,
					vrstaVr=x.LabPretraga.VrstaVr,
					JMJ=x.LabPretraga.MjernaJedinica
				}).ToList()

			};
            return PartialView("Index",model);
        }
		public IActionResult Uredi(int id)
		{
			RezultatPretrageUrediVM model = new RezultatPretrageUrediVM();
			model.pretraga = _context.RezultatPretrage.Include(x=>x.LabPretraga).Where(x => x.Id == id).FirstOrDefault().LabPretraga.Naziv;
			model.vrijednostiPretrage = _context.Modalitet.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Value=x.Id.ToString(),
				Text=x.Opis
			}).ToList();
			model.vrstaVrijednosti = _context.RezultatPretrage.Include(x => x.LabPretraga).Where(x => x.Id == id).FirstOrDefault().LabPretraga.VrstaVr.ToString();
			model.izmjerenaVrijednost = _context.RezultatPretrage.Where(x => x.Id == id).FirstOrDefault().NumerickaVrijednost;
			model.rezultatPretrageId = id;
			return PartialView("Uredi",model);
		}

		public IActionResult Snimi(int Modalitet, double NumerickaVrijednost,int rezultatPretrage)
		{
			RezultatPretrage rez = new RezultatPretrage();
			rez = _context.RezultatPretrage.Include(x=>x.Modalitet).Where(x=>x.Id== rezultatPretrage).FirstOrDefault();
			
			//rez.ModalitetId=Modalitet  --> prouzrokuje null referencess?????

				rez.NumerickaVrijednost = NumerickaVrijednost;
				_context.SaveChanges();
			
		
			
			
			string idd = rez.UputnicaId.ToString();
			return Redirect("/Uputnica/Detalji?id=" + idd);
		}
	}
}