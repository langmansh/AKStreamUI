using System;
using System.ComponentModel.DataAnnotations;

namespace Furion.Extras.Admin.NET.Service
{
    public class BaseDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset? CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTimeOffset? UpdatedTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public virtual long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [MaxLength(20)]
        public virtual string CreatedUserName { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public virtual long? UpdatedUserId { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        [MaxLength(20)]
        public virtual string UpdatedUserName { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public virtual bool IsDeleted { get; set; } = false;
    }
}