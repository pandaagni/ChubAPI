using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Models.Entity
{
    [PrimaryKey(nameof(MessageId), nameof(UserId))]
    public class MessageReads
    {
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReadAt { get; set; }
    }
}
