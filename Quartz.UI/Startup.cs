using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Tasks;
using Vulild.Service;
using Vulild.Service.Quartz;
using Vulild.Service.Task;

namespace Quartz.UI
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ServiceUtil.SearchAssmbly();
            ServiceUtil.InitService("taskservice", new QuartOption()
            {
                ConnectionString = "Server=vulild.top;Database=Quartz;Uid=vulild;Pwd=gelz1122"
            });

            var service = ServiceUtil.GetService<ITaskService>();
            service.AddTask(new QuartzTask
            {
                GroupName = "TaskGroup",
                JobName = "TaskJob",
                TaskType = typeof(DemoTask),
                Cron = "0/5 * * * * ?"
            });
            service.StartTask();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
