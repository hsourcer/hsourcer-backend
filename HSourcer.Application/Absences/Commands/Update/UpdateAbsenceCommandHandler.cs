using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateAbsenceCommandHandler : IRequestHandler<UpdateAbsenceCommand, int>
    {
        private readonly IHSourcerDbContext _context;

        public UpdateAbsenceCommandHandler(IHSourcerDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateAbsenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Absences.FindAsync(request.AbsenceId);
            //TODO 
            //Get identity and assign

            entity.Status = (int)request.Status;
            entity.DecisionDate = DateTime.UtcNow;
            entity.Comment = request.Comment;

            _context.Absences.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.AbsenceId;
        }
    }
}