using FileUploadServiceAPI.Repositories;
using FileUploadServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "allowCorsFromSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:4200") // specifying the allowed origin
                            .AllowAnyMethod() // defining the allowed HTTP method
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

// Add services to the container.

builder.Services.AddControllers();

//Add dependencies
builder.Services.AddTransient<IFileUploadService, FileUploadService>();
builder.Services.AddSingleton<IFileUploadRepository, FileUploadInMemoryRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Open API v1");
        //options.SwaggerEndpoint("/swagger/v1swagger.json", "File Upload service v1");
    });
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
