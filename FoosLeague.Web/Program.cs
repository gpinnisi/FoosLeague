using System.Reflection;
using FoosLeague.Core.Commands.Players;
using FoosLeague.Core.Handlers.Players;
using FoosLeague.Core.Models.XResults;
using FoosLeague.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var writeConnectionString = builder.Configuration.GetConnectionString("WriteConnectionString") ?? throw new InvalidOperationException("Connection string 'WriteConnectionString' not found.");
builder.Services.AddDbContext<WriteContext>(options => options.UseSqlServer(writeConnectionString));
var readConnectionString = builder.Configuration.GetConnectionString("ReadConnectionString") ?? throw new InvalidOperationException("Connection string 'ReadConnectionString' not found.");
builder.Services.AddDbContext<ReadContext.ReadonlyContext>(options => options.UseSqlServer(readConnectionString));
builder.Services.AddScoped<ReadContext>();

builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();

// HANDLERS
builder.Services.AddTransient<IRequestHandler<CreatePlayer, XResult<Guid>>, CreatePlayerHandler>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
