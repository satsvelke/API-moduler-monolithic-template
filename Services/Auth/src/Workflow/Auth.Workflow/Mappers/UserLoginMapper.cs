using Auth.Dtos;
using Auth.Models;
using Riok.Mapperly.Abstractions;

namespace Auth.Workflow.Mappers;


[Mapper]
public sealed partial class UserLoginMapper
{
    public partial User ToUser(UserLoginRequestDto userLoginRequestDto);
}
