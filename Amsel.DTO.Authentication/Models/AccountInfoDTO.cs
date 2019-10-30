namespace Amsel.DTO.Authentication.Models
{
    public class AccountInfoDTO : AccountDTO
    {

        public  bool Admin { get; set; }

        public  bool Banned { get; set; }

        public  string Email { get; set; }

        public  string TwitchId { get; set; }


    }
}