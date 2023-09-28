using ApiKey2.Authentication;
using ApiKey2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
int cnt = 0;
List<Game> ActiveGames = new();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapGet("CreateGame", () =>
{
    Game g = new(cnt.ToString());
    ActiveGames.Add(g);
    ++cnt;

    Console.WriteLine(new string('-', Console.WindowWidth));
    foreach (var game in ActiveGames)   
    {
        Console.WriteLine(game.GameId);
    }
    Console.WriteLine(new string('-', Console.WindowWidth));

    return g.GameId;
});

app.Run();