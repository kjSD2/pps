using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IBankOfficeService, BankOfficeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBankAtmService, BankAtmService>();
builder.Services.AddScoped<IPaymentAccountService, PaymentAccountService>();
builder.Services.AddScoped<ICreditAccountService, CreditAccountService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICreditService, CreditService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Применение миграций
    DbInitializer.Initialize(context); // Инициализация базы данных с новыми данными
}

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