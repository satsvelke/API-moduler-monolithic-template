
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Api.Filters;

namespace Auth.Service.CoreControllers;


[Route("api/v1/Auth/[controller]/[action]")]
[ApiController]
[ServiceFilter(typeof(AuthenticationAttribute))]
[ModelValidatorAttribute]
public class CoreController : ControllerBase
{

}
