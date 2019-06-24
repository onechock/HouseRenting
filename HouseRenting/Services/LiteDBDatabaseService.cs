using HouseRenting.Models;
using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRenting.Services
{

	public class LiteDBDatabaseService : IDatabaseService
	{
		private readonly LiteDatabase _db;
		private decimal? currentBaseDayFee;

		public LiteDBDatabaseService()
		{
			_db = new LiteDatabase(@"HouseRenting.db");
			currentBaseDayFee = GetBaseDayFee(1)?.Price;
		}

		public HouseTypeModel AddHouseType(HouseTypeModel houseType)
		{
			var houseTypeTable = _db.GetCollection<HouseTypeModel>("HouseType");

			var result = houseTypeTable.Insert(houseType);

			if (!result.IsInt32)
				return null;

			houseType.Id = result;

			return houseTypeTable.FindOne(x => x.Id == result);
		}

		public HouseTypeModel GetHouseType(int id)
		{
			var houseTypeTable = _db.GetCollection<HouseTypeModel>("HouseType");

			var houseType = houseTypeTable.FindOne(x => x.Id == id);

			if (houseType == null)
				return houseType;

			if(currentBaseDayFee != null)
				houseType.SetPrice((decimal)currentBaseDayFee);

			return houseType;
		}

		public IEnumerable<HouseTypeModel> GetHouseTypes()
		{
			var houseTypeTable = _db.GetCollection<HouseTypeModel>("HouseType");

			var houseTypes = houseTypeTable.FindAll().ToList();

			foreach (var houseType in houseTypes)
			{
				if (currentBaseDayFee != null)
					houseType.SetPrice((decimal)currentBaseDayFee);
			}

			return houseTypes;
		}

		public ReservationModel AddReservation(ReservationModel reservation)
		{
			var houseType = GetHouseType(reservation.HouseType);

			if (houseType == null)
				return null;

			var reservationTable = _db.GetCollection<ReservationModel>("Reservation");

			reservation.SetPrice(houseType.Price);

			var result = reservationTable.Insert(reservation);

			if (!result.IsInt32)
				return null;

			reservation.Id = result;

			return reservationTable.FindOne(x => x.Id == result);
		}

		public ReservationModel GetReservation(int id)
		{
			var reservationTable = _db.GetCollection<ReservationModel>("Reservation");

			return reservationTable.FindOne(x => x.Id == id);
		}

		public BaseDayFeeModel AddUpdateBaseDayFee(BaseDayFeeModel baseDayFee)
		{
			var baseDayFeeTable = _db.GetCollection<BaseDayFeeModel>("BaseDayFee");

			var hasbaseDayFee = baseDayFeeTable.FindOne(x => x.Id == baseDayFee.Id) != null;

			if (!hasbaseDayFee)
			{
				var result = baseDayFeeTable.Insert(baseDayFee);
				baseDayFee.Id = result;
				currentBaseDayFee = baseDayFee.Price;
			}
			else {
				baseDayFeeTable.Update(baseDayFee);
				currentBaseDayFee = baseDayFee.Price;
			}

			return baseDayFee;
		}

		public BaseDayFeeModel GetBaseDayFee(int id)
		{
			var basePriceTable = _db.GetCollection<BaseDayFeeModel>("BaseDayFee");

			return basePriceTable.FindOne(x => x.Id == id);
		}
	}
}
