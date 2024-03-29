﻿// UserLoginDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Dashboard.DataTransferObjects;

public class UserLoginDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginDto"/> class.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    [JsonConstructor]
    public UserLoginDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }

    public string Password { get; }
}
