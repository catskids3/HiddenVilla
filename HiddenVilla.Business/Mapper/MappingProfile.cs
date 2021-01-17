﻿using AutoMapper;
using HiddenVilla.DataAccess.Data;
using HiddenVilla.Models;

namespace HiddenVilla.Business.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<HotelRoomDTO, HotelRoom>();
			CreateMap<HotelRoom, HotelRoomDTO>();
			CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
			CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
			CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ReverseMap();
		}
	}
}
