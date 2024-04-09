using Kvm.Mapping.Application.Models;
using Kvm.Mapping.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kvm.Mapping.Application.Queries;

public class GetEGBusinessUnitCodeFromECCQuery : IRequest<string>
{
    public string ECCBusinessUnitCode { get; set; }

    public GetEGBusinessUnitCodeFromECCQuery(string eCCBusinessUnitCode)
    {
        ECCBusinessUnitCode = eCCBusinessUnitCode;
    }
}

public class GetEGBusinessUnitCodeFromECCQueryHandler : IRequestHandler<GetEGBusinessUnitCodeFromECCQuery, string>
{
    private readonly IKvmMappingService _kvmMappingService;
    private readonly ILogger<GetEGBusinessUnitCodeFromECCQueryHandler> _logger;

    public GetEGBusinessUnitCodeFromECCQueryHandler(IKvmMappingService kvmMappingService, ILogger<GetEGBusinessUnitCodeFromECCQueryHandler> logger)
    {
        _kvmMappingService = kvmMappingService;
        _logger = logger;
    }

    public async Task<string> Handle(GetEGBusinessUnitCodeFromECCQuery request, CancellationToken cancellationToken)
    {
        var s4Code = await GetMappedCodeAsync(request.ECCBusinessUnitCode, "ECC", "S4");
        var egCode = await GetMappedCodeAsync(s4Code, "S4", "EG");

        return egCode;
    }

    private async Task<string> GetMappedCodeAsync(string code, string from, string to)
    {
        var codeMappingRequest = new KvmMappingRequest(
            code,
            "BusinessUnitCode",
            to,
            from,
            "codigoTipoUnidadeBasica",
            "Eq",
            "F"
        );

        var codeMappingResponse = await _kvmMappingService.GetMapping(codeMappingRequest);
        if (!codeMappingResponse.IsSuccessStatusCode)
        {
            _logger.LogError(codeMappingResponse.Error, codeMappingResponse.Error!.Content);
            return string.Empty;
        }

        return codeMappingResponse.Content!.ValueTo;
    }
}
