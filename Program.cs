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
    // dbContext.Database.EnsureDeleted();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }

    await next();
});

app.UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<RequestHandlerMiddleware>();

app.Run();