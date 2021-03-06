using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using VironIT_Social_network_server.BLL;
using VironIT_Social_network_server.BLL.Services;
using VironIT_Social_network_server.BLL.Services.Interface;
using VironIT_Social_network_server.DAL.Context;
using VironIT_Social_network_server.DAL.UnitOfWork;
using VironIT_Social_network_server.WEB.Identity;
using VironIT_Social_network_server.WEB.Identity.JWT;
using VironIT_Social_network_server.WEB.IdentityProvider;
using VironIT_Social_network_server.WEB.SignalR;


namespace VironIT_Social_network_server.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string angularSPAClientForlder = "..\\..\\..\\..\\client\\vironit-social-network";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(
                options => 
                { 
                    options.UseNpgsql(Configuration.GetConnectionString("UsersConnection"));
                });
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    options.User.RequireUniqueEmail = true;
                });
            services.AddDbContext<MediaContext>(
                options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("MediaConnection"),
                        b => b.MigrationsAssembly("VironIT_Social_network_server.WEB"));
                });
            services.AddDbContext<ContactContext>(
                options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("ContactsConnection"),
                        b => b.MigrationsAssembly("VironIT_Social_network_server.WEB"));
                });
            services.AddDbContext<MessageContext>(
                options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("MessagesConnection"),
                        b => b.MigrationsAssembly("VironIT_Social_network_server.WEB"));
                });

            services.AddCors();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = JwtOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Secret)),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Query["token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(token) &&
                                (path.StartsWithSegments("/hubs/message")))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };
                }
            );

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, MessageHubUserIdProvider>();

            services.AddScoped(provider => new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new WEBMapperProfile(provider.GetService<UserManager<User>>()));
                    cfg.AddProfile(new BLLMapperProfile());
                }).CreateMapper());

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IAudioService, AudioService>();
            services.AddTransient<IVideoService, VideoService>();

            services.AddScoped<IUnitOfWork<MediaContext>, UnitOfWork<MediaContext>>();
            services.AddScoped<IUnitOfWork<ContactContext>, UnitOfWork<ContactContext>>();
            services.AddScoped<IUnitOfWork<MessageContext>, UnitOfWork<MessageContext>>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $"{angularSPAClientForlder}\\dist\\vironit-social-network";
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseSpaStaticFiles();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                if (context.Response.StatusCode != StatusCodes.Status401Unauthorized)
                {
                    await next.Invoke();
                }
            });
            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapHub<MessageHub>("/hubs/message");
                builder.MapControllers();
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = angularSPAClientForlder;

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
