namespace OmniSedeBackend.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequireRoleAttribute : Attribute
{
    public string Role { get; }
 
    public RequireRoleAttribute(string role)
    {
        Role = role;
    }
}