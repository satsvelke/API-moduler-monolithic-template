using Gateway.Ocelot;
using Microsoft.AspNetCore.HttpOverrides;
using Nucleus.Api.Dependency;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration);

// add ocelot.json file 
builder.Configuration.CreateOcelotConfigFiles(@"Ocelot/OcelotConfig/");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//register or enable jwt from nucleas 
builder.RegisterNucleasDependency();

builder.RegisterSerilogUiDependency();

// add authorization 
builder.Services.AddAuthorization();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseForwardedHeaders();

app.UseHttpsRedirection();

#pragma warning disable CA1849 // Call async methods when in an async method
app.UseOcelot().Wait();
#pragma warning restore CA1849 // Call async methods when in an async method

app.UseAuthentication();
app.UseAuthorization();


// serilog ui to view error logs 
app.UseSerilogUiDependency();


await app.RunAsync().ConfigureAwait(true);


