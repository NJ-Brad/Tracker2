namespace Tracker.Models
{
    internal class Heading(string title, string? description = null)
    {
        public string Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public bool IsExpanded { get; set; } = true;
    }
}
