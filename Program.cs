using HolidayApi.Configurations;
using HolidayApi.Configurations.Database;
using HolidayApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.RegisterAppDependencies();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<IbgeRedirectMiddleware>();

app.Run();




/*
    Implementar o padrão Strategy neste projeto
    https://refactoring.guru/pt-br/design-patterns/strategy
*/