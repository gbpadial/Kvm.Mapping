using Kvm.Mapping.Application.Models;
using Refit;

namespace Kvm.Mapping.Application.Services;

public interface IKvmMappingService
{
    [Get("/v1/kvm:mapping")]
    Task<IApiResponse<KvmMappingResponse>> GetMapping([Query] KvmMappingRequest request);
}
