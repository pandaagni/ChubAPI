using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Models.Entity
{
    [PrimaryKey(nameof(ChatroomId), nameof(UserId))]
    public class ChatroomUsers
    {
        public Guid ChatroomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
