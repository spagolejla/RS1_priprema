using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.EF
{
    public class MojContext: DbContext
    {
        public MojContext(DbContextOptions<MojContext> options): base(options)
        {

        }

       
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<UpisUOdjeljenje> UpisUOdjeljenje { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
		public DbSet<MaturskiIspit> MaturskiIspit { get; set; }
		public DbSet<MaturskiIspitStavka> MaturskiIspitStavka { get; set; }

		protected override void OnModelCreating(ModelBuilder mb) {
			base.OnModelCreating(mb);

			mb.Entity<MaturskiIspit>().HasOne(x=>x.Odjeljenje).WithMany().HasForeignKey(x=>x.OdjeljenjeId).OnDelete(DeleteBehavior.Restrict);
			mb.Entity<MaturskiIspitStavka>().HasOne(x => x.UpisUOdjeljenje).WithMany().HasForeignKey(x => x.UpisUOdjeljenjeId).OnDelete(DeleteBehavior.Restrict);
		}


	}
}
