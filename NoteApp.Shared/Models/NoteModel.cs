using NoteApp.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteApp.Shared.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }

        [NotMapped]
        public Categories[] Category { get; set; }
        public States State { get; set; }
        public int UserId { get; set; }
    }
}
