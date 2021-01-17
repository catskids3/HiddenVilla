using HiddenVilla.Client.Service.IService;
using HiddenVilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service
{
	public class RoomOrderDetailsService : IRoomOrderDetailsService
	{
		private readonly HttpClient _client;

		public RoomOrderDetailsService(HttpClient client)
		{
			_client = client;
		}
		public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
		{
			throw new NotImplementedException();
		}

		public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
		{
			throw new NotImplementedException();
		}
	}
}
