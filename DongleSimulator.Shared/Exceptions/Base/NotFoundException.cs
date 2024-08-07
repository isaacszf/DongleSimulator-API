namespace Shared.Exceptions.Base;

public class NotFoundException : DongleSimulatorException
{
    public NotFoundException(string msg) : base(msg)
    {
    }
}