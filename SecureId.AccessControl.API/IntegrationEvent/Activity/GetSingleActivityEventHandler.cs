using MediatR;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Persistence;

namespace SecureId.AccessControl.API.IntegrationEvent.Activity
{
    public class GetSingleActivityEventHandler
    {
        public class Query : IRequest<ResponseMessage> { public Guid Id { get; set; } }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity != null)
                    return new ResponseMessage { Data = activity, Status = true };

                return new ResponseMessage { Message = "Not Found", Status = false };

            }
        }
    }
}
