using Furion.Extras.Admin.NET;
using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application
{
    /// <summary>
    /// 设备管理输入参数
    /// </summary>
    public class videochannelsInput : PageInputBase
    {
        /// <summary>
        /// 无人断流
        /// </summary>
        public virtual System.UInt64 NoPlayerBreak { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual System.UInt64 Enabled { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 默认端口
        /// </summary>
        public virtual System.UInt64 DefaultRtpPort { get; set; }
        
        /// <summary>
        /// rtsp地址
        /// </summary>
        public virtual string VideoSrcUrl { get; set; }
        
        /// <summary>
        /// TCP协议
        /// </summary>
        public virtual System.UInt64 RtpWithTcp { get; set; }
        
        /// <summary>
        /// 通道ID
        /// </summary>
        public virtual string ChannelId { get; set; }
        
        /// <summary>
        /// 设备ID
        /// </summary>
        public virtual string DeviceId { get; set; }
        
        /// <summary>
        /// 云台
        /// </summary>
        public virtual System.UInt64 HasPtz { get; set; }
        
        /// <summary>
        /// ipv6地址
        /// </summary>
        public virtual string IpV6Address { get; set; }
        
        /// <summary>
        /// ipv4地址
        /// </summary>
        public virtual string IpV4Address { get; set; }
        
        /// <summary>
        /// 录制计划模板名称
        /// </summary>
        public virtual string RecordPlanName { get; set; }
        
        /// <summary>
        /// 自动录制
        /// </summary>
        public virtual System.UInt64 AutoRecord { get; set; }
        
        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual string VideoDeviceType { get; set; }
        
        /// <summary>
        /// 非rtp设备拉流方式
        /// </summary>
        public virtual string MethodByGetStream { get; set; }
        
        /// <summary>
        /// 设备的流类型
        /// </summary>
        public virtual string DeviceStreamType { get; set; }
        
        /// <summary>
        /// 设备的网络类型
        /// </summary>
        public virtual string DeviceNetworkType { get; set; }
        
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public virtual string PDepartmentName { get; set; }
        
        /// <summary>
        /// 上级部门代码
        /// </summary>
        public virtual string PDepartmentId { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DepartmentName { get; set; }
        
        /// <summary>
        /// 部门代码
        /// </summary>
        public virtual string DepartmentId { get; set; }
        
        /// <summary>
        /// 通道名称
        /// </summary>
        public virtual string ChannelName { get; set; }
        
        /// <summary>
        /// app
        /// </summary>
        public virtual string App { get; set; }
        
        /// <summary>
        /// vhost
        /// </summary>
        public virtual string Vhost { get; set; }
        
        /// <summary>
        /// 流媒体服务器ID
        /// </summary>
        public virtual string MediaServerId { get; set; }
        
        /// <summary>
        /// 设备ID
        /// </summary>
        public virtual string MainId { get; set; }
        
        /// <summary>
        /// 自动推拉流
        /// </summary>
        public virtual System.UInt64 AutoVideo { get; set; }
        
        /// <summary>
        /// 录制时长（秒）
        /// </summary>
        public virtual int RecordSecs { get; set; }
        
    }

    public class AddvideochannelsInput : videochannelsInput
    {
    }

    public class DeletevideochannelsInput
    {
        /// <summary>
        /// 数据库主键
        /// </summary>
        [Required(ErrorMessage = "数据库主键不能为空")]
        public long Id { get; set; }
        
    }

    public class UpdatevideochannelsInput : videochannelsInput
    {
        /// <summary>
        /// 数据库主键
        /// </summary>
        [Required(ErrorMessage = "数据库主键不能为空")]
        public long Id { get; set; }
        
    }

    public class QueryevideochannelsInput : DeletevideochannelsInput
    {

    }
}
