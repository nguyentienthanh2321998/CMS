using System.Collections.ObjectModel;

namespace FSH.WebApi.Shared.Authorization;
public static class FSHRoles
{
    public const string Admin = nameof(Admin);
    public const string CCMaker = nameof(CCMaker);
    public const string CCChecker = nameof(CCChecker);
    public const string CEMaker = nameof(CEMaker);
    public const string CEChecker = nameof(CEChecker);
    public const string Basic = nameof(Basic);


    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        CCMaker,
        CCChecker,
        CEMaker,
        CEChecker,
          Basic,

    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}