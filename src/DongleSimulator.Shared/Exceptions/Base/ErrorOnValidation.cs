namespace Shared.Exceptions.Base;

public class ErrorOnValidation : DongleSimulatorException
{
    public IList<string> ErrorsMessages { get; set; }
    
    public ErrorOnValidation(IList<string> errorsMessages) : base(string.Empty)
    {
        ErrorsMessages = errorsMessages;
    }

    public ErrorOnValidation(string msg) : base (string.Empty)
    {
        ErrorsMessages = new List<string>();
        ErrorsMessages.Add(msg);
    }
}