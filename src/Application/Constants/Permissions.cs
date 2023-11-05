﻿namespace Application.Constants;
public static class Permissions
{
    // These will generate all constant property value of particular module like Faculties
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>
            {
                 $"Permissions.{module}.Create",
                 $"Permissions.{module}.View",
                 $"Permissions.{module}.Edit",
                 $"Permissions.{module}.Delete"
            };
    }

    /// <summary>
    /// These will generate list of modules name (string) like ApplicationUsers, IdentityRoles
    /// </summary>
    /// <returns></returns>
    public static List<string> GetAllNestedModule()
    {
        Type permissionType = typeof(Permissions);
        Type[] nestedTypes = permissionType.GetNestedTypes();
        List<string> result = new List<string>();
        foreach (var type in nestedTypes)
        {
            result.Add(type.Name);
        }
        return result;
    }

    /// <summary>
    /// These will generate list of modules type (Type) like ApplicationUsers, IdentityRoles
    /// </summary>
    /// <returns></returns>
    public static Type[] GetAllNestedModuleType()
    {
        Type permissionType = typeof(Permissions);
        Type[] nestedTypes = permissionType.GetNestedTypes();
        return nestedTypes;
    }


    public static class ApplicationUsers
    {
        public const string View = "Permissions.ApplicationUsers.View";
        public const string Create = "Permissions.ApplicationUsers.Create";
        public const string Edit = "Permissions.ApplicationUsers.Edit";
        public const string Delete = "Permissions.ApplicationUsers.Delete";
    }
    public static class IdentityRoles
    {
        public const string View = "Permissions.IdentityRoles.View";
        public const string Create = "Permissions.IdentityRoles.Create";
        public const string Edit = "Permissions.IdentityRoles.Edit";
        public const string Delete = "Permissions.IdentityRoles.Delete";
    }

    public static class ManageUserRoles
    {
        public const string View = "Permissions.ManageUserRoles.View";
        public const string Create = "Permissions.ManageUserRoles.Create";
        public const string Edit = "Permissions.ManageUserRoles.Edit";
        public const string Delete = "Permissions.ManageUserRoles.Delete";
    }

    public static class ManageRoleClaims
    {
        public const string View = "Permissions.ManageRoleClaims.View";
        public const string Create = "Permissions.ManageRoleClaims.Create";
        public const string Edit = "Permissions.ManageRoleClaims.Edit";
        public const string Delete = "Permissions.ManageRoleClaims.Delete";
    }
}
