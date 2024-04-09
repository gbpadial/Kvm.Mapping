using Kvm.Mapping.Application;
using Kvm.Mapping.Application.Setup.Installers;
using Kvm.Mapping.Application.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

builder.Services.InstallKeyCloakService(builder.Configuration);
builder.Services.InstallKvmMappingService(builder.Configuration);

// O ideal é usar o Redis ou alguma outra fonte para gravar esta info
var tokenStore = new TokenStore();
builder.Services.AddSingleton(tokenStore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
