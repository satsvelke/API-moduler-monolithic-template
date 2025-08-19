using Auth.Dtos;
using Auth.IWorkflow;
using Auth.Service.CoreControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nucleus.Api;
using Nucleus.Models;

namespace Auth.Service;

public class AuthController : CoreController
{

    private readonly IUserWorkflow userWorkflow;
    private readonly IOptions<MessageHeader> messageOptions;

    public AuthController(IUserWorkflow userWorkflow, IOptions<MessageHeader> messageOptions)
    {
        this.userWorkflow = userWorkflow;
        this.messageOptions = messageOptions;
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequestDto, CancellationToken cancellationToken)
    {
        var user = await userWorkflow.Verify(userLoginRequestDto, cancellationToken).ConfigureAwait(true);

        return user is not null
        ? user.ToOk(messageOptions, HttpContext, "AuthX101")
        : user.ToBadRequest(messageOptions, HttpContext, "AuthX102");
    }
}
