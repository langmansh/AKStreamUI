using System.ComponentModel;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 查询类型枚举
    /// </summary>
    public enum QueryTypeEnum
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equals = 0,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEquals = 1,

        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan = 2,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterThanOrEquals = 3,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan = 4,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessThanOrEquals = 5,

        /// <summary>
        /// 在列表中
        /// </summary>
        [Description("在列表中")]
        StdIn = 6,

        /// <summary>
        /// 不在列表中
        /// </summary>
        [Description("不在列表中")]
        StdNotIn = 7,

        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Contains = 8,

        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        NotContains = 9,

        /// <summary>
        /// 头部包含
        /// </summary>
        [Description("头部包含")]
        StartsWith = 10,

        /// <summary>
        /// 尾部包含
        /// </summary>
        [Description("尾部包含")]
        EndsWith = 11
    }
}