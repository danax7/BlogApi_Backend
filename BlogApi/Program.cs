using BlogApi;
using BlogApi.Context;
using BlogApi.Middleware;
using BlogApi.Repository;
using BlogApi.Repository.Interface;
using BlogApi.Service;
using BlogApi.Service.Interface;
using BlogApi.Services;
using BlogApi.Services.Interface;
using BlogApi.ValidateToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food delivery", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
    });
});

builder.Services.AddScoped<IPostRepository, PostRepositoryImpl>();
builder.Services.AddScoped<IPostService, PostServiceImpl>();

builder.Services.AddScoped<ICommentRepository, CommentRepositoryImpl>();
builder.Services.AddScoped<ICommentService, CommentServiceImpl>();

builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

builder.Services.AddScoped<ITokenRepository, TokenRepositoryImpl>();
builder.Services.AddScoped<ITokenService, TokenServiceImpl>();

builder.Services.AddScoped<IAuthorService, AuthorServiceImpl>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepositoryImpl>();

builder.Services.AddScoped<ITagRepository, TagRepositoryImpl>();
builder.Services.AddScoped<ITagService, TagServiceImpl>();

builder.Services.AddScoped<ICommunityRepository, CommunityRepositoryImpl>();
builder.Services.AddScoped<ICommunityService, CommunityServiceImpl>();


builder.Services.AddScoped<IAuthorizationHandler, ValidateAccessTokenRequirementHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "ValidateToken",
        policy => policy.Requirements.Add(new ValidateAccessTokenRequirement()));
});

var connectionPsql = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<BlogDbContext>(options => options.UseNpgsql(connectionPsql));
builder.Services.AddTransient<BlogDbContext>();

builder.Services.AddDbContext<AddressDbContext>(options => options.UseNpgsql(connectionPsql));
builder.Services.AddTransient<AddressDbContext>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetService<BlogDbContext>();
var addressDbContext = serviceScope.ServiceProvider.GetService<AddressDbContext>();

addressDbContext?.Database.Migrate();
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