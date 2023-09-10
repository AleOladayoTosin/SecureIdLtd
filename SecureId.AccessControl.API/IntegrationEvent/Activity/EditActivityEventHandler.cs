using AutoMapper;
using MediatR;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Persistence;

namespace SecureId.AccessControl.API.IntegrationEvent.Activity
{
    public class EditActivityEventHandler
    {
        public class Command : IRequest<ResponseMessage>
        {
            public ActivityDto activityRequest { get; set; }
            public Guid Id { get; set; }
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
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null) return new ResponseMessage { Message = "Not Found", Status = false };

                var map = _mapper.Map(request.activityRequest, activity);

                _context.Update(map);

                var result = await _context.SaveChangesAsync() > 0;
               
                if (!result) return new ResponseMessage { Message = "Failed to edit activity", Status = false };

                return new ResponseMessage { Message = "Successfully updated", Status = true };
            }
        }
    }
    
}
