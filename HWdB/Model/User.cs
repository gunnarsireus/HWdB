using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HWdB.Model
{
    public class User : BaseActivatable
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rights { get; set; }
        public string Role { get; set; }
        public bool LogedIn { get; set; }
        public string LastLogin { get; set; }
        public virtual ICollection<LtbDataSet> ltbdatasets { get; set; }
        public static User CreateUser(string userName, string password, string email, string rights, string role)
        {
            return new User { UserName = userName, Password = password, Email = email, Rights = rights, Role = role, LogedIn = false, LastLogin = DateTime.Now.ToString(), active = "1" };
        }
        public void Clone(User that)
        {
            base.Clone(that);
            // Don't clone id, password or lastlogin
            this.UserName = that.UserName;
            this.Email = that.Email;
            this.Rights = that.Rights;
            this.Role = that.Role;
            this.LogedIn = that.LogedIn;
            this.active = that.active;
        }
    }
}
