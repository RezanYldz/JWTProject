using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JWTProject
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=> {
                opt.RequireHttpsMetadata = false; //Proje olu�tururken HTTPS kapatt���m�z i�in burdan da kapat�yoruz.
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost", //Bu token� kim olu�urdu?
                    ValidAudience = "http://localhost", // Bu token� kim kullanacak?
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rezanrezanrezan1")), // signature yani imzam�z
                    ValidateIssuerSigningKey = true, //signkey i aktifle�tiriyoruz
                    ValidateLifetime = true, //Bir token geldi�inde ge�erlilik s�resini kontrol et, s�resi bitmi�se invalid (ge�ersiz) kabul et
                    ClockSkew = TimeSpan.Zero //Server saatleri farkl� olabilece�i i�in token ge�erlilik s�resine ek olarak s�re ilave edebiliyoruz, biz zero diyerek ek s�re tan�mad�k
                };
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
