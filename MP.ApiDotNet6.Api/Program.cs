//Para incrementar mais entidades, seguir os passos:

//00 => Criar a tabela na base de dados
//01 => Criar a entidade e validações em Domain/Entities/Validation
//02 => Criar o mapemaneto da entidade em Data/Maps
//03 => Adicionar o DbSet da entidade em Data/Context/ApplicationDbContex
//04 => Criar a Interface  de repositório em Domain/Repositories
//05 => Criar a classe concreta no Repositório em Data/Repositories
//06 => Criar a classe DTO em Application/DTOs
//07 => Criar a validation em Application/DTOs/Validations

//08 => Criar as conversões DomainToDto e DtoToDomain em Application/Mappings

//09 => Criar a Service Interface ou a função nova na interface da entidade em Application/Services/Interfaces
//10 => Criar o Service ou implementar a função no Service em Application/Services
//11 => Fazer as injeções de dependencia do Service/Repository em IoC/DependencyInjection
//12 => Criar/Atualizar o Controller em Api/Controllers


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MP.ApiDotNet6.Infra.IoC;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Configurar Swagger para trabalhar com token(rotas protegidas)
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Api Dotnet 6",
        Version= "v1",
        Description = "Criando uma api em dotnet core 6"
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Autenticação em JWT. Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        },
    });
});

//Referencia as dependencias de IoC DependencyInjection
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddService(builder.Configuration);

//Remove parametros nulos nas respostas dos endpoints
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//Inicialização de autenticação
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projetoDotNetCore6"));
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata= false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Sistema de autenticação
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
