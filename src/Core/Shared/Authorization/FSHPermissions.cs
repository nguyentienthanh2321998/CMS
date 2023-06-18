using System.Collections.ObjectModel;

namespace FSH.WebApi.Shared.Authorization;
public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Approval = nameof(Approval);
    public const string Cancel = nameof(Cancel);
    public const string ClientClosure = nameof(ClientClosure);
    public const string BankClousure = nameof(BankClousure);
    public const string RouteToCE = nameof(RouteToCE);
    public const string RouteToCC = nameof(RouteToCC);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
    public const string Basic = nameof(Basic);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard , IsRoot: true),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire , IsRoot: true),
        new("View Users", FSHAction.View, FSHResource.Users, IsRoot: true ),
        new("Search Users", FSHAction.Search, FSHResource.Users, IsRoot: true),
        new("Create Users", FSHAction.Create, FSHResource.Users, IsRoot: true),
        new("Update Users", FSHAction.Update, FSHResource.Users, IsRoot: true),
        new("Delete Users", FSHAction.Delete, FSHResource.Users, IsRoot: true),
        new("Export Users", FSHAction.Export, FSHResource.Users, IsRoot: true),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles, IsRoot: true),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles, IsRoot: true),
        new("View Roles", FSHAction.View, FSHResource.Roles, IsRoot: true),
        new("Create Roles", FSHAction.Create, FSHResource.Roles, IsRoot: true),
        new("Update Roles", FSHAction.Update, FSHResource.Roles, IsRoot: true),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles, IsRoot: true),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims, IsRoot: true),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims, IsRoot: true),


        new("View Products", FSHAction.View, FSHResource.Products, IsAll: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsAll: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Brands", FSHAction.View, FSHResource.Brands,IsCCChecker : true, IsRoot : true , IsCCMaker : true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsAll: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands,IsCCMaker  : true , IsCCChecker : true),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true)
    };
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(x => x.IsBasic).ToArray());

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> CCMaker { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsCCMaker).ToArray());
    public static IReadOnlyList<FSHPermission> CCChecker { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsCCChecker).ToArray());
    public static IReadOnlyList<FSHPermission> CEMaker { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsCEMaker).ToArray());
    public static IReadOnlyList<FSHPermission> CEChecker { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsCEChecker).ToArray());

}

public record FSHPermission(string Description, string Action, string Resource,
    bool IsBasic = false
    , bool IsAll = false
    , bool IsCEChecker = false
    , bool IsCEMaker = false
    , bool IsCCMaker = false
    , bool IsCCChecker = false
    , bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
