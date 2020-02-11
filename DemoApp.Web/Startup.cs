using APIHubConnector.Services;
using APIHUbConnector.Core.Clients;
using DemoApp.Web.APIKeyModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoApp.Web
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
            //1- Supply API Access Tokens ( Try User Secrets )
            services.Configure<AuthRepoHubConnectorOptions>(Configuration);
            services.Configure<AuthHostingConnectorOptions>(Configuration);
            //2- Register APIHubConnector
            APIHubConnectorServiceConfiguration.ConfigureAPIConnector(services);
            //3- Add http client to ApiHubConnector HubClients with api url from appsettings.json
            services.AddHttpClient<GitLabHubClient>(c =>
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiHubConnectorClients:GitLabAPIAddress"))
                );

            services.AddHttpClient<NetlifyHubClient>(c =>
               c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiHubConnectorClients:NetlifyAPIAddress"))
               );


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
