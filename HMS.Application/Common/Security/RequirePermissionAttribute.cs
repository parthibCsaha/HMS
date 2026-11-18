namespace HMS.Application.Common.Security;

/// <summary>
/// Marks a MediatR request (command / query) with the permission it requires.
/// Checked automatically by <c>PermissionBehavior</c> in the pipeline —
/// no controller-level role attributes needed.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class RequirePermissionAttribute : Attribute
{
    public string Permission { get; }

    public RequirePermissionAttribute(string permission)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(permission);
        Permission = permission;
    }
}

