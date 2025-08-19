using Nucleus.Models;
using Nucleus.IRepository;
using Nucleus.IWorkflow;

namespace Nucleus.Workflow;

public class ValidationWorkflow : IValidationWorkflow
{
    private readonly IValidationRepository validationRepository;

    public ValidationWorkflow(IValidationRepository validationRepository)
    {
        this.validationRepository = validationRepository;
    }


    public Task<IList<MessageElement>> Validate(DatbaseValidation datbaseValidation, CancellationToken cancellationToken)
    {
        return validationRepository.Validate(datbaseValidation, cancellationToken);
    }
}
