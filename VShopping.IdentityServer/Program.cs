using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VShopping.IdentityServer.Configuration;
using VShopping.IdentityServer.Data;
using VShopping.IdentityServer.SeedDataBase;
using VShopping.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Banco de dados
//var mySql = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//              options.UseMySql(mySql, ServerVersion.AutoDetect(mySql)));

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=VShoppingIdentityServerBD; Username=postgres;Password=31510635"));

//IdentityServer Events

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

var builderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(
                           IdentityConfiguration.IdentityResources)
                            .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                            .AddInMemoryClients(IdentityConfiguration.Clients)
                           .AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDataBaseSeedInitializer, DataBaseIdentityServerInitializer>();

//Injeção de Dependencia do ProfileAppService referente as roles/claims do identity
builder.Services.AddScoped<IProfileService, ProfileAppServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

SeedDataBaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//Seed populando o identity na inicialização do projeto identity

void SeedDataBaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var initRolesUsers = serviceScope.ServiceProvider.GetService<IDataBaseSeedInitializer>();

        initRolesUsers.InitializeSeedUsers();
        initRolesUsers.InitializeSeedRoles();
    }
}
