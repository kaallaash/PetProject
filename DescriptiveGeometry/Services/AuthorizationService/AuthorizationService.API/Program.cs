using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.DI;
//using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors( options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUsers(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();