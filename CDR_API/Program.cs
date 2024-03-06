using CDR_API.Data;
using CDR_API.Models;
using CDR_API.Services.Abstraction;
using CDR_API.Services.Impl;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IFileReadService, FileReadService>();
builder.Services.AddTransient<IRecordsProcessingService, RecordsProcessingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

builder.Services.AddDbContext<CdrContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyLocalDevCorsPolicy",
    builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var fileUploadOptions = new FileUploadOptions();
builder.Configuration.GetSection("FileUploadOptions").Bind(fileUploadOptions);

var fileReadServiceOptions = builder.Configuration.GetSection("FileReadService").Get<FileReadServiceOptions>();
builder.Services.AddSingleton(fileReadServiceOptions);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = fileUploadOptions.MultipartBodyLengthLimit;
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = fileUploadOptions.MaxRequestBodySize;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("MyLocalDevCorsPolicy");

app.MapControllers();

app.Run();
