using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShopping.IdentityServer.Data;

namespace VShopping.IdentityServer.Services
{
    public class ProfileAppServices : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaims;

        public ProfileAppServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaims)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaims = userClaims;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //ID do usuario do Identity Server
            string id = context.Subject.GetSubjectId();

            //Localiza o usuario pelo ID
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            //Cria ClaimPrincipal para o usuario
            ClaimsPrincipal userClaims = await _userClaims.CreateAsync(user);

            //Define uma coleção de claims para o usuario
            //e inclui o nome e sobre nome do usuario
            List<Claim> claims = userClaims.Claims.ToList();
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            //se o UserManager do identity suporta role
            if (_userManager.SupportsUserRole)
            {
                //obtem a lista dos nomes dos roles para o usuario
                IList<string> roles = await _userManager.GetRolesAsync(user);
                
                //percorre a lista
                foreach (string role in roles)
                {
                    //add role na claim
                    claims.Add(new Claim(JwtClaimTypes.Role, role));

                    //se roleManager suporta claims para a role
                    if (_roleManager.SupportsRoleClaims)
                    {
                        //localiza perfil
                        IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                        //inclui o perfil
                        if(identityRole != null)
                        {
                            //inclui as claims associada com a role
                            claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                        }
                    }
                }
            }
            //retorna as claims do contexto
            context.IssuedClaims = claims;  
         
        }
        public async Task IsActiveAsync(IsActiveContext context)
        {
            //ID do usuario do Identity Server
            string id = context.Subject.GetSubjectId();

            //Localiza o usuario pelo ID
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            //Verifica se está ativo
            context.IsActive = user is not null;

        }
    }
}
