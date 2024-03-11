using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;
public static partial class Permissions
{
    [DisplayName("RoomType")]
    [Description("RoomType Permissions")]
    public static class RoomType
    {
        public const string View = "Permissions.RoomType.View";
        public const string Create = "Permissions.RoomType.Create";
        public const string Edit = "Permissions.RoomType.Edit";
        public const string Delete = "Permissions.RoomType.Delete";
        public const string Search = "Permissions.RoomType.Search";
        public const string Export = "Permissions.RoomType.Export";
        public const string Import = "Permissions.RoomType.Import";
    }
}
