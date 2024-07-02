using EasyCommerce.Autentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

public class CustomSignInManager : SignInManager<ApplicationUser>
{
    public CustomSignInManager(UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user)
    {
        var principal = await base.CreateUserPrincipalAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        if (!identity.HasClaim(c => c.Type == ClaimTypes.Name))
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        }

        return principal;
    }
}
