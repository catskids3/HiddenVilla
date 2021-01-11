using HiddenVilla.Client.Service.IService;
using HiddenVilla.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service
{
	public class AmenityService : IAmenityService
	{
		private readonly HttpClient _client;

		public AmenityService(HttpClient client)
		{
			_client = client;
		}

		public async Task<IEnumerable<HotelAmenityDTO>> GetAmenities()
		{
			var response = await _client.GetAsync($"api/hotelAmenity");
			var content = await response.Content.ReadAsStringAsync();
			var amenities = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
			return amenities;
		}
	}
}
