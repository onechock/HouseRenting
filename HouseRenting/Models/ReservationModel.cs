using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HouseRenting.Models
{
	public class ReservationModel
	{
		public int Id { get; set; }

		public int HouseType { get; set; }
		public string HouseTypeName { get; set; }
		public string Email { get; set; }
		public decimal Price { get; set; }
		public DateTime Date { get; set; }

		public void SetPrice(decimal price) {
			Price = price;
		}
	}
}
