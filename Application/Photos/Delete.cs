
using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Photos;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {

        private readonly DataContext _context;

        private readonly IUserAccessor _userAccessor;

        private readonly IPhotoAccessor _photoAccessor;

        public Handler(DataContext context,
                       IUserAccessor userAccessor,
                       IPhotoAccessor photoAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if(user is null) return new();

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

            if(photo is null) return new();

            if(photo.IsMain) return Result.Failure<Unit>("You cannot delete your main photo");

            var result = await _photoAccessor.DeletePhotoAsync(photo.Id);

            if(result is null) return Result.Failure<Unit>("Problem deleting photo from cloudinary");

            user.Photos.Remove(photo);

            var success = await _context.SaveChangesAsync() > 0;

            if(success) return Result.Success(Unit.Value);

            return Result.Failure<Unit>("Problem deleting photo from API");
        }
    }
}