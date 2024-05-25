using System;
using System.Collections.Generic;

namespace tokesServiceWebApi.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? RoleUser { get; set; }

    public bool? Status { get; set; }
}
