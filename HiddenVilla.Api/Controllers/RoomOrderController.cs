﻿using HiddenVilla.Business.Repository.IRepository;
using HiddenVilla.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RoomOrderController : Controller
	{
		private readonly IRoomOrderDetailsRepository _repository;
		private readonly IEmailSender _emailSender;

		public RoomOrderController(IRoomOrderDetailsRepository repository,
			IEmailSender emailSender)
		{
			_repository = repository;
			_emailSender = emailSender;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
		{
			if(ModelState.IsValid)
			{
				var result = await _repository.Create(details);
				return Ok(result);
			}
			else
			{
				return BadRequest(new ErrorModel()
				{
					ErrorMessage = "Error while creating Room Details/Booking"
				});
			}
		}

		[HttpPost]
		public async Task<IActionResult> PaymentSuccessful([FromBody] RoomOrderDetailsDTO details)
		{
			var service = new SessionService();
			var sessionDetails = service.Get(details.StripeSessionId);
			if(sessionDetails.PaymentStatus == "paid")
			{
				var result = await _repository.MarkPaymentSuccessful(details.Id);
				if(result == null)
				{
					return BadRequest(new ErrorModel()
					{
						ErrorMessage = "Can not mark payment as successful"
					});
				}

				await _emailSender.SendEmailAsync(details.Email, "Booking Confirmed - Hidden Villa",
					"Your booking has been confirmed at Hidden Villa with order ID :" + details.Id);

				return Ok(result);
			}
			else
			{
				return BadRequest(new ErrorModel()
				{
					ErrorMessage = "Can not mark payment as successful"
				});
			}
		}
	}
}
