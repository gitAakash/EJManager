using System;

namespace EJournalManager.Entity
{
    public class RegionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}