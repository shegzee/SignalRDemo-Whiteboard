using Microsoft.AspNetCore.SignalR;
using SignalRDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("https://localhost")
        .AllowCredentials();
    });
});



builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WhiteboardHub>("/whiteboardHub");
    endpoints.MapControllers();
});

app.MapRazorPages();

app.Run();
