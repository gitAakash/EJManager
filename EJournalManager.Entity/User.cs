using System.ComponentModel.DataAnnotations;

namespace EJournalManager.Entity
{
    public class User
    {
        [Required(ErrorMessage = "Please enter Your UserName")]
        [Display(Name = "User name")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid User Name")]
        public string UserName { get; set; }

        public int UserId { get; set; }

        public bool IsPasswordReset { get; set; }


        [DataType(DataType.Password)]
        //[StringLength(15, ErrorMessage = "Password you entered is incorrect. Try Again.", MinimumLength = 8)]
        //[RegularExpression(@"(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&+=*/;':,.<>?-_]).*$", ErrorMessage = "Password you entered is incorrect. Try Again.")]
        [Required(ErrorMessage = "Please enter the Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [RegularExpression(
            @"^([\w-\.$#^&*]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid Email Address")]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid Email Address")]
        // [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid Email Address")]
        public string EmailId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }
    }
}