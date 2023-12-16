using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TrackMoney.BLL.TransactionBl;
using TrackMoney.BLL.UserBl;
using TrackMoney.Data.Context;
using TrackMoney.Data.Repos.Repos.Transactions;
using TrackMoney.Data.Repos.Repos.Users;
using TrackMoney.Services.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<TrackMoneyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mssql"))
);

var issuer = builder.Configuration["Jwt:Issuer"];
var secret = builder.Configuration["Jwt:Key"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IUserBl, UserBl>();
builder.Services.AddScoped<IUserRepo, SqlUserRepo>();
builder.Services.AddScoped<ITransactionBl, TransactionBl>();
builder.Services.AddScoped<ITransactionRepo, SqlTransactionRepo>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("DefPolicy", b =>
    {
        b.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();