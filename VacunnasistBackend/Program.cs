global using VacunassistBackend.Data;
using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Helpers;
using VacunassistBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using VacunassistBackend.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var _MyCors = "MyCors";
// Add services to the container.
{
    var services = builder.Services;

    services.Configure<AppSettings>(builder.Configuration.GetSection("JWT"));

    //Adding My Dependencies
    services.AddTransient<IUsersService, UsersService>();
    services.AddTransient<IDevelopedVaccinesService, DevelopedVaccinesService>();
    services.AddTransient<INotificationsService, NotificationsService>();
    services.AddTransient<IBatchVaccinesService, BatchVaccinesService>();
    services.AddTransient<ILocalBatchVaccinesService, LocalBatchVaccinesService>();
    services.AddTransient<IPurchaseOrdersService, PurchaseOrdersService>();

    services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    var jwt = builder.Configuration.GetSection("JWT");
    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.GetValue<string>("Secret")));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
    // Adding Authentication
    services.AddAuthorization()
        .AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                    .AddJwtBearer(x =>
                    {
                        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.MapInboundClaims = false;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.Zero,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,
                            ValidIssuer = jwt.GetValue<string>("ValidIssuer"),
                            ValidAudience = jwt.GetValue<string>("ValidAudience"),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                        x.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                if (context.Request.Query.TryGetValue("token", out var token))
                                {
                                    context.Token = token;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });
    services.AddSingleton(signingCredentials);
    services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
}

builder.Configuration.AddEnvironmentVariables();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: _MyCors, builder =>
            {
                //for when you're running on localhost
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyHeader().AllowAnyMethod();
                //builder.WithOrigins("url from where you're trying to do the requests")
            });
        });

var app = builder.Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
        c.RoutePrefix = string.Empty;
    });

app.UseCors(_MyCors);

app.MapControllers();

app.Run();