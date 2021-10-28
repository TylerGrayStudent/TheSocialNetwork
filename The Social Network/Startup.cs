
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using The_Social_Network.Extensions;
using The_Social_Network.MiddlewareClasses;
using The_Social_Network.QuickStart.Data;
using Serilog;
using The_Social_Network.Interfaces;
using The_Social_Network.Services;
using The_Social_Network.Utilities;
using The_Social_Network.Utilities.Interfaces;
using IResourceOwnerPasswordValidator = IdentityServer4.Validation.IResourceOwnerPasswordValidator;

namespace The_Social_Network
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
            services.AddControllersWithViews();

            services.AddSameSiteCookiePolicy();

            services.AddIdentityServer(opts =>
                {
                    opts.Events.RaiseErrorEvents = true;
                    opts.Events.RaiseFailureEvents = true;
                    opts.Events.RaiseInformationEvents = true;
                    opts.Events.RaiseSuccessEvents = true;
                }).AddInMemoryApiScopes(Config.GetApiScopes()).AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryIdentityResources(Config.GetIdentityResources()).AddInMemoryClients(Config.GetClients())
                // .AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential(false);

            services.AddAuthentication()
            .AddLocalApi(opts => opts.ExpectedScope = "api");

            // preserve OIDC state in cache (solves problems with AAD and URL lenghts)
            services.AddOidcStateDataFormatterCache("aad");

            services.AddCors(opts =>
            {
                opts.AddPolicy("api", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            
            services.AddTransient<IRedirectUriValidator, SNRedirectValidator>();
            services.AddTransient<ICorsPolicyService, SNCorsPolicy>();
            // services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
            // services.AddTransient<IProfileService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCookiePolicy();
           // app.UseSerilogRequestLogging();
            app.UseCors("api");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
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