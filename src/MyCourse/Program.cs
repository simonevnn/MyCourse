var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();   //Per permettere l'utilizzo dei file statici

app.MapGet("/", () => "Hello World!");

app.Run();
