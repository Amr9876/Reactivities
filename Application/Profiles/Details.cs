
using MediatR;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Core;
using Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Interfaces;

namespace Application.Profiles;

public class Details
{
    public class Query : IRequest<Result<Profile>>
    {
        public string Username { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, Result<Profile>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public Handler(IMapper mapper, DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .ProjectTo<Profile>(_mapper.ConfigurationProvider, 
                    new 
                    {
                        currentUsername = _userAccessor.GetUsername()
                    })
                .SingleOrDefaultAsync(x => x.Username == request.Username);  

            return Result.Success(user!);
        }
    }
}