using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HouseRenting.Models
{
	public class HouseTypeModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public decimal MultiplyPriceBy { get; set; }

		[BsonIgnore]
		public decimal Price { get; private set; }

		public void SetPrice(decimal baseDayFee) {
			Price = baseDayFee * MultiplyPriceBy;
		}
	}
}
