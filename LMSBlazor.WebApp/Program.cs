using LMSBlazor.Plugins.InMemory;
using LMSBlazor.Plugins.SqlServer;
using LMSBlazor.UseCases.Activities;
using LMSBlazor.UseCases.Activities.Interfaces;
using LMSBlazor.UseCases.Common;
using LMSBlazor.UseCases.Common.Interface;
using LMSBlazor.UseCases.Employees;
using LMSBlazor.UseCases.Employees.Interfaces;
using LMSBlazor.UseCases.LeaveApplications;
using LMSBlazor.UseCases.LeaveApplications.Interfaces;
using LMSBlazor.UseCases.Leaves;
using LMSBlazor.UseCases.Leaves.Interfaces;
using LMSBlazor.UseCases.PluginInterfaces;
using LMSBlazor.WebApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// configure authorizations
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Department", "Administration"));
    options.AddPolicy("HR", policy => policy.RequireClaim("Department", "HRDepartment"));
});

var constr = builder.Configuration.GetConnectionString("LeaveManagement");

// configure EF Core for Identity
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(constr);
});

// configure Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AccountDbContext>();

builder.Services.AddDbContextFactory<LMSDbContext>(options =>
{
    options.UseSqlServer(constr);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

if (builder.Environment.IsEnvironment("TESTING"))
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    builder.Services.AddSingleton<ILeaveRepository, LeaveRepository>();
    builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddSingleton<ILeaveApplicationRepository, LeaveApplicationRepository>();
}
else
{
    builder.Services.AddTransient<ILeaveRepository, LeaveEFCoreRepository>();
    builder.Services.AddTransient<IEmployeeRepository, EmployeeEFCoreRepository>();
    builder.Services.AddTransient<ILeaveApplicationRepository, LeaveApplicationEFCoreRepository>();
}


builder.Services.AddTransient<IViewLeavesByNameUseCase, ViewLeavesByNameUseCase>();
builder.Services.AddTransient<IAddLeaveUseCase, AddLeaveUseCase>();
builder.Services.AddTransient<IEditLeaveUseCase, EditLeaveUseCase>();
builder.Services.AddTransient<IViewLeaveByIdUseCase, ViewLeaveByIdUseCase>();

builder.Services.AddTransient<IViewEmployeesByNameUseCase, ViewEmployeesByNameUseCase>();
builder.Services.AddTransient<IAddEmployeeUseCase, AddEmployeeUseCase>();
builder.Services.AddTransient<IEditEmployeeUseCase, EditEmployeeUseCase>();
builder.Services.AddTransient<IViewEmployeeByIdUseCase, ViewEmployeeByIdUseCase>();

builder.Services.AddTransient<IViewLeaveApplicationsByLvNumberUseCase, ViewLeaveApplicationsByLvNumberUseCase>();
builder.Services.AddTransient<IViewLeaveApplicationByIdUseCase, ViewLeaveApplicationByIdUseCase>();
builder.Services.AddTransient<ISearchLeaveApplicationsUseCase, SearchLeaveApplicationsUseCase>();

builder.Services.AddTransient<IApplyLeaveUseCase, ApplyLeaveUseCase>();
builder.Services.AddTransient<IApproveLeaveUseCase, ApproveLeaveUseCase>();
builder.Services.AddTransient<IDenyLeaveUseCase, DenyLeaveUseCase>();
builder.Services.AddTransient<IGenerateRefNumberUseCase, GenerateRefNumberUseCase>();
builder.Services.AddTransient<IGenerateTotalDaysBasedOnDateFromAndDateTo, GenerateTotalDaysBasedOnDateFromAndDateTo>();


builder.Services.AddServerSideBlazor().AddCircuitOptions(e =>
{
    e.DetailedErrors = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();;

app.Run();
