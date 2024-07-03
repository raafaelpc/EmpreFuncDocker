// EmpresaFuncionarioAPI.API/Program.cs
using EmpresaFuncionario.Application;
using EmpresaFuncionario.Infrastructure;
using EmpresaFuncionario.Infrastructure.Services;
using EmpresaFuncionario.Persistence;
using EmpresaFuncionario.API.Middlewares;
using EmpresaFuncionario.API.Middlewares;
using EmpresaFuncionario.Infrastructure.Interfaces;
using EmpresaFuncionario.Infrastructure.Services;
using EmpresaFuncionario.Persistence;
using EmpresaFuncionario.Persistence.StartupService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

var app = builder.Build();

await new EntityFrameworkCoreMigrator(app.Services).ApplyMigrationsWithRetryAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
