using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Dicer.Models;
using Dicer.Repositories;
using Dicer.Interfaces;
using Dicer.Services;
using Dicer.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

#region Dependancy
//scope
builder.Services.AddScoped<IPaymentService, PaymentRepository>();
builder.Services.AddScoped<IAcceptanceService, AcceptanceRepository>();
builder.Services.AddScoped<IProvinsiService, ProvinsiRepository>();
builder.Services.AddScoped<IKotaService, KotaRepository>();
builder.Services.AddScoped<IApiIgService, ApiIgRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IChatMessageService, ChatMessageRepository>();
builder.Services.AddScoped<IProgressService, ProgressRepository>();

//singletone


//transient
builder.Services.AddTransient<IEmailService, EmailService>();

#endregion Dependancy

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Index}");

app.MapHub<ChatHub>("/chatHub");
app.Run();