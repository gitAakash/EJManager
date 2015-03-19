using System;
using System.ComponentModel.DataAnnotations;

namespace EJournalManager.Entity
{
    public class UserRole
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Role Name")]
        [Display(Name = "Role Name *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Role Code")]
        [Display(Name = "Role Code *")]
        public string RoleCode { get; set; }

        [Required(ErrorMessage = "Please select type of role")]
        [Display(Name = "Is Branch Role *")]
        public Boolean IsBranchRole { get; set; }
    }
}