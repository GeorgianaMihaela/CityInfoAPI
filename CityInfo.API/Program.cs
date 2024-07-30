using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers(options =>
    options.ReturnHttpNotAcceptable = true
    ).AddNewtonsoftJson();
//.AddXmlDataContractSerializerFormatters(); to add XML format support for the response im case clients ask

// add user friendly exception message
builder.Services.AddProblemDetails(); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add this for file formats 
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

// globally handle the exceptions
app.UseExceptionHandler(); 

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
