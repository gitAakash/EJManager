namespace EJournalManager.Entity
{
    public class VmUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
        public int TotalCount { get; set; }
        public int Role { get; set; }
        public int Branch { get; set; }
        //public int IsActive { get; set; }
        public string IsActive { get; set; }
        public string UserSatus { get; set; }
    }
}