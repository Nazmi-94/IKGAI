using IKGAI.BLL.Services;
using IKGAI.BLL.Services.IServices;
using IKGAI.Domain.Entities;
using IKGAI.Domain.Interfaces.IRepository;


//using IKGAI.Domain.Interfaces.IServices;
using IKGAI.Infrastructure;
using IKGAI.Infrastructure.Data;
using IKGAI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddHttpClient();




builder.Services.AddAutoMapper(typeof(Program));


var connStr = builder.Configuration.GetConnectionString("DBconn");
builder.Services.AddDbContext<DB>(options => options.UseSqlServer(connStr)); 

var app = builder.Build();


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        Console.WriteLine($"Unhandled exception: {exception?.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred.");
    });
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
