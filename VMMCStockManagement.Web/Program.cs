using Microsoft.AspNetCore.Identity;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Utils;
using VMMCStockManagement.Infrastructure.DbContexts;
using VMMCStockManagement.Web.Utils;
using VMMCStockManagement.Domain.Extensions;
using VMMCStockManagement.Infrastructure.Extensions;


namespace VMMCStockManagement.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			//builder.Services.AddDbContext<ApplicationDbContext>(options =>
			//	options.UseSqlServer(connectionString));
			//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddRazorPages();
			builder.Services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<VMMCStockManagementDbContext>()
					.AddDefaultTokenProviders();

			builder.Services.AddIdentityCore<User>();
			builder.Services.AddScoped<RoleManager<Role>>();
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			//	.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services
			.AddMvc()
			.AddRazorPagesOptions(options => {
				options.Conventions.AuthorizePage("/identity/account/login");
			});
			builder.Services.AddRazorPages();

			////Framework
			builder.Services.AddDomain();
			builder.Services.AddInfrastructure();
			builder.Services.AddTransient<IWebSecurity, WebSecurity>();
			builder.Services.AddCors();
			builder.Services.AddRouting(options => options.LowercaseUrls = true);
			builder.Services.AddControllers(o =>
			{
				o.AllowEmptyInputInBodyModelBinding = true;
			}).AddNewtonsoftJson(
				 options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
			 );
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = new PathString("/Identity/Account/Login");
				//other properties
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			else
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapRazorPages();
			app.MapControllers();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				   name: "default",
				   pattern: "{controller}/{action}");
				endpoints.MapRazorPages();

				endpoints.MapFallback(context => {
					context.Response.Redirect("/404");
					return Task.CompletedTask;
				});
			});

			app.Run();
		}
	}
}
