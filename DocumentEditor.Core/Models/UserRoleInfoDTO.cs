namespace DocumentEditor.Core.Models;

public class UserRoleInfoDTO
{
    public Guid UserId { get;} 
    public string Username { get;} 
    public string RoleName { get;} 
    public Guid UserRoleId { get;} 
    public UserRoleInfoDTO(Guid userId, string username, string roleName, Guid userRoleId)
    {
        UserId = userId;
        Username = username;
        RoleName = roleName;
        UserRoleId = userRoleId;
    }
}
