using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;
public static partial class Permissions
{
    [DisplayName("Rooms")]
    [Description("Rooms Permissions")]
    public static class Rooms
    {
        public const string View = "Permissions.Rooms.View";
        public const string Create = "Permissions.Rooms.Create";
        public const string Edit = "Permissions.Rooms.Edit";
        public const string Delete = "Permissions.Rooms.Delete";
        public const string Search = "Permissions.Rooms.Search";
        public const string Export = "Permissions.Rooms.Export";
        public const string Import = "Permissions.Rooms.Import";
    }
}
