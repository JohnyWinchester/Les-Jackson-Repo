using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt
        => opt.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.EnableAnnotations();
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });


    ////var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    ////c.IncludeXmlComments(filePath);

    ////const string rest_api_xml = "WebApi.xml";
    //string rest_api_xml = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    //const string debug_path = "bin/Debug/net7.0";

    //if (File.Exists(rest_api_xml))
    //    c.IncludeXmlComments(rest_api_xml);
    //else if (File.Exists(Path.Combine(debug_path, rest_api_xml)))
    //    c.IncludeXmlComments(Path.Combine(debug_path, rest_api_xml));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDB.PrepPopulation(app);

app.Run();


