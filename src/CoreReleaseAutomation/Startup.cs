using AutoMapper;
using CoreRelease.Repositories;
using CoreReleaseAutomation.Data;
using CoreReleaseAutomation.Helpers;
using CoreReleaseAutomation.Interfaces;
using CoreReleaseAutomation.Mapper;
using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.Repositories;
using CoreReleaseAutomation.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreReleaseAutomation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }     

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options => options.AutomaticAuthentication = true);
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

            services.AddDbContext<ApplicationDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);            
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));            
            services.AddScoped<ILogVersionRepository, LogVersionRepository>();
            services.AddScoped<IReleaseRepository, ReleaseRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ISetup, Setup>();            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");                
                app.UseHsts();
            }            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
