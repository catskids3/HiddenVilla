using AutoMapper;
using HiddenVilla.Business.Repository;
using HiddenVilla.Business.Repository.IRepository;
using HiddenVilla.DataAccess.Data;
using HiddenVilla.Server.Data;
using HiddenVilla.Server.Service;
using HiddenVilla.Server.Service.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HiddenVilla.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
			services.AddScoped<IHotelImagesRepository, HotelImagesRepository>();
			services.AddScoped<IAmenityRepository, AmenityRepository>();
			services.AddScoped<IFileUpload, FileUpload>();
			services.AddRazorPages();
			services.AddHttpContextAccessor();
			services.AddServerSideBlazor();
			services.AddSingleton<WeatherForecastService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
