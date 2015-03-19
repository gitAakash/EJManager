using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EJournalManager.Entity
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid First Name")]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid Last Name")]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Email Address")]
        [RegularExpression(
            @"^([\w-\.$#^&*]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid Email Address")]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        public Boolean IsActive { get; set; }

        [Required(ErrorMessage = "Please select one of the valid roles")]
        [Display(Name = "Role *")]
        public int Role { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }

        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "Username *")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please enter valid User Name")]
        public string Username { get; set; }

        [Display(Name = "Password *")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at most 15 characters long.",
            MinimumLength = 8)]
        //[RegularExpression(@"(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&+=*/;':,.<>?-_]).*$", ErrorMessage = "Password must contain letters,numbers and 1 special character.No space allowed.")]
        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }

        public int RoleId { get; set; }


        [Required(ErrorMessage = "Please select one of the valid branch")]
        [Display(Name = "Branch *")]
        public int Branch { get; set; }

        public IEnumerable<SelectListItem> BranchList { get; set; }

        public int BranchId { get; set; }

        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Please select one of the valid Region")]
        [Display(Name = "Region *")]
        public int Region { get; set; }

        public IEnumerable<SelectListItem> RegionList { get; set; }

        public int RegionId { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number")]
        [RegularExpression("^[0-9\\-\\+]{9,15}$", ErrorMessage = "Phone Number is invalid.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number *")]
        public string PhoneNumber { get; set; }

        public int CreatedBy { get; set; }
        public int TotalCount { get; set; }
        public string RoleName { get; set; }
        public string BranchName { get; set; }
        public string Status { get; set; }
        public string TerminalId { get; set; }

        public int UserStatus { get; set; }
        public string RegionName { get; set; }
    }
}