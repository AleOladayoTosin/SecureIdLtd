using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureId.AccessControl.API.DTOs;
using SecureId.AccessControl.Persistence;

namespace SecureId.AccessControl.API.IntegrationEvent.Activity
{
    public class GetActivitiesEventHandler
    {
        public class Query : IRequest<ResponseMessage> { }

        public class Handler : IRequestHandler<Query, ResponseMessage>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ResponseMessage> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities =  await _context.Activities.ToListAsync();
                if(activities.Any())
                    return new ResponseMessage { Data = activities, Status = true };

                return new ResponseMessage { Message = "Not Found", Status = false };

            }
        }
    }
}
