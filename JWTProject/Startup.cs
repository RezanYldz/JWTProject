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
                opt.RequireHttpsMetadata = false; //Proje oluþtururken HTTPS kapattýðýmýz için burdan da kapatýyoruz.
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost", //Bu tokený kim oluþurdu?
                    ValidAudience = "http://localhost", // Bu tokený kim kullanacak?
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rezanrezanrezan1")), // signature yani imzamýz
                    ValidateIssuerSigningKey = true, //signkey i aktifleþtiriyoruz
                    ValidateLifetime = true, //Bir token geldiðinde geçerlilik süresini kontrol et, süresi bitmiþse invalid (geçersiz) kabul et
                    ClockSkew = TimeSpan.Zero //Server saatleri farklý olabileceði için token geçerlilik süresine ek olarak süre ilave edebiliyoruz, biz zero diyerek ek süre tanýmadýk
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
