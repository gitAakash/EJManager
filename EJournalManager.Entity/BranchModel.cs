using System;

namespace EJournalManager.Entity
{
    public class BranchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public Boolean IsDeleted { get; set; }
        public string Address { get; set; }
        public int RegionId { get; set; }
    }
}