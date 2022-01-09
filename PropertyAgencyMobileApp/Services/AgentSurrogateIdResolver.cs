namespace PropertyAgencyMobileApp.Services
{
    public class AgentSurrogateIdResolver : ISurrogateIdResolver<int>
    {
        private readonly int agentId;

        public AgentSurrogateIdResolver()
        {
            agentId = 1;
        }

        public int ResolveId()
        {
            return agentId;
        }
    }
}
