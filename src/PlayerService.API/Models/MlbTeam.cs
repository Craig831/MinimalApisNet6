namespace PlayerService.API.Models
{
    public class MlbTeam
    {
        public MlbTeam()
        {
        }

        public int MlbTeamId { get; set; }
        public string? TeamName { get; set; }
        public string? TeamAbbreviation { get; set; }
        public string? TeamCity { get; set; }
    }
}
