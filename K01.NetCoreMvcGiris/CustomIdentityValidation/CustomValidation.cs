using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.CustomIdentityValidation
{
    public class CustomValidation : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError() {
            Code= "PasswordRequiresDigit",
            Description="Parola sayı içermelidir"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = "Parola küçük harf içermelidir"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} kadar karakter içermelidir"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = $"Parola büyük harf içermelidir"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} daha önce alınmış, lütfen farklı bir kullanıcı adı giriniz"
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = "PasswordMismatch",
                Description = $" eski parolanız yanlış"
            };
        }
    }
}
