using Kvm.Mapping.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kvm.Mapping.API.Controllers;
[ApiController]
[Route("[controller]")]
public class TmsController : ControllerBase
{
    private readonly ILogger<TmsController> _logger;
    private readonly IMediator _mediator;

    public TmsController(ILogger<TmsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("v1/business-unit:from-ecc/{eccBusinessUnitCode}")]
    public async Task<string> GetCode(string eccBusinessUnitCode)
        => await _mediator.Send(new GetEGBusinessUnitCodeFromECCQuery(eccBusinessUnitCode));
}
