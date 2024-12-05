namespace PlayTicket.UserService;

public static class ProjectsDbProperties
{
    public const string ConnectionStringName = "UserService";
    public static string DbTablePrefix { get; set; } = "UserService";

    public static string DbSchema { get; set; } = null;
}