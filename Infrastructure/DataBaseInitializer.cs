using JewellaryStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewellaryStore.Infrastructure
{
	public static class DataBaseInitializer
	{
		public static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.RoleExistsAsync("Privileged").Result)
			{
				var role = new IdentityRole()
				{
					Name = Role.Privileged
				};
				roleManager.CreateAsync(role);

				IdentityUser privilegedUser = new IdentityUser
				{
					UserName = "privilegeduser@gmail.com",
					Email = "privilegeduser@gmail.com",
					EmailConfirmed = true
				};
				IdentityResult result1 = userManager.CreateAsync(privilegedUser, "Password@123").Result;

				if (result1.Succeeded)
				{
					userManager.AddToRoleAsync(privilegedUser, Role.Privileged).Wait();
				}
			}

			if (!roleManager.RoleExistsAsync("Normal").Result)
			{
				var role = new IdentityRole()
				{
					Name = Role.Normal
					
				};
				roleManager.CreateAsync(role);

				IdentityUser normalUser = new IdentityUser
				{
					UserName = "normaluser@gmail.com",
					Email = "normaluser@gmail.com",
					EmailConfirmed = true
				};
				IdentityResult result1 = userManager.CreateAsync(normalUser, "Password@123").Result;
				if (result1.Succeeded)
				{
					userManager.AddToRoleAsync(normalUser, Role.Normal).Wait();
				}
			}
		}
	}
}
