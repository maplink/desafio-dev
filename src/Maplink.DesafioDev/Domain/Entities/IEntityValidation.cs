using FluentValidation.Results;

namespace Maplink.DesafioDev.Domain.Entities
{
    public interface IEntityValidation
    {
        ValidationResult Validate();
    }
}