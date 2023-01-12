//using AuthorizationService.API.DI;
//using AuthorizationService.BLL.DI;
using AuthorizationService.API.DI;
using DG.API.Validators;
using DG.API.ViewModels;
using DG.BLL.DI;
using FluentValidation;

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

builder.Services.AddDrawings(builder.Configuration);
builder.Services
    .AddAutoMapper(typeof(DG.API.AutoMapper.MappingProfile), typeof(DG.BLL.AutoMapper.MappingProfile));
builder.Services.AddScoped<IValidator<ChangeDrawingViewModel>, ChangeDrawingViewModelValidator>();

builder.Services.AddAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();