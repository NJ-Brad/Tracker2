using System.ComponentModel;

namespace Tracker.Models
{
    public class TrackedItemModel : IEditableObject
    {
        // https://stackoverflow.com/questions/14670579/canceledit-on-bindingsource-doesnt-cancel-all-edited-fields-of-an-object
        private TrackedItemModel? _backup;
        public void BeginEdit()
        {
            _backup = new TrackedItemModel(this);
        }
        public void CancelEdit()
        {
            if (_backup != null)
            {
                CopyFrom(_backup);
            }
        }
        public void EndEdit()
        {
            _backup = null;
        }

        // sortable GUID - Req Dotnet 9.0+
        // https://steven-giesel.com/blogPost/ea42a518-4d8b-4e08-8f73-e542bdd3b983
        public string Id { get; set; } = Guid.CreateVersion7(DateTimeOffset.UtcNow).ToString();
        public string? Team { get; set; } = "";
        public string? Meeting { get; set; } = "";
        public DateTime WhenCreated { get; set; } = DateTime.Now;
        public string? Text { get; set; } = "";
        public string? Tag { get; set; } = "";
        public string? Flag { get; set; } = "";
        public string? FollowUp { get; set; } = "";
        public DateTime? WhenCompleted { get; set; } = null;

        public TrackedItemModel() { }
        public TrackedItemModel(TrackedItemModel Original)
        {
            CopyFrom(Original);
        }

        public void CopyFrom(TrackedItemModel Original)
        {
            Id = Original.Id;
            Team = Original.Team;
            Meeting = Original.Meeting;
            WhenCreated = Original.WhenCreated;
            Text = Original.Text;
            Tag = Original.Tag;
            Flag = Original.Flag;
            FollowUp = Original.FollowUp;
            WhenCompleted = Original.WhenCompleted;
        }
    }
}
