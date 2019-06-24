using HouseRenting.Controllers;
using HouseRenting.Models;
using HouseRenting.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace HouseRentingUnitTest
{
	public class HouseRentalControllerTest
	{
		HouseRentalController _controller;
		IDatabaseService _service;
		
		public HouseRentalControllerTest()
		{
			_service = new DatabaseServiceFake();
			_controller = new HouseRentalController(_service);
		}

		#region HouseType Tests

		[Fact]
		public void HouseType_GetAll_ReturnsAllItems()
		{
			var okResult = _controller.GetHouseTypes().Result as OkObjectResult;

			var items = Assert.IsType<List<HouseTypeModel>>(okResult.Value);
			Assert.Equal(2, items.Count);
		}

		[Fact]
		public void HouseType_AddOne_ReturnsAddedHouseType()
		{
			var houseType = new HouseTypeModel()
			{
				Name = "Small house",
				MultiplyPriceBy = 2.5M
			};

			var okResult = _controller.AddHouseType(houseType).Result as OkObjectResult;

			var item = Assert.IsType<HouseTypeModel>(okResult.Value);

			Assert.Equal(2, item.Id);
			Assert.Equal("Small house", item.Name);
			Assert.Equal(2.5M, item.MultiplyPriceBy);
		}

		[Fact]
		public void HouseType_GetOne_ReturnsOkResult()
		{
			var okResult = _controller.GetHouseType(1);
			
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public void HouseType_GetOne_RightPrice()
		{
			var okResult = _controller.GetHouseType(1).Result as OkObjectResult;

			var item = Assert.IsType<HouseTypeModel>(okResult.Value);
			
			Assert.Equal(60 * item.MultiplyPriceBy, item.Price);
		}

		[Fact]
		public void HouseType_GetOne_ReturnsNotFoundResult()
		{
			var notFoundResult = _controller.GetHouseType(5);
			
			Assert.IsType<NotFoundResult>(notFoundResult.Result);
		}

		#endregion


		#region Reservation Tests

		[Fact]
		public void Reservation_AddOne_ReturnsAddedReservation()
		{
			var reservation = new ReservationModel()
			{
				HouseType = 1,
				Email = "test@fake.com",
				Date = new DateTime(2020, 5, 10)
			};

			var okResult = _controller.AddReservation(reservation).Result as OkObjectResult;

			var item = Assert.IsType<ReservationModel>(okResult.Value);

			var isValidEmail = Regex.IsMatch(item.Email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

			Assert.Equal(2, item.Id);
			Assert.Equal("test@fake.com", item.Email);
			Assert.True(isValidEmail);
			Assert.Equal(reservation.Date, item.Date);
		}

		[Fact]
		public void Reservation_GetAll_ReturnsNotFoundResult()
		{
			var notFoundResult = _controller.GetReservation(5);

			Assert.IsType<NotFoundResult>(notFoundResult.Result);
		}

		[Fact]
		public void Reservation_GetOne_ReturnsOkResult()
		{
			var okResult = _controller.GetReservation(1);

			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		#endregion
	}
}
