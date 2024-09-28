namespace Tracker.Models
{
    public class TrackedItemModel
    {
        public string Meeting { get; set; } = "";
        public DateTime? WhenCreated { get; set; } = DateTime.Now;
        public string Text { get; set; } = "";
        public string Tag { get; set; } = "";
    }
}
