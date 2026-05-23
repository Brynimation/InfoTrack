using InfoTrackBackend.Clients;
using InfoTrackBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var swaggerCorsPolicyOrigin = builder.Configuration["ServerConfig:SwaggerOrigin"];
var angularCorsPolicyOrigin = builder.Configuration["ServerConfig:FrontEndOrigin"];
var corsPolicyName = "localCorsPolicy";

builder.Services.AddCors(options => {
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins(angularCorsPolicyOrigin, swaggerCorsPolicyOrigin)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SolicitorsClient>();

builder.Services.AddHttpClient<SolicitorsClient>(client =>
{
    client.BaseAddress = new Uri("https://www.solicitors.com");

    //user agent header - used by the server to identify the client - I found that the get request would only return the actual contents of the html when this was non-empty
    var userAgentHeader = "infotracktest/1.0 useragentheader";

    client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgentHeader);
});

builder.Services.AddScoped<IParsingService, ParsingService>();
builder.Services.AddScoped<ISolicitorsService, SolicitorsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
