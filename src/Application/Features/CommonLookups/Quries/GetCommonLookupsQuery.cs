using Application.Common.Interfaces;
using Application.Common.Security;
using Application.Constants;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommonLookups.Quries;

[Authorize(Policy = Permissions.ApplicationUsers.View)]
public record GetCommonLookupsQuery : IRequest<IList<CommonLookupResponse>>;

internal sealed class GetCommonLookupsQueryHandler : IRequestHandler<GetCommonLookupsQuery, IList<CommonLookupResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCommonLookupsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IList<CommonLookupResponse>> Handle(GetCommonLookupsQuery request, CancellationToken cancellationToken)
    {
        return await _context.CommonLookups
            .OrderByDescending(x => x.Created)
            .ProjectTo<CommonLookupResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
