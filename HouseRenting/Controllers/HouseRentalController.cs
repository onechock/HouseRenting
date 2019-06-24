using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseRenting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using HouseRenting.Services;

namespace HouseRenting.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HouseRentalController : ControllerBase
	{
		private readonly IDatabaseService _dbService;

		public HouseRentalController(IDatabaseService databaseService)
		{
			_dbService = databaseService;
		}

		#region HouseType Methods
		// GET api/gethousetypes
		[HttpGet("[action]")]
		public ActionResult<IEnumerable<HouseTypeModel>> GetHouseTypes()
		{
			return Ok(_dbService.GetHouseTypes());
		}

		[HttpGet("[action]/{id:int}")]
		public ActionResult<HouseTypeModel> GetHouseType(int id)
		{
			var houseType = _dbService.GetHouseType(id);

			if (houseType == null)
				return NotFound();

			return Ok(houseType);
		}


		// POST api/values
		[HttpPost("[action]")]
		public ActionResult<HouseTypeModel> AddHouseType([FromBody] HouseTypeModel model)
		{
			var newHouseType = _dbService.AddHouseType(model);
			
			return Ok(newHouseType);
		}
		#endregion

		#region Registration Methods

		[HttpGet("[action]/{id:int}")]
		public ActionResult<ReservationModel> GetReservation(int id)
		{
			var reservation = _dbService.GetReservation(id);

			if(reservation == null)
				return NotFound();

			return Ok(reservation);
		}

		[HttpPost("[action]")]
		public ActionResult<ReservationModel> AddReservation([FromBody] ReservationModel model)
		{
			var newReservation = _dbService.AddReservation(model);

			return Ok(newReservation);
		}

		#endregion

		#region BaseDayFee Methods

		[HttpPost("[action]")]
		public ActionResult<BaseDayFeeModel> AddUpdateBaseDayFee([FromBody] BaseDayFeeModel model)
		{
			var baseDayFee = _dbService.AddUpdateBaseDayFee(model);

			return Ok(baseDayFee);
		}

		[HttpGet("[action]")]
		public ActionResult<BaseDayFeeModel> GetBaseDayFee()
		{
			var baseDayFee = _dbService.GetBaseDayFee(1);
			
			return Ok(baseDayFee);
		}

		#endregion
	}
}
