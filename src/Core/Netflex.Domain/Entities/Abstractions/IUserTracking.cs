namespace Netflex.Domain.Entities.Abstractions;

public interface IUserTracking
{
    string? CreatedBy { get; set; }
    string? LastModifiedBy { get; set; }
}