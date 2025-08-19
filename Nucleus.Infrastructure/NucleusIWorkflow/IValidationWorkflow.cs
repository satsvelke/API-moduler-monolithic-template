using Nucleus.Models;

namespace Nucleus.IWorkflow;

public interface IValidationWorkflow
{
    Task<IList<MessageElement>> Validate(DatbaseValidation datbaseValidation, CancellationToken cancellationToken);
}
