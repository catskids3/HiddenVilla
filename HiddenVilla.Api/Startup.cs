using AutoMapper;
using HiddenVilla.Api.Helper;
using HiddenVilla.Business.Repository;
using HiddenVilla.Business.Repository.IRepository;
using HiddenVilla.DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace HiddenVilla.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			var appSettingsSection = Configuration.GetSection("APISettings");
			services.Configure<APISettings>(appSettingsSection);

			var apiSettings = appSettingsSection.Get<APISettings>();
			var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidAudience = apiSettings.ValidAudience,
						ValidIssuer = apiSettings.ValidIssuer,
						ClockSkew = TimeSpan.Zero

					};
				});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
			services.AddScoped<IHotelImagesRepository, HotelImagesRepository>();
			services.AddScoped<IAmenityRepository, AmenityRepository>();
			services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();

			services.AddCors(o => o.AddPolicy("HiddenVilla", builder =>
			{
				builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
			}));

			services.AddRouting(option => option.LowercaseUrls = true);
			services.AddControllers()
				.AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
				.AddNewtonsoftJson(opt =>
				{
					opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
					opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "HiddenVilla.Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please Bearer and then token in the field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
				   {
					 new OpenApiSecurityScheme
					 {
					   Reference = new OpenApiReference
					   {
						 Type = ReferenceType.SecurityScheme,
						 Id = "Bearer"
					   }
					  },
					  new string[] { }
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HiddenVilla.Api v1"));
			}

			app.UseHttpsRedirection();
			app.UseCors("HiddenVilla");
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
