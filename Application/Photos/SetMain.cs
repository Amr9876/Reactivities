using MediatR;
using Domain;
using Application.Core;
using Persistence;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Photos;

public class SetMain
{
    
    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {

        private readonly DataContext _context;

        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if(user is null) return new();

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

            if(photo is null) return new();

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if(currentMain is not null) currentMain.IsMain = false;

            photo.IsMain = true;

            var success = await _context.SaveChangesAsync() > 0;

            if(success) return Result.Success(Unit.Value);

            return Result.Failure<Unit>("Problem setting photo as main");
        }
    }

}