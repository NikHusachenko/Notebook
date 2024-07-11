namespace Notebook.Database.Entities;

public sealed record FollowingEntity : EntityBase
{
    public Guid FollowerId { get; set; }
    public UserEntity Follower { get; set; }

    public Guid FollowingId { get; set; }
    public UserEntity Following { get; set; }
}