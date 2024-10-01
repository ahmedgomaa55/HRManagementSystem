namespace HRMangmentSystem.API.DTOS.SettingsDto
{
    public class SettingsQueryDto
    {
        public int Id { get; set; }
        public decimal BonusRate { get; set; }
        public decimal PenaltyRate { get; set; }
        public string WeeklyHoliday1 { get; set; }
        public string? WeeklyHoliday2 { get; set; }
    }
}
