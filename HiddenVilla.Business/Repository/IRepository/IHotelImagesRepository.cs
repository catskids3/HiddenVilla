using HiddenVilla.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla.Business.Repository.IRepository
{
	public interface IHotelImagesRepository
	{
		public Task<int> CreateHotelRoomImage(HotelRoomImageDTO image);
		public Task<int> DeleteHotelRoomImageByImageId(int imageId);
		public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
		public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
		public Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl);
	}
}
