using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreZhao.Models;
using AspNetCoreZhao.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreZhao
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        //快捷键 ctor 快速创建构造函数   快速创建实例ctrl+.
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
            //var three = configuration["Three"];获取某个值
           // var three = configuration["Three:BoldDepartMent"]; //获取对象值
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注册MVC
            services.AddControllersWithViews();
            //Web Api
            // services.AddControllers();
            //注册服务
            services.AddSingleton<IDepartMentService, DepartMentService>();
            //获取配置文件信息
            services.Configure<ThreeOptions>(configuration.GetSection(key:"Three"));
            //services.Configure<ThreeOptions>(configuration);获取全部
            //注册SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //静态文件
            app.UseStaticFiles();
            //ssl
            app.UseHttpsRedirection();

            app.UseAuthentication();//身份认证
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                //MVC配置
                endpoints.MapControllerRoute(
                    name: default,
                    pattern: "{controller=DepartMent}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
