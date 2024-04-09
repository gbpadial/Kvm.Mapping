namespace Kvm.Mapping.Application.Models;

public record KvmMappingRequest(
    string ValueFrom,
    string CodeType,
    string SystemTo,
    string SystemFrom,
    string CriteriaAttribute,
    string CriteriaOperation,
    string CriteriaValue);
