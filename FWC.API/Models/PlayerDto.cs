namespace FWC.API.Models
{
    public class PlayerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public Guid TeamId { get; set; }
    }
}
