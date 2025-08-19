namespace Nucleus.Models;

public partial class ApiResponse
{
    public MessageHeader? MessageHeader { get; set; }
    public string? TraceId { get; set; }
    public dynamic? Transaction { get; set; }
}

public class MessageHeader
{
#pragma warning disable CA2227 // Collection properties should be read only
    public IList<MessageElement>? Messages { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
}

public partial class MessageElement
{
    public string? Message { get; set; }
    public string? Type { get; set; }
    public string? Code { get; set; }
    public string? ietf { get; set; }
}