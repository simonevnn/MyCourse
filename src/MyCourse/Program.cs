using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(MvcOptions =>
{
    MvcOptions.EnableEndpointRouting = false;   // Per rendere possibile app.UseMvc()
});

builder.Services.AddTransient<CourseService>(); // Diciamo a .NET Core che deve prepararsi ad utilizzare oggetti di tipo CourseService

var app = builder.Build();

app.UseStaticFiles();   // Per permettere l'utilizzo dei file statici

/**
 * MIDDLEWARE DI ROUTING
 * Mappiamo le richieste secondo il template con nome del controller, azione (metodo) e id dell'elemento
 * 
 * Da una richiesta https://localhost:5001/Courses/Detail/5
 * estrae la Route data:
 * - controller = Courses
 * - action = Detail
 * - id = 5 
 * 
 * Il codice scritto qui sotto equivale in realtà ad utilizzare app.UseMvcWithDefaultRoute();
 */
app.UseMvc(routeBuilder =>
{
    routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");    // Con gli "=" indichiamo il valore di default (in caso di risorsa non trovata), e con il "?" indichiamo che l'id è facoltativo
});

app.Run();
