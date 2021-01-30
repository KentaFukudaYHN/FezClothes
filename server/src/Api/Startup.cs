using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using ApplicationCore.Services;
using Api.Interfaces;
using Api.ViewServices;

namespace Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            //クッキー認証の設定
            services.Configure<CookiePolicyOptions>(o =>
            {
                o.Secure = CookieSecurePolicy.Always;
                o.HttpOnly = HttpOnlyPolicy.Always; //jsから触れなくする
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            //DIコンテナにDbContext登録
            string connectionString = Configuration.GetConnectionString("FezClothesDatabase");
            services.AddDbContext<FezClothesContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            //DIコンテナにEfRepositoryの登録
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //DIコンテナにServiceの登録
            services.AddScoped<ILoginService, LoginService>();

            //DIコンテナにViewServiceの登録
            services.AddScoped<ILoginViewService, LoginViewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
