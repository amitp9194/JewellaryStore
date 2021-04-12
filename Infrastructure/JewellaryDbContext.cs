using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewellaryStore.Models
{
	public class JewellaryDbContext : IdentityDbContext<IdentityUser>
	{
		public JewellaryDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
