using CharacterDatabase.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using CharacterDatabase.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace CharacterDatabase
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            var keyVaultName = "CharacterDatabasevault";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            KeyVaultSecret fb_appId = await client.GetSecretAsync("Facebook-AppId");
            KeyVaultSecret fb_appSecret = await client.GetSecretAsync("Facebook-AppSecret");
            KeyVaultSecret g_clientId = await client.GetSecretAsync("Google-ClientId");
            KeyVaultSecret g_clientSecret = await client.GetSecretAsync("Google-ClientSecret");

            builder.Services.AddTransient<IDbConnection>((s) =>
            {
                IDbConnection conn = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));

                conn.Open();
                return conn;
            });

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
            builder.Services.AddTransient<ICharacterService, CharacterService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

            builder.Services.ConfigureApplicationCookie(o => {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(4));

            builder.Services.AddAuthentication()

                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = g_clientId.ToString();
                    googleOptions.ClientSecret = g_clientSecret.ToString();
                    googleOptions.AccessDeniedPath = "/Home/AccessDenied";
                })


                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = fb_appId.ToString();
                    facebookOptions.AppSecret = fb_appSecret.ToString();
                    facebookOptions.AccessDeniedPath = "/Home/AccessDenied";
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}