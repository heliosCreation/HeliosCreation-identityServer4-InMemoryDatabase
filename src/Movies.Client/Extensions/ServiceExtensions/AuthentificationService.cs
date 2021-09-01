using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Movies.Client.Extensions.ServiceExtensions
{
    public static class AuthentificationService
    {
        public static IServiceCollection AddAuthentificationService(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //Make sure when someone connects, he goes through the OIDC
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.AccessDeniedPath = "/Authorization/AccessDenied";
            })
            //Use those parameters for OIDC
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
            {
                opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.Authority = "https://localhost:5005/";
                opt.ClientId = "movies_mvc_client";
                opt.ClientSecret = "secret";

                opt.ResponseType = "code";
                opt.UsePkce = true;

                //Uncomment these to change the callback path set in the IDP
                //opt.CallbackPath = new PathString("");

                opt.Scope.Add("address");
                opt.Scope.Add("roles");
                opt.Scope.Add("movieAPI");

                opt.SaveTokens = true;
                opt.GetClaimsFromUserInfoEndpoint = true;

                //Some claims are filtered by the middleware pipeline. With this command, we remove the filter.
                //opt.ClaimActions.Remove("nbf");
                //Some claims might also not be wanted. With this method, we remove them from the claim Identity. Thus, making the cookie lighter.
                opt.ClaimActions.DeleteClaims("sid", "idp", "s_hash", "auth_time");
                opt.ClaimActions.MapUniqueJsonKey("role", "role");
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
            });

            return services;

        }
    }
}

