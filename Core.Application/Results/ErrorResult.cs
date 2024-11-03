namespace Core.Application.Results;

public class ErrorResult : Result
{
    public ErrorResult() : base(false)
    {

    }

    public ErrorResult(string messsage) : base(false, messsage)
    {

    }
}