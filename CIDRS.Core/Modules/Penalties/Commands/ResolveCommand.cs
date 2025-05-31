using CIDRS.Core.Modules.Penalties.Services;
using CIDRS.Shared.Common.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Penalties.Commands
{
    public class ResolveCommand : IRequest<ResponseResult<bool>>
    {
        public int Id { get; set; }
    }

    public class ResolveCommandHandler : IRequestHandler<ResolveCommand, ResponseResult<bool>>
    {
        private readonly IPenaltyService _penaltyService;

        public ResolveCommandHandler(IPenaltyService penaltyService)
        {
            _penaltyService = penaltyService;
        }
        public async Task<ResponseResult<bool>> Handle(ResolveCommand request, CancellationToken cancellationToken)
        {
            return await _penaltyService.ResolveAsync(request.Id);
        }
    }
}
