using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SafeVault;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("VaultContextConnection") ?? throw new InvalidOperationException("Connection string 'VaultContextConnection' not found.");;

builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();
EncryptionHelper.Initialize(builder.Configuration);


// Add EF Core with SQLite
builder.Services.AddDbContext<VaultContext>(options =>
    options.UseSqlite("Data Source=safevault.db"));

// Add Identity
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<VaultContext>();

builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
