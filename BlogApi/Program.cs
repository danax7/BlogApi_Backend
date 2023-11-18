using BlogApi;
using BlogApi.Middleware;
using BlogApi.Repository;
using BlogApi.Repository.Interface;
using BlogApi.Service;
using BlogApi.Service.Interface;
using BlogApi.Services;
using BlogApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostRepositoryImpl>();
builder.Services.AddScoped<IPostService, PostServiceImpl>();

builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

builder.Services.AddScoped<ITokenRepository, TokenRepositoryImpl>();
builder.Services.AddScoped<ITokenService, TokenServiceImpl>();

builder.Services.AddScoped<ITagRepository, TagRepositoryImpl>();
builder.Services.AddScoped<ITagService, TagServiceImpl>();

var connectionPsql = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<BlogDbContext>(options => options.UseNpgsql(connectionPsql));
builder.Services.AddTransient<BlogDbContext>();


var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetService<BlogDbContext>();
dbContext?.Database.Migrate();

app.UseExceptionHandlingMiddleware();

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