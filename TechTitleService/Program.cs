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
    var forecast =  frontend.Select(index =>
        new FrontEnd
        (
            Name: index               
        ))
        .ToArray();
    return forecast;
})
.WithName("GetFrontend")
.WithOpenApi();

app.Run();

public record FrontEnd(string Name);
