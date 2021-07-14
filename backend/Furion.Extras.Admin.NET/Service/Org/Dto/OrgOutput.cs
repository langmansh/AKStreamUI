namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 组织机构参数
    /// </summary>
    public class OrgOutput
    {
        /// <summary>
        /// 机构类型-品牌_1、总店(加盟/直营)_2、直营店_3、加盟店_4
        /// </summary>
        public OrgTypeEnum OrgType { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 父Ids
        /// </summary>
        public string Pids { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态（字典 0正常 1停用 2删除）
        /// </summary>
        public int Status { get; set; }
    }
}