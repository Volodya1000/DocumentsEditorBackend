namespace DocumentEditor.Infrastructure;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;

    public int ExpitesHours { get; set; } //скрлько часов будет действителен
}
