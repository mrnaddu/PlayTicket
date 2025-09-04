namespace PlayTicket.Hosting.Shared.Options;

public class AuthOptions
{
    public string? Region { get; set; }
    public string? TckPoolId { get; set; }
    public string? KskPoolId { get; set; }
    public string? TckClientId { get; set; }
    public string? KskClientId { get; set; }
    public string? RequireHttpsMetadata { get; set; }
    public string? DisablePII { get; set; }
}
