using AdminDashboard.Mapper;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services.Profiles;

namespace AdminDashboard
{
	public class Programs
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
			});

			builder.Services.AddDbContext<StoreIdentityContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySQLConnection"));
			});

			builder.Services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireDigit = true;
			})
			.AddEntityFrameworkStores<StoreIdentityContext>()
			.AddDefaultTokenProviders(); // Adds token providers for password resets, etc.

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.AccessDeniedPath = new PathString("Home/Error");
					options.LoginPath = new PathString("Admin/Login");
				});
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			builder.Services.AddAutoMapper(typeof(ProductVMProfile));
			builder.Services.AddAutoMapper(typeof(ProductProfile));


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			// Add authentication before authorization
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Admin}/{action=Login}/{id?}");

			app.Run();
		}
	}
}

