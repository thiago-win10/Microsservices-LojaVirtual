using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShopping.IdentityServer.Configuration;
using VShopping.IdentityServer.Data;

namespace VShopping.IdentityServer.SeedDataBase;

public class DataBaseIdentityServerInitializer : IDataBaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        //Se o perfil Admin nao existir então cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            //Cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }

        //Se o perfil Client nao existir então cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            //Cria o perfil Client
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }

    }

    public void InitializeSeedUsers()
    {
        //Se o usuario admin não existir cria o usuario, define a senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            //define os dados do usuario admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12484-7895",
                FirstName = "Usuario",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Cria o usuario Admin e atribui a senha
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Impri#2022").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuario admin ao perfil admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuario admin
                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }

        }

        //Se o usuario client não existir cria o usuario, define a senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            //define os dados do usuario client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "clien1@com.br",
                NormalizedEmail = "CLIENT1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12484-7895",
                FirstName = "Usuario",
                LastName = "Client1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Cria o usuario Client e atribui a senha
            IdentityResult resultClient = _userManager.CreateAsync(client, "Impri#2022").Result;
            if (resultClient.Succeeded)
            {
                //inclui o usuario client ao perfil admin
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                //inclui as claims do usuario admin
                var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }

        }
    }
}