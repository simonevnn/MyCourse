using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(MvcOptions =>
{
    MvcOptions.EnableEndpointRouting = false;   // Per rendere possibile app.UseMvc()
});

builder.Services.AddTransient<ICourseService, CourseService>(); // Informiamo .NET Core di costruire un oggetto CourseService qualora un controller richieda di utilizzare ICourseService
/**
 * AddTransient() -> Crea una nuova istanza del servizio per ogni componente che ne ha bisogno. L'istanza viene rimossa dal Garbage Collector non appena il componente non ne ha più bisogno.
 * Lo utilizziamo quando i servizi non sono particolarmente complessi e sono facili da "costruire".
 * 
 * AddScoped() -> Crea una nuova istanza che viene riutilizzata nel contesto della stessa richiesta HTTP
 * Lo utilizziamo quando i servizi sono più pesanti da "costruire" (ad esempio il DbContext).
 * 
 * AddSingleton() -> Crea un'unica istanza e la inietta in tutti i componenti che ne hanno bisogno (riutilizzata anche per richieste HTTP diverse)
 * Lo utilizziamo quando abbiamo dei servizi che devono funzionare al di fuori della singola richiesta HTTP (ad esempio per problemi di concorrenza di un server SMTP nell'invio di mail).
 * Con esso potrebbe anche essere possibile contare il numero delle richieste HTTP.
 * 
 * Nell'utilizzo di un servizio singleton è importante verificare che (se questo ha uno stato interno) sia thread-safe.
 * Questo perché ASP .NET Core lavora in multi-threading, ed è quindi necessario prevenire problemi di race condition. 
 * Grazie alla classe Interlocked è possibile regolare queste situazioni.
 */

var app = builder.Build();

app.UseStaticFiles();   // Per permettere l'utilizzo dei file statici (wwwroot)

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
