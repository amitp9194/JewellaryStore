using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewellaryStore.Models
{
	public class CalculateModel
	{
		public float GoldPerGram { get; set; }
		public float Weight { get; set; }
		public int Discount { get; set; } = 2;

	}
}
