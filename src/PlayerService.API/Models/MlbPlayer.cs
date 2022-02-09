namespace PlayerService.API.Models
{
    public class MlbPlayer
    {
        public MlbPlayer()
        { }

        public int MlbPlayerId { get; set; }
        public string? PlayerFirstName { get; set; }
        public string? PlayerLastName { get; set; }
        public string? PlayerFullName { get; set; }
        public string? PlayerNickName { get; set; }
        public int MlbTeamId { get; set; }
        public string? PositionId { get; set; }
        public string? Status { get; set; }
    }
}
