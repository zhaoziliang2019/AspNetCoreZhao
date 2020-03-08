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
        //��ݼ� ctor ���ٴ������캯��   ���ٴ���ʵ��ctrl+.
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
            //var three = configuration["Three"];��ȡĳ��ֵ
           // var three = configuration["Three:BoldDepartMent"]; //��ȡ����ֵ
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��MVC
            services.AddControllersWithViews();
            //Web Api
            // services.AddControllers();
            //ע�����
            services.AddSingleton<IDepartMentService, DepartMentService>();
            //��ȡ�����ļ���Ϣ
            services.Configure<ThreeOptions>(configuration.GetSection(key:"Three"));
            //services.Configure<ThreeOptions>(configuration);��ȡȫ��
            //ע��SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //��̬�ļ�
            app.UseStaticFiles();
            //ssl
            app.UseHttpsRedirection();

            app.UseAuthentication();//�����֤
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                //MVC����
                endpoints.MapControllerRoute(
                    name: default,
                    pattern: "{controller=DepartMent}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
