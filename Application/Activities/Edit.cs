using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using Application.Core;

namespace Application.Activities;

public class Edit
{

    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Activity)
                .SetValidator(new ActivityValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {

        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity!.Id);

            _mapper.Map(request.Activity, activity);
    
            bool result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result.Failure<Unit>("Failed to update activity");

            return Result.Success(Unit.Value);
        }
    }

}