namespace Shared.Requests;

public class RequestGenerateImageJson
{
    public IList<string> SourcesIds { get; set; } = [];
    public string TemplateId { get; set; } = string.Empty;
}