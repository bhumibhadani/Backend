using TestDemo1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TestDemo1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext to use SQL Server with the connection string
builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

// Register CarService
builder.Services.AddScoped<ICarService, CarService>();

// Configure CORS to allow requests from the Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Your Angular app's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure static file serving for uploaded images
var uploadedImagesPath = Path.Combine(builder.Environment.ContentRootPath, "UploadedImages");
var uploadedImagesProvider = new PhysicalFileProvider(uploadedImagesPath);

builder.Services.AddSingleton<IFileProvider>(uploadedImagesProvider);
builder.Services.AddSingleton(new StaticFileOptions
{
    FileProvider = uploadedImagesProvider,
    RequestPath = "/UploadedImages"
});


// Build the application
var app = builder.Build();

// Use the configured CORS policy
app.UseCors("AllowAngularApp");

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve static files and enable directory browsing for debugging
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = uploadedImagesProvider,
    RequestPath = "/UploadedImages"
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = uploadedImagesProvider,
    RequestPath = "/UploadedImages"
});

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Add authorization middleware
app.UseAuthorization();

// Map controllers for API endpoints
app.MapControllers();

// Run the application
app.Run();
