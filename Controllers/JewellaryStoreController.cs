using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewellaryStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JewellaryStore.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class JewellaryStoreController : ControllerBase
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		public JewellaryStoreController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}
		
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(LoginModel loginModel)
		{
			if (ModelState.IsValid) {
				var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);
				if (result.Succeeded)
				{
					var user = await _userManager.FindByEmailAsync(loginModel.UserName);
					var roles = await _userManager.GetRolesAsync(user);

					return Ok(new { user = user.UserName, role = roles });
				}
			}
			return BadRequest("Please try again.");
		}

		[Authorize(Roles = Role.Privileged)]
		[HttpPost("calculateforprivileged")]
		public async Task<IActionResult> CalculateWithDAsync(CalculateModel calculateModel)
		{
			if (ModelState.IsValid)
			{
				var result = calculateModel.GoldPerGram * calculateModel.Weight * 1000 * (calculateModel.Discount / 100);
				return Ok(result);
			}
			return BadRequest("Please try again.");
		}

		[Authorize(Roles = Role.Normal)]
		[HttpPost("calculatefornormal")]
		public async Task<IActionResult> CalculateAsync(CalculateModel calculateModel)
		{
			if (ModelState.IsValid)
			{
				if (ModelState.IsValid)
				{
					var result = calculateModel.GoldPerGram * calculateModel.Weight * 1000;
					return Ok(result);
				}
				return BadRequest("Please try again.");
			}
			return BadRequest("Please try again.");
		}

		[AllowAnonymous]
		[HttpGet("PrintToFile")]
		public async Task<FileStreamResult> PrintToFile(float totalAmount)
		{

			var byteArray = Encoding.ASCII.GetBytes(totalAmount.ToString());
			var stream = new MemoryStream(byteArray);

			return File(stream, "text/plain", "totalAmount.txt");
		}

		[AllowAnonymous]
		[HttpGet("PrintToPaper")]
		public async Task<FileStreamResult> PrintToPaper(float totalAmount)
		{
			throw new NotImplementedException();
		}
	}
}
