// -----------------------------------------------------------------------------
// Generate By Furion Tools v2.12.3                            
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Admin.NET.EntityFramework.Core;

namespace Admin.NET.EntityFramework.Core
{
    public partial class videochannels : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<videochannels, MasterDbContextLocator>
    {
    
        public long Id { get; set; }
        public string MainId { get; set; }
        public string MediaServerId { get; set; }
        public string Vhost { get; set; }
        public string App { get; set; }
        public string ChannelName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string PDepartmentId { get; set; }
        public string PDepartmentName { get; set; }
        public string DeviceNetworkType { get; set; }
        public string DeviceStreamType { get; set; }
        public string MethodByGetStream { get; set; }
        public string VideoDeviceType { get; set; }
        public ulong AutoVideo { get; set; }
        public ulong AutoRecord { get; set; }
        public string RecordPlanName { get; set; }
        public string IpV4Address { get; set; }
        public string IpV6Address { get; set; }
        public ulong HasPtz { get; set; }
        public string DeviceId { get; set; }
        public string ChannelId { get; set; }
        public ulong? RtpWithTcp { get; set; }
        public string VideoSrcUrl { get; set; }
        public ulong? DefaultRtpPort { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public ulong? Enabled { get; set; }
        public ulong? NoPlayerBreak { get; set; }
        public string SpjkTZID { get; set; }
        public int? RecordSecs { get; set; }
    
        public void Configure(EntityTypeBuilder<videochannels> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
                entityBuilder.HasComment("摄像头通道实例")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entityBuilder.HasIndex(e => e.ChannelName, "idx_vcs_chnn");

                entityBuilder.HasIndex(e => e.DepartmentId, "idx_vcs_dept");

                entityBuilder.HasIndex(e => e.Enabled, "idx_vcs_enbl");

                entityBuilder.HasIndex(e => e.IpV4Address, "idx_vcs_ipv4");

                entityBuilder.HasIndex(e => e.IpV6Address, "idx_vcs_ipv6");

                entityBuilder.HasIndex(e => e.MainId, "idx_vcs_maid")
                    .IsUnique();

                entityBuilder.HasIndex(e => e.MediaServerId, "idx_vcs_msid");

                entityBuilder.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasComment("数据库主键");

                entityBuilder.Property(e => e.App)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("app");

                entityBuilder.Property(e => e.AutoRecord)
                    .HasColumnType("bit(1)")
                    .HasComment("是否自动启用录制计划");

                entityBuilder.Property(e => e.AutoVideo)
                    .HasColumnType("bit(1)")
                    .HasComment("是否自动启用推拉流");

                entityBuilder.Property(e => e.ChannelId)
                    .HasMaxLength(20)
                    .HasComment("GB28181设备的SipChannel.DeviceId");

                entityBuilder.Property(e => e.ChannelName)
                    .HasMaxLength(64)
                    .HasComment("通道名称，整个系统唯一");

                entityBuilder.Property(e => e.CreateTime)
                    .HasColumnType("datetime(3)")
                    .HasComment("创建时间");

                entityBuilder.Property(e => e.DefaultRtpPort)
                    .HasColumnType("bit(1)")
                    .HasComment("Rtp设备是否使用流媒体默认rtp端口，如10000端口");

                entityBuilder.Property(e => e.DepartmentId)
                    .HasMaxLength(20)
                    .HasComment("部门代码");

                entityBuilder.Property(e => e.DepartmentName)
                    .HasMaxLength(64)
                    .HasComment("部门名称");

                entityBuilder.Property(e => e.DeviceId)
                    .HasMaxLength(20)
                    .HasComment("GB28181设备的SipDevice.DeviceId");

                entityBuilder.Property(e => e.DeviceNetworkType)
                    .HasMaxLength(255)
                    .HasComment("设备的网络类型");

                entityBuilder.Property(e => e.DeviceStreamType)
                    .HasMaxLength(255)
                    .HasComment("设备的流类型");

                entityBuilder.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasComment("是否启用");

                entityBuilder.Property(e => e.HasPtz)
                    .HasColumnType("bit(1)")
                    .HasComment("设备是否有云台控制");

                entityBuilder.Property(e => e.IpV4Address)
                    .HasMaxLength(16)
                    .HasComment("设备的ipv4地址");

                entityBuilder.Property(e => e.IpV6Address)
                    .HasMaxLength(64)
                    .HasComment("设备的ipv6地址");

                entityBuilder.Property(e => e.MainId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("设备的唯一ID");

                entityBuilder.Property(e => e.MediaServerId)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("流媒体服务器ID");

                entityBuilder.Property(e => e.MethodByGetStream)
                    .HasMaxLength(255)
                    .HasComment("使用哪种方式拉取非rtp设备的流");

                entityBuilder.Property(e => e.NoPlayerBreak)
                    .HasColumnType("bit(1)")
                    .HasComment("无人观察时断开流端口，此字段为true时AutoVideo字段必须为Flase\n            如果AutoVideo为true,则此字段无效");

                entityBuilder.Property(e => e.PDepartmentId)
                    .HasMaxLength(20)
                    .HasComment("上级部门代码");

                entityBuilder.Property(e => e.PDepartmentName)
                    .HasMaxLength(64)
                    .HasComment("上级部门名称");

                entityBuilder.Property(e => e.RecordPlanName)
                    .HasMaxLength(255)
                    .HasComment("录制计划模板名称");

                entityBuilder.Property(e => e.RecordSecs)
                    .HasColumnType("int(11)")
                    .HasComment("录制时长（秒）");

                entityBuilder.Property(e => e.RtpWithTcp)
                    .HasColumnType("bit(1)")
                    .HasComment("Rtp设备是否使用Tcp推流");

                entityBuilder.Property(e => e.SpjkTZID)
                    .HasMaxLength(255)
                    .HasComment("台账ID");

                entityBuilder.Property(e => e.UpdateTime)
                    .HasColumnType("datetime(3)")
                    .HasComment("更新时间");

                entityBuilder.Property(e => e.Vhost)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("vhost");

                entityBuilder.Property(e => e.VideoDeviceType)
                    .HasMaxLength(255)
                    .HasComment("设备类型，IPC,NVR,DVR");

                entityBuilder.Property(e => e.VideoSrcUrl)
                    .HasMaxLength(255)
                    .HasComment("非Rtp设备的视频流源地址");
        }

    }
}
