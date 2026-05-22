using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var swaggerCorsPolicyOrigin = "http://localhost:5216";
var angularCorsPolicyOrigin = "http://localhost:4200";

builder.Services.AddCors(options => {
    options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins(angularCorsPolicyOrigin, swaggerCorsPolicyOrigin)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
