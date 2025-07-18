using System.Text.Json.Serialization;
using eb4341u202318323.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using eb4341u202318323.API.Shared.Infrastructure.Mediator.Cortext.Configuration;


using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using eb4341u202318323.API.Hr.Application.Internal.CommandServices;
using eb4341u202318323.API.Hr.Application.Internal.EventHandlers;
using eb4341u202318323.API.Hr.Application.Internal.OutboundServices;
using eb4341u202318323.API.Hr.Application.Internal.QueryServices;
using eb4341u202318323.API.Hr.Domain.Model.Commands;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Hr.Domain.Services;
using eb4341u202318323.API.Hr.Infrastructure.Persistence.EFC.Repositories;
using eb4341u202318323.API.Maintenance.Application.ACL;
using eb4341u202318323.API.Maintenance.Application.Internal.CommandServices;
using eb4341u202318323.API.Maintenance.Application.Internal.QueryServices;
using eb4341u202318323.API.Maintenance.Domain.Repositories;
using eb4341u202318323.API.Maintenance.Domain.Services;
using eb4341u202318323.API.Maintenance.Infrastructure.Persistence.EFC.Repositories;
using eb4341u202318323.API.Maintenance.Interfaces.ACL;
using eb4341u202318323.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ACME.LearningCenterPlatform.API",
            Version = "v1",
            Description = "ACME Learning Center Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "ACME Studios",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
            },
        });
    options.EnableAnnotations();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", 
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod().AllowAnyHeader());
});


// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Maintenance Bounded Context
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();
builder.Services.AddScoped<IProjectQueryService, ProjectQueryService>();

// HR Bounded Context
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeCommandService, EmployeeCommandService>();
builder.Services.AddScoped<IEmployeeQueryService, EmployeeQueryService>();
builder.Services.AddScoped<IMaintenanceContextFacade, MaintenanceContextFacade>();
builder.Services.AddScoped<ExternalMaintenanceService>();

// *** REGISTRO DE DEPENDENCIAS PARA CONTRACTTYPE ***
builder.Services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
builder.Services.AddScoped<IContractTypeCommandService, ContractTypeCommandService>();




// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    // *** MODIFICACIÓN AQUÍ ***
    // En lugar de typeof(Program), apunta a un tipo dentro de la asamblea donde está tu manejador de eventos.
    handlerAssemblyMarkerTypes: new[] { typeof(ChangePersonnelBudgetEventHandler) }, // Escanea la asamblea donde está tu manejador
    configure: options =>
    {
        // Esto es para los Command Behaviors (si tienes alguno, como LoggingCommandBehavior)
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    var contractTypeCommandService = services.GetRequiredService<IContractTypeCommandService>();
    await contractTypeCommandService.Handle(new SeedContractTypesCommand());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();