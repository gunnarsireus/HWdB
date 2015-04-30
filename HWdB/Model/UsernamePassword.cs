using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HWdB.Properties;
namespace HWdB.Model
{
    public class UsernamePassword
    {
        [Display(Name = "Username", ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Username_can_not_be_empty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(16, MinimumLength = 5, ErrorMessageResourceName = "Field_must_be_between_X_and_Y_characters", ErrorMessageResourceType = typeof(Strings))]
        public string UserName { get; set; }

        [UIHint("Password")]
        [Display(Name = "Password", ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Password_can_not_be_empty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(16, MinimumLength = 6, ErrorMessageResourceName = "Field_must_be_between_X_and_Y_characters", ErrorMessageResourceType = typeof(Strings))]
        public string Password { get; set; }

        public UsernamePassword()
        {          
            UserName = "";
            Password = "";
        }
        public UsernamePassword(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public string ValidateFields()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                foreach (var result in results)
                {
                    return result.ErrorMessage;
                }
            }
            return null;
        }
    }
}