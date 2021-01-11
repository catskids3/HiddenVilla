using HiddenVilla.Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HiddenVilla.Api.Controllers
{
	[Route("api/[controller]")]
	public class HotelAmenityController : Controller
	{
		private readonly IAmenityRepository _amenityRepository;

		public HotelAmenityController(IAmenityRepository amenityRepository)
		{
			_amenityRepository = amenityRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetHotelAmenities()
		{
			var allAmenities = await _amenityRepository.GetAllHotelAmenity();
			return Ok(allAmenities);
		}
	}
}
