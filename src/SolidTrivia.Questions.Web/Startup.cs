using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolidTrivia.Common;
using SolidTrivia.Questions.Storage;
using SolidTrivia.Questions.Web.Areas.Identity;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true
            )
            .AddUserStore<TrifleUserStore>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<WeatherForecastService>();


            //game
            services.AddSingleton<IQuestionFacade, QuestionFacade>();
            services.AddSingleton<IQuestionStore, QuestionStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<IBoardStore, BoardStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ICommentStore, CommentStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ITagStore, TagStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<ICategoryStore, CategoryStoreMock>(); //MOCKS FOR DEVELOPMENT
            services.AddSingleton<IVoteStore, VoteStoreMock>(); //MOCKS FOR DEVELOPMENT

            //ui tags
            services.AddScoped<TagCreateViewModel>();
            services.AddScoped<TagDeleteViewModel>();
            services.AddScoped<TagEditViewModel>();
            services.AddScoped<TagListViewModel>();

            //ui board
            services.AddScoped<BoardCreateViewModel>();
            services.AddScoped<BoardDeleteViewModel>();
            services.AddScoped<BoardEditViewModel>();
            services.AddScoped<BoardListViewModel>();
            services.AddScoped<BoardAddCategoryViewModel>();

            //ui category
            services.AddScoped<CategoryCreateViewModel>();
            services.AddScoped<CategoryDeleteViewModel>();
            services.AddScoped<CategoryEditViewModel>();
            services.AddScoped<CategoryListViewModel>();

            //ui question
            services.AddScoped<QuestionCreateViewModel>();
            services.AddScoped<QuestionDeleteViewModel>();
            services.AddScoped<QuestionEditViewModel>();
            services.AddScoped<QuestionListViewModel>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }

    public class MarkupStringSanitizedComponent : ComponentBase
    {
        private string sanitizedString;
        [Parameter]
        public string Text { get; set; }
        protected override void OnParametersSet()
        {
            sanitizedString = Sanitize(Text);
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!string.IsNullOrEmpty(sanitizedString))
                builder.AddMarkupContent(0, sanitizedString);
        }

        private static string Sanitize(string value)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }
    }

    public static class MarkupStringExtensions
    {
        public static MarkupString Sanitize(this MarkupString markupString)
        {
            return new MarkupString(SanitizeInput(markupString.Value));
        }

        private static string SanitizeInput(string value)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }
    }

    public struct MarkupStringSanitized
    {
        public MarkupStringSanitized(string value)
        {
            Value = Sanitize(value);
        }

        public string Value { get; }

        public static explicit operator MarkupStringSanitized(string value) => new MarkupStringSanitized(value);

        public override string ToString() => Value ?? string.Empty;

        private static string Sanitize(string value)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }
    }
}
