namespace Shared.Responses;

public class ResponseErrorJson
{
    public IList<string> Errors { get; set; }

    public ResponseErrorJson(IList<string> errors) => Errors = errors;

    public ResponseErrorJson(string errMsg)
    {
        Errors = new List<string>();
        Errors.Add(errMsg);
    }
}