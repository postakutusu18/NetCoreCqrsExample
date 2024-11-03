using Core.Persistance.Repositories;
using Core.Security.Enums;

namespace Core.Security.Entities;

public class UserBase<TId> : Entity<TId>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }

    public UserBase()
    {
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
    }

    public UserBase(string email, string firstName,string lastName, byte[] passwordSalt, byte[] passwordHash, AuthenticatorType authenticatorType)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        AuthenticatorType = authenticatorType;
    }

    public UserBase(TId id, string email,string firstName,string lastName, byte[] passwordSalt, byte[] passwordHash, AuthenticatorType authenticatorType)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        AuthenticatorType = authenticatorType;
    }
}