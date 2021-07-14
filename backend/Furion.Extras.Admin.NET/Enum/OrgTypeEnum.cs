using System.ComponentModel;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 机构类型
    /// </summary>
    public enum OrgTypeEnum
    {
        /// <summary>
        /// 品牌
        /// </summary>
        [Description("品牌")]
        BRAND = 1,

        /// <summary>
        /// 总店(加盟/直营)
        /// </summary>
        [Description("总店(加盟/直营)")]
        HEAD = 2,

        /// <summary>
        /// 直营店
        /// </summary>
        [Description("直营店")]
        DIRECT = 3,

        /// <summary>
        /// 加盟店
        /// </summary>
        [Description("加盟店")]
        FRANCHISED = 4
    }
}