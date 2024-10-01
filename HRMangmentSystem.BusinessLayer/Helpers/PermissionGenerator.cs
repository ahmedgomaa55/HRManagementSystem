using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.Helpers
{
    public static class PermissionGenerator
    {
        public static List<string> GeneratePermissions(string permissionName, bool create, bool read, bool update, bool delete)
        {
            List<string> permissions = new List<string>();

            if (create)
                permissions.Add($"{permissionName}.Create");
            if (read)
                permissions.Add($"{permissionName}.Read");
            if (update)
                permissions.Add($"{permissionName}.Update");
            if (delete)
                permissions.Add($"{permissionName}.Delete");

            return permissions;
        }
    }

}
