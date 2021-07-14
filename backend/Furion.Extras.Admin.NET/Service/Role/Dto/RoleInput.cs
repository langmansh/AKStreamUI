using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 角色参数
    /// </summary>
    public class RoleInput
    {
        /// <summary>
        /// 角色类型-集团角色_0、加盟商角色_1、门店角色_2
        /// </summary>
        public RoleTypeEnum RoleType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

    public class RolePageInput : PageInputBase
    {
        /// <summary>
        /// 角色类型-集团角色_0、加盟商角色_1、门店角色_2
        /// </summary>
        [Required(ErrorMessage = "请选择角色类型")]
        public RoleTypeEnum RoleType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
    }

    public class AddRoleInput
    {
        /// <summary>
        /// 角色类型-集团角色_0、加盟商角色_1、门店角色_2
        /// </summary>
        [Required(ErrorMessage = "请选择角色类型")]
        public RoleTypeEnum RoleType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "角色编码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 数据范围类型（字典 1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public int DataScopeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class DeleteRoleInput : BaseId
    {
    }

    public class UpdateRoleInput
    {
        /// <summary>
        /// 角色类型-集团角色_0、加盟商角色_1、门店角色_2
        /// </summary>
        [Required(ErrorMessage = "请选择角色类型")]
        public RoleTypeEnum RoleType { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "角色编码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 数据范围类型（字典 1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public int DataScopeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class QueryRoleInput : BaseId
    {
    }

    public class GrantRoleMenuInput : IXnInputBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 数据范围类型（字典 1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public int DataScopeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<long> GrantMenuIdList { get; set; }
        public List<long> GrantRoleIdList { get; set; }
        public List<long> GrantOrgIdList { get; set; }
    }

    public class GrantRoleDataInput : IXnInputBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 数据范围类型（字典 1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public int DataScopeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<long> GrantMenuIdList { get; set; }
        public List<long> GrantRoleIdList { get; set; }
        public List<long> GrantOrgIdList { get; set; }
    }
}