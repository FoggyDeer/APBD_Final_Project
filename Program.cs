using APBD_Final_Project.DbContexts;
using APBD_Final_Project.Middlewares;
using APBD_Final_Project.Repositories;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services;
using APBD_Final_Project.Services.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<IndividualClientMiddleware>();
app.MapControllers();
app.Run();
