using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Models.Entity
{
    [PrimaryKey(nameof(MessageId))]
    public class Messages
    {
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }
        public Guid ChatroomId { get; set; }
        public required string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
