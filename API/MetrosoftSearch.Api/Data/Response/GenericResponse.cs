﻿namespace MetrosoftSearch.Api.Data.Response;

public class GenericResponse<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
}
