﻿namespace Core.Application.Results;

public interface IDataResult<out T> : IResult
{
    T Data { get; }
}