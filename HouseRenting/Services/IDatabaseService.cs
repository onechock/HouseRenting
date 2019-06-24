using HouseRenting.Models;
using System.Collections.Generic;

namespace HouseRenting.Services
{
	public interface IDatabaseService
	{
		IEnumerable<HouseTypeModel> GetHouseTypes();
		HouseTypeModel GetHouseType(int id);
		HouseTypeModel AddHouseType(HouseTypeModel houseType);

		ReservationModel AddReservation(ReservationModel reservation);
		ReservationModel GetReservation(int id);

		BaseDayFeeModel AddUpdateBaseDayFee(BaseDayFeeModel baseDayPrice);
		BaseDayFeeModel GetBaseDayFee(int id);
	}
}
