using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolidTrivia.Common;
using SolidTrivia.Questions.Web.Data;
using SolidTrivia.Tests; // MOCKS FOR DEVELOPMENT!

namespace SolidTrivia.Questions.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();


            //game
            services.AddSingleton<IQuestionFacade, QuestionFacade>();
            services.AddSingleton<IQuestionStore, QuestionStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<IBoardStore, BoardStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ICommentStore, CommentStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ITagStore, TagStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ICategoryStore, CategoryStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<IVoteStore, VoteStoreMock>(); //MOCKS FOR DEVELOPMENT



            //ui
            services.AddScoped<ICreateQuestionViewModel, CreateQuestionViewModel>();
            services.AddScoped<ITagsViewModel, TagsViewModel>();
            services.AddScoped<ITagCreateViewModel, TagCreateViewModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
