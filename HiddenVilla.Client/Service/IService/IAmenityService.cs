using HiddenVilla.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service.IService
{
	public interface IAmenityService
	{
		public Task<IEnumerable<HotelAmenityDTO>> GetAmenities();
	}
}
