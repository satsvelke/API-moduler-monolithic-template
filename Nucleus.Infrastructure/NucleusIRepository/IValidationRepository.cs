using Nucleus.Models;

namespace Nucleus.IRepository;

public interface IValidationRepository
{
    Task<IList<MessageElement>> Validate(DatbaseValidation datbaseValidation, CancellationToken cancellationToken);
}
