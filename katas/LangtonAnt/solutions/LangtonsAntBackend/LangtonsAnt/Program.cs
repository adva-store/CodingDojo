using LangtonsAntAPI.LangtonsAntBackend;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

LangtonsAntMain langtonsAntMain = new();

app.MapGet("api/langtonsant/", () =>
{
    langtonsAntMain.NextStep();
    return Results.Created($"api/langtonsant/", langtonsAntMain);
});


app.MapPost("api/langtonsant", (LangtonsAntMain iLangtonsAntMain) =>
{
    return CreateNewGame(iLangtonsAntMain);
});

app.MapPut("api/langtonsant", (LangtonsAntMain iLangtonsAntMain) =>
{
    return CreateNewGame(iLangtonsAntMain);
});

app.MapDelete("api/langtonsant/", () =>
{
    langtonsAntMain = new LangtonsAntMain();
    return Results.Ok();

});

IResult CreateNewGame(LangtonsAntMain iLangtonsAntMain)
{
    langtonsAntMain = iLangtonsAntMain;
    langtonsAntMain.Initialize();
    return Results.Created($"api/langtonsant/", langtonsAntMain);
}

app.Run();

