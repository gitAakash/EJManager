namespace EJournalManager.Entity
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string CreatedBy { get; set; }
    }
}