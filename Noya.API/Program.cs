using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// إضافة الخدمات إلى الحاوية
builder.Services.AddControllers();  // تأكد من إضافة هذا السطر هنا
builder.Services.AddEndpointsApiExplorer();  // إضافة Swagger/OpenAPI
builder.Services.AddSwaggerGen();  // إضافة Swagger

// إضافة CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // يسمح لأي مصدر بالوصول
              .AllowAnyMethod()  // يسمح بأي طريقة HTTP (GET, POST, PUT, DELETE, إلخ)
              .AllowAnyHeader(); // يسمح بأي رأس HTTP
    });
});

var app = builder.Build();

// استخدام CORS
app.UseCors("AllowAll");

// تكوين خط الأنابيب لطلبات HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();  // تأكد من أن هذا السطر يأتي بعد app.UseAuthorization()

app.Run();
