namespace FWC.UI.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public Guid TeamId { get; set; }

        // Navigation properties
        public Team Team { get; set; }
    }
}
