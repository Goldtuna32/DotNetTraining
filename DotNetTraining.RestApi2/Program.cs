using Microsoft.Extensions.DependencyInjection;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HttpClient>(n=> new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!)
});
//builder.Services.AddSingleton<RestClient>(n=> new RestClient (builder.Configuration.GetSection("ApiUrl").Value!));
builder.Services
    .AddRefitClient<DotNetTraining.RestApi2.Controllers.BirdsController.ISnakeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!));

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
