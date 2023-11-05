namespace Domain.Entities;
public class CommonLookupDetail : BaseAuditableEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string NameBN { get; set; }
    public string Description { get; set; }
    public int ParentId { get; set; }
    public int LookupId { get; set; }
}
