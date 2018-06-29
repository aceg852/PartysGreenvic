namespace PartysGreenvic.Models
{
    using SQLite.Net.Attributes;
    public class UserLocal
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Rut { get; set; }
        public override int GetHashCode()
        {
            return UserId;

        }
    }
}