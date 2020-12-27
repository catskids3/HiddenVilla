using AutoMapper;
using HiddenVilla.DataAccess.Data;
using HiddenVilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.Business.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<HotelRoomDTO, HotelRoom>();
			CreateMap<HotelRoom, HotelRoomDTO>();
		}
	}
}
