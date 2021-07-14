using System.ComponentModel;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 角色类型
    /// </summary>
    public enum RoleTypeEnum
    {
        /// <summary>
        /// 集团角色
        /// </summary>
        [Description("集团角色")]
        GROUP = 1,

        /// <summary>
        /// 加盟商角色
        /// </summary>
        [Description("加盟商角色")]
        JOIN = 2,

        /// <summary>
        /// 门店角色
        /// </summary>
        [Description("门店角色")]
        STORE = 3
    }
}