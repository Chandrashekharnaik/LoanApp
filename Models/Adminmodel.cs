using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LoanApp.Models
{
    public class Adminmodel:IValidatableObject
    {

        public string AdminName { get; set; }

        public string Password { get; set; }

        public bool Rememberme { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ValidationResult vr;
            bool isLoginValid = FormsAuthentication.Authenticate(this.AdminName, this.Password);
            if (isLoginValid)
            {
                vr = ValidationResult.Success;
            }
            else
            {

                vr = new ValidationResult("Invalid Adminname or Password");
               
            }
            return new List<ValidationResult>() { vr };
        }
    }

}