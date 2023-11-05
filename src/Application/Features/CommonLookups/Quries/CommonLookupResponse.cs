using Domain.Entities;

namespace Application.Features.CommonLookups.Quries;
public class CommonLookupResponse
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string NameBN { get; set; }
    public string Description { get; set; }
    public int ParentId { get; set; }
    public string TypeCode { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CommonLookup, CommonLookupResponse>();
        }
    }
}
