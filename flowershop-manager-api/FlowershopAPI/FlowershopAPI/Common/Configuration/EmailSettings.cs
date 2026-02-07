namespace FlowerShopAPI.Common.Configuration;

public class EmailSettings
{
    public string Host { get; set; } = String.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } =  string.Empty;
    public string FromName { get; set; } = String.Empty;
    public string From { get; set; } =  string.Empty;
}