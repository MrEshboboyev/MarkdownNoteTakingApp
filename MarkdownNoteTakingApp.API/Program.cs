using MarkdownNoteTakingApp.Application.Common.Interfaces;
using MarkdownNoteTakingApp.Application.Services.Implementation;
using MarkdownNoteTakingApp.Application.Services.Interfaces;
using MarkdownNoteTakingApp.Infrastructure.Data;
using MarkdownNoteTakingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// register http clients
builder.Services.AddHttpClient<IGrammarCheckService>();

// lifetime for services
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IGrammarCheckService, GrammarCheckService>();
builder.Services.AddScoped<IMarkdownRenderService, MarkdownRenderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// added Program.cs file, remove pyyyy.txt
