﻿namespace Core.Application.Results;

public interface IResult
{
    bool Success { get; }
    string Message { get; }
}
