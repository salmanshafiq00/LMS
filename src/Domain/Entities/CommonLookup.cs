namespace Domain.Entities;
public class CommonLookup : BaseAuditableEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string NameBN { get; set; }
    public string Description { get; set; }
    public int ParentId { get; set; }
    public string TypeCode { get; set; }
}
