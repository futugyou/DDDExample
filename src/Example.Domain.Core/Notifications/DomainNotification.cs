namespace Example.Domain.Core;

public class DomainNotification : Event
{
    public DomainNotification(string key, string value)
    {
        NotificationId = Guid.NewGuid();
        Key = key;
        Value = value;
        Version = 1;
    }
    public Guid NotificationId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public int Version { get; set; }
}
