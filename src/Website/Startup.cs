using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Attributes;
using BrickAtHeart.Communities.Models.Authentication;
using BrickAtHeart.Communities.Models.Authorization;
using BrickAtHeart.Communities.Services.Email;
using BrickAtHeart.Communities.Services.Slack;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace BrickAtHeart.Communities
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SqlServerDataClientOptions>(Configuration.GetSection(SqlServerDataClientOptions.Section));
            services.Configure<SendGridOptions>(Configuration.GetSection(SendGridOptions.Section));

            services.AddScoped<IUserDataClient, SqlServerDataClient>();

            services.AddIdentity<User,Role>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>( options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaPage("User", "/Account/Logout");
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/User/Account/Login";
                options.LogoutPath = $"/User/Account/Logout";
                options.AccessDeniedPath = $"/User/Account/AccessDenied";
            });

            ConfigureAuthentication(services);
            ConfigureAuthorization(services);

#if DEBUG
            services.AddSingleton<IEmailService, LocalSmtpEmailService>();
#else
            services.AddSingleton<IEmailService, SendGridEmailService>();
#endif

            services.AddScoped<ISlackService, SlackService>();

            services.AddScoped<ICommunityDataClient, SqlServerDataClient>();
            services.AddScoped<IMembershipDataClient, SqlServerDataClient>();
            services.AddScoped<IRoleDataClient, SqlServerDataClient>();
            services.AddScoped<ICatalogDataClient, SqlServerDataClient>();

            services.AddScoped<CommunityStore>();
            services.AddScoped<MembershipStore>();
            services.AddScoped<RoleStore>();
            services.AddScoped<UserStore>();
            services.AddScoped<CatalogStore>();

            services.AddScoped<IAuthorizationHandler, RequiredRightHandler>();
            services.AddScoped<IAuthorizationHandler, RequireAnyRightHandler>();

            ConfigureHttpClients(services);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            AuthenticationBuilder builder = services.AddAuthentication();

            List<IConfigurationSection> sections = Configuration.GetSection(IdentityProviderOption.Section).GetChildren().ToList();

            foreach (IConfigurationSection section in sections)
            {
                IdentityProviderOption identityProvider = new IdentityProviderOption();
                section.Bind(identityProvider);

                switch (identityProvider?.Type?.ToLower())
                {
                    case "microsoft":

                        if (string.IsNullOrWhiteSpace(identityProvider.DisplayName) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientId) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientSecret))
                            {
                                break;
                            }

                        builder.AddMicrosoftAccount("Microsoft", identityProvider.DisplayName, options =>
                        {
                            options.ClientId = identityProvider.ClientId;
                            options.ClientSecret = identityProvider.ClientSecret;
                        });
                        break;

                    case "facebook":

                        if (string.IsNullOrWhiteSpace(identityProvider.DisplayName) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientId) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientSecret))
                        {
                            break;
                        }

                        builder.AddFacebook("Facebook", identityProvider.DisplayName, options =>
                        {
                            options.AppId = identityProvider.ClientId;
                            options.AppSecret = identityProvider.ClientSecret;
                        });
                        break;

                    //case "twitter":
                    //    builder.AddTwitter("Twitter", identityProvider.DisplayName, options =>
                    //    {
                    //        options.ConsumerKey = identityProvider.ClientId;
                    //        options.ConsumerSecret = identityProvider.ClientSecret;
                    //        options.RetrieveUserDetails = true;
                    //    });
                    //    break;

                    case "google":

                        if (string.IsNullOrWhiteSpace(identityProvider.DisplayName) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientId) ||
                            string.IsNullOrWhiteSpace(identityProvider.ClientSecret))
                        {
                            break;
                        }

                        builder.AddGoogle("Google", identityProvider.DisplayName, options =>
                        {
                            options.ClientId = identityProvider.ClientId;
                            options.ClientSecret = identityProvider.ClientSecret;
                        });
                        break;

                    default:
                        break;
                }
            }
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Right.CreateRole.GetPolicyName(), policy => policy.Requirements.Add(new RequiredRightRequirement(Right.CreateRole)));
                options.AddPolicy(Right.UpdateRole.GetPolicyName(), policy => policy.Requirements.Add(new RequiredRightRequirement(Right.UpdateRole)));
                options.AddPolicy(Right.DeleteRole.GetPolicyName(), policy => policy.Requirements.Add(new RequiredRightRequirement(Right.DeleteRole)));
                options.AddPolicy(Right.MaintainUserGroupProfile.GetPolicyName(), policy => policy.Requirements.Add(new RequiredRightRequirement(Right.MaintainUserGroupProfile)));
                options.AddPolicy(Right.MaintainMemberships.GetPolicyName(), policy => policy.Requirements.Add(new RequiredRightRequirement(Right.MaintainMemberships)));

                options.AddPolicy("MaintainRoles", policy => policy.Requirements.Add(new RequireAnyRightRequirement(new List<Right> { Right.CreateRole, Right.UpdateRole, Right.DeleteRole })));
                options.AddPolicy("MaintainUserGroup", policy => policy.Requirements.Add(new RequireAnyRightRequirement(new List<Right> { Right.CreateRole, Right.UpdateRole, Right.DeleteRole, Right.MaintainUserGroupProfile, Right.MaintainMemberships })));
            });
        }

        private void ConfigureHttpClients(IServiceCollection services)
        {
            SlackOptions slackOptions = new SlackOptions();
            Configuration.GetSection(SlackOptions.Section).Bind(slackOptions);

            services.AddHttpClient<ISlackService, SlackService>(client =>
            {
                client.BaseAddress = new Uri("https://slack.com/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", slackOptions.BotToken);
            });
        }
    }
}