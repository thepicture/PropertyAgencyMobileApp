namespace PropertyAgencyMobileApp.Models
{
    [System.Diagnostics.CodeAnalysis
    .SuppressMessage("Style", "IDE1006:Naming Styles",
    Justification =
    "Specifications and " +
    "DataContractJsonSerializer are defined such that the violation exists")]
    public class Event
    {
        public Event()
        {
        }

        public string uuid { get; set; }
        public int agent_id { get; set; }
        public long datetime { get; set; }
        public int? duration { get; set; }
        public string type { get; set; }
        public string comment { get; set; }
    }
}
