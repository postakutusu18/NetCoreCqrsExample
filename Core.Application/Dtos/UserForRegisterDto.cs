﻿namespace Core.Application.Dtos;

public record UserForRegisterDto(string Email, string Password, string FirstName, string LastName) : IDto;
