using AutoMapper;
using MediatR;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Persistence;

namespace SecureId.AccessControl.API.IntegrationEvent.Activity
{
    public class CreateActivityEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public ActivityDto activityRequest { get; set; }
        }
        public class Handler : IRequestHandler<Command, ResponseMessage>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ResponseMessage> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = _mapper.Map<SecureId.AccessControl.Domain.Activity>(request.activityRequest);

                _context.Activities.Add(activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return new ResponseMessage { Message = "Failed to create activity", Status = false };

                return new ResponseMessage { Message = "Successfully created", Status = true };
            }
        }
    }
    
}
