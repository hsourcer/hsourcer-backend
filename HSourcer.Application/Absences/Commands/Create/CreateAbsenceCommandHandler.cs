using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;

namespace HSourcer.Application.Absences.Commands.Create
{
    public class CreateAbsenceCommandHandler : IRequestHandler<CreateAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;

        public CreateAbsenceCommandHandler(IHSourcerDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAbsenceCommand request, CancellationToken cancellationToken)
        {
            //TODO
            //Get identity, assign userId
            var identityUserId = 1;
            var entity = new Absence
            {

                UserId = identityUserId,
                ContactPersonId = request.ContactPersonId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreationDate = DateTime.UtcNow,
                TypeId = (int)request.AbsenceType
            };

            _context.Absences.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.AbsenceId;
        }
    }
}