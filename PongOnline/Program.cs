using Microsoft.AspNetCore.Authentication.JwtBearer;
using PongOnline.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-n3qh9yho.us.auth0.com/";
    options.Audience = "https://localhost:5001/game";
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("ReadPolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "read:Game"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
