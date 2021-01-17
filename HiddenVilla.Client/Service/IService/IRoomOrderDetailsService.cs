﻿using HiddenVilla.Models;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service.IService
{
	public interface IRoomOrderDetailsService
	{
		public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details);
		public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details);
	}
}
