using HWdB.CustomValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HWdB.Model
{
    public class User : BaseActivatable
    {
        bool _loopOk;

        public int Id { get; set; }

        [NotMapped]
        public bool IsSelected
        {
            get { return GetValue(() => IsSelected); }
            set { SetValue(() => IsSelected, value); }
        }

        [RegularExpression(@"^[a-z0-9_\-]+$", ErrorMessage = "Only lower case letters, no spaces")]
        [UserNameUniqueAttribute]
        public string UserName
        {
            get { return GetValue(() => UserName); }
            set
            {
                _loopOk = (value != UserName);
                SetValue(() => UserName, value);
                if (_loopOk)
                {
                    Password = Password;
                }
            }
        }

        public string Password
        {
            get { return GetValue(() => Password); }
            set
            {
                _loopOk = (value != Password);
                SetValue(() => Password, value);
                if (_loopOk)
                {
                    UserName = UserName;
                }
            }
        }
        [RegularExpression(@"^[\wÅÄÖåäö\-_]+(\.[\wÅÄÖåäö\-_]+)*@[\wÅÄÖåäö\-_]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Not valid e-mail")]
        public string Email
        {
            get { return GetValue(() => Email); }
            set { SetValue(() => Email, value); }
        }
        public string Rights { get; set; }
        public string Role
        {
            get { return GetValue(() => Role); }
            set { SetValue(() => Role, value); }
        }
        public bool LogedIn
        {
            get { return GetValue(() => LogedIn); }
            set { SetValue(() => LogedIn, value); }
        }
        public string LastLogin
        {
            get { return GetValue(() => LastLogin); }
            set { SetValue(() => LastLogin, value); }
        }

        public static User CreateUser(string userName, string password, string email, string rights, string role)
        {
            return new User { UserName = userName, Password = password, Email = email, Rights = rights, Role = role, LogedIn = false, LastLogin = DateTime.Now.ToString(), active = "1" };
        }
        public void Clone(User that)
        {
            base.Clone(that);
            this.Id = that.Id;
            this.Password = that.Password;
            this.LastLogin = that.LastLogin;
            this.UserName = that.UserName;
            this.Email = that.Email;
            this.Rights = that.Rights;
            this.Role = that.Role;
            this.LogedIn = that.LogedIn;
            this.active = that.active;
        }
    }
}
