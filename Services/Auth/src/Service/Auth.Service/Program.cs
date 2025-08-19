using Auth.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nucleus.Api.Dependency;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                      {
                          //   options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                          options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                          options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                      });

builder.Services.Configure<ApiBehaviorOptions>(options =>
       {
           options.SuppressModelStateInvalidFilter = true;
       });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// register nucleus dependencies 
builder.RegisterNucleasDependency();

// register auth service dependencies
builder.Services.Register();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// app.UseHttpsRedirection();

app.Run();


