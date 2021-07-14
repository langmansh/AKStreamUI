using System;
using Furion.Extras.Admin.NET;

namespace Admin.NET.Application
{
    /// <summary>
    /// 设备管理输出参数
    /// </summary>
    public class videochannelsDto
    {
        /// <summary>
        /// 数据库主键
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 无人断流
        /// </summary>
        public System.UInt64 NoPlayerBreak { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public System.UInt64 Enabled { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 默认端口
        /// </summary>
        public System.UInt64 DefaultRtpPort { get; set; }
        
        /// <summary>
        /// rtsp地址
        /// </summary>
        public string VideoSrcUrl { get; set; }
        
        /// <summary>
        /// TCP协议
        /// </summary>
        public System.UInt64 RtpWithTcp { get; set; }
        
        /// <summary>
        /// 通道ID
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }
        
        /// <summary>
        /// 云台
        /// </summary>
        public System.UInt64 HasPtz { get; set; }
        
        /// <summary>
        /// ipv6地址
        /// </summary>
        public string IpV6Address { get; set; }
        
        /// <summary>
        /// ipv4地址
        /// </summary>
        public string IpV4Address { get; set; }
        
        /// <summary>
        /// 录制计划模板名称
        /// </summary>
        public string RecordPlanName { get; set; }
        
        /// <summary>
        /// 台账ID
        /// </summary>
        public string SpjkTZID { get; set; }
        
        /// <summary>
        /// 自动录制
        /// </summary>
        public System.UInt64 AutoRecord { get; set; }
        
        /// <summary>
        /// 设备类型
        /// </summary>
        public string VideoDeviceType { get; set; }
        
        /// <summary>
        /// 非rtp设备拉流方式
        /// </summary>
        public string MethodByGetStream { get; set; }
        
        /// <summary>
        /// 设备的流类型
        /// </summary>
        public string DeviceStreamType { get; set; }
        
        /// <summary>
        /// 设备的网络类型
        /// </summary>
        public string DeviceNetworkType { get; set; }
        
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string PDepartmentName { get; set; }
        
        /// <summary>
        /// 上级部门代码
        /// </summary>
        public string PDepartmentId { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// 部门代码
        /// </summary>
        public string DepartmentId { get; set; }
        
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        
        /// <summary>
        /// app
        /// </summary>
        public string App { get; set; }
        
        /// <summary>
        /// vhost
        /// </summary>
        public string Vhost { get; set; }
        
        /// <summary>
        /// 流媒体服务器ID
        /// </summary>
        public string MediaServerId { get; set; }
        
        /// <summary>
        /// 设备ID
        /// </summary>
        public string MainId { get; set; }
        
        /// <summary>
        /// 自动推拉流
        /// </summary>
        public System.UInt64 AutoVideo { get; set; }
        
        /// <summary>
        /// 录制时长（秒）
        /// </summary>
        public int RecordSecs { get; set; }
        
    }
}
