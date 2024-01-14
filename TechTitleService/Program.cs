var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var frontend = new[]
{
    "React", "Angular", "Next.js", "Express", "Backbone.js", "Bootstrap", "Remix", "Blazor", "MAUI"
};

app.MapGet("/frontend", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new FrontEnd
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            frontend[Random.Shared.Next(frontend.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetFrontend")
.WithOpenApi();

app.Run();

record FrontEnd(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
