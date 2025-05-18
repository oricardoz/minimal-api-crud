using Microsoft.EntityFrameworkCore;
using PersonCRUD.Data;
using PersonCRUD.Models;

namespace PersonCRUD.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");

        route.MapPost("",
            async (PersonRequest req, PersonContext context) =>
            {
                var person = new PersonModel(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
                return Results.Created($"person/{person.Id}", person);
            });

        route.MapGet("",
            async (PersonContext context) =>
            {
                var persons = await context.People.ToListAsync();
                return Results.Ok(persons);
            });

        route.MapGet("{id:guid}",
            async (Guid id, PersonContext context) =>
            {
                var person = await context.People.FindAsync(id);
                if (person == null)
                    return Results.NotFound();
                return Results.Ok(person);
            });

        route.MapPut("{id:guid}", 
            async (Guid id, PersonRequest req, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(p => p.Id == id);
            if(person == null)
                return Results.NotFound();
            person.ChangeName(req.name);
            await context.SaveChangesAsync();
            
            return Results.Ok(person);
        });

        route.MapDelete("{id:guid}",
            async (Guid id, PersonContext context) =>
            {
                var person = await context.People.FirstOrDefaultAsync(p => p.Id == id);
                if(person == null)
                    return Results.NotFound();
                person.SetInactive();
                await context.SaveChangesAsync();
                return Results.Ok(person);
            });
    }

}