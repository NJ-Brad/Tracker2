namespace Tracker.Models
{
    internal class Heading
    {
        public Heading(string title, string? description = null)
        {
            Title = title;
            Description = description;
        }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsExpanded { get; set; } = true;
    }
}
