using PersonCRUD.Models;

namespace PersonCRUD.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        app.MapGet("person", () => new PersonModel("Ricardo"));
    }

}