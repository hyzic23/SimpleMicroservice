using Microsoft.AspNetCore.Builder;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();
//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot(builder.Configuration);

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => 
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddControllers();
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

app.UseCors("CORSPolicy");
app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

app.Run();
