using NoteApp.Shared.Enums;

namespace NoteApp.Shared.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public States State { get; set; }

    }
}
