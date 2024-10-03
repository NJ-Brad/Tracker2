namespace Tracker.Models
{
    public class TrackedItemModel
    {
        public string? Meeting { get; set; } = "";
        public DateTime WhenCreated { get; set; } = DateTime.Now;
        public string? Text { get; set; } = "";
        public string? Tag { get; set; } = "";
        public string? FollowUp { get; set; } = "";
        public DateTime? WhenCompleted { get; set; } = null;

        public TrackedItemModel() { }
        public TrackedItemModel(TrackedItemModel Original)
        {
            CopyFrom(Original);
        }
        public void CopyFrom(TrackedItemModel item)
        {
            Meeting = item.Meeting;
            WhenCreated = item.WhenCreated;
            Text = item.Text;
            Tag = item.Tag;
            FollowUp = item.FollowUp;
            WhenCompleted = item.WhenCompleted;
        }
    }
}
