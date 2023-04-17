namespace ProjectHub.Catalog.UserService.Data;

public class DbInitializer
{
    public static void Initialize(AuthDbContext context)
    {
        context.Database.EnsureCreated();
    }
}