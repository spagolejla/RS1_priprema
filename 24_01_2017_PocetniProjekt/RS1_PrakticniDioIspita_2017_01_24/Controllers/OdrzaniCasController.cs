using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class OdrzaniCasController : Controller
    {
		MojContext _context;
		public OdrzaniCasController(MojContext db)
		{
			_context = db;
		}
        public IActionResult Index()
        {
			
            return View();
        }
    }
}