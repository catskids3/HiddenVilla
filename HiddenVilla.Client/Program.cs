using Blazored.LocalStorage;
using HiddenVilla.Client.Service;
using HiddenVilla.Client.Service.IService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
			builder.Services.AddScoped<IAmenityService, AmenityService>();

			await builder.Build().RunAsync();
		}
	}
}
