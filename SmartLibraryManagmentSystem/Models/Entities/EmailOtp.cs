namespace SmartLibraryManagmentSystem.Models.Entities
{
    public class EmailOtp
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpireTime { get; set; }

    }
}
