using System.Threading;
using System.Threading.Tasks;
using MediatR;
using HSourcer.Application.Interfaces;
using HSourcer.Domain.Entities;
using System;
using HSourcer.Application.UserIdentity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HSourcer.Domain.Security;
using HSourcer.Application.Notifications.Models;
using System.Collections.Generic;

namespace HSourcer.Application.Absences.Commands.Update
{
    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, int>
    {
        private readonly IHSourcerDbContext _context;
        private readonly UserResolverService _userResolver;

        public UpdateOrganizationCommandHandler(IHSourcerDbContext context, UserResolverService userResolver)
        {
            _context = context;
            _userResolver = userResolver;
        }

        public async Task<int> Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
        {
            var user = (await _userResolver.GetUserIdentity());
            var organization = await _context.Users
                .Where(u => u == user)
                .Include(w => w.Team.Organization)
                .Select(w=>w.Team.Organization).FirstAsync();
            organization.Name = command.Name;
            organization.Description = command.Description;

            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync(cancellationToken);

            return organization.OrganizationId;
        }
    }
}