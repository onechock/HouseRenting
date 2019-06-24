using HouseRenting.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseRenting.Services
{

	public class DatabaseServiceFake : IDatabaseService
	{
		private readonly List<HouseTypeModel> _houseTypes;
		private readonly List<ReservationModel> _reservations;
		private readonly BaseDayFeeModel _baseDayFeeModel;

		public DatabaseServiceFake()
		{
			_houseTypes = new List<HouseTypeModel> {
				new HouseTypeModel() { Id = 0, Name = "Apartment", MultiplyPriceBy = 1 },
				new HouseTypeModel() { Id = 1, Name = "Bungalow", MultiplyPriceBy = 1.5M },
			};

			_reservations = new List<ReservationModel> {
				new ReservationModel() { Id = 0, HouseType = 0, Email = "test@fake.com", Price = 80  },
				new ReservationModel() { Id = 1, HouseType = 1, Email = "test@fake.com", Price = 120 },
			};


			_baseDayFeeModel = new BaseDayFeeModel { Id = 1, Price = 70 };
		}

		public HouseTypeModel AddHouseType(HouseTypeModel houseType)
		{
			int id = _houseTypes.Max(x => x.Id);

			houseType.Id = ++id;

			_houseTypes.Add(houseType);

			return houseType;
		}

		public HouseTypeModel GetHouseType(int id)
		{
			var houseType = _houseTypes.FirstOrDefault(x => x.Id == id);

			if (houseType == null)
				return null;

			houseType.SetPrice(_baseDayFeeModel.Price);

			return houseType;
		}

		public IEnumerable<HouseTypeModel> GetHouseTypes()
		{
			return _houseTypes;
		}

		public ReservationModel AddReservation(ReservationModel reservation)
		{
			var houseType = GetHouseType(reservation.HouseType);

			if (houseType == null)
				return null;

			int id = _reservations.Max(x => x.Id);
			reservation.Id = ++id;
			reservation.SetPrice(houseType.Price);

			_reservations.Add(reservation);

			return reservation;
		}

		public ReservationModel GetReservation(int id)
		{
			return _reservations.FirstOrDefault(x => x.Id == id);
		}

		public BaseDayFeeModel AddUpdateBaseDayFee(BaseDayFeeModel baseDayFee)
		{
			throw new NotImplementedException();
		}

		public BaseDayFeeModel GetBaseDayFee(int id)
		{
			throw new NotImplementedException();
		}
	}
}
