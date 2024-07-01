using System.Security.Principal;

namespace Banking.Core.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
