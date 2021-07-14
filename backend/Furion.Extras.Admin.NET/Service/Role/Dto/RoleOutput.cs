namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 登录用户角色参数
    /// </summary>
    public class RoleOutput
    {
        /// <summary>
        /// 角色类型-集团角色_0、加盟商角色_1、门店角色_2
        /// </summary>
        public RoleTypeEnum RoleType { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}