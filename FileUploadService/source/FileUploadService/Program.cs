using FileUploadService;
using FileUploadService.DataValidation;
using FileUploadService.Exceptions;
using FileUploadService.FileService;
using FileUploadService.Helpers;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = builder.Configuration.GetValue<long?>("MaximumFileSize") ?? serverOptions.Limits.MaxRequestBodySize;
});
builder.Services.AddScoped<IDataValidator, DataValidator>();
builder.Services.AddScoped<IValidationMessageHelper, ValidationMessageHelper>();
builder.Services.AddScoped<IFileService, FileService>();

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "FileValidation.Api", Version = "v1" });

    setup.ResolveConflictingActions(x => x.First());
    setup.OperationFilter<FileOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
