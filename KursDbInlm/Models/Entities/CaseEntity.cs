namespace KursDbInlm.Models.Entities;

internal class CaseEntity
{

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; } = DateTime.Now;


    public int StatusId { get; set; } = 1;

    public StatusEntity Status { get; set; } = null!;

    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}
