using System;
using Furion.Extras.Admin.NET;

namespace Admin.NET.Application
{
    /// <summary>
    /// 流媒体服务
    /// </summary>
    public class mediaserverDto
    {
        /// <summary>
        /// 流媒体ID
        /// </summary>
        public string mediaServerId { get; set; }
        /// <summary>
        /// ipv4
        /// </summary>
        public string ipV4Address { get; set; }
        /// <summary>
        /// ipv6
        /// </summary>
        public string ipV6Address { get; set; }
        /// <summary>
        /// keeper端口
        /// </summary>
        public int keeperPort { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string secret { get; set; }
        /// <summary>
        /// keeper服务状态
        /// </summary>
        public bool isKeeperRunning { get; set; }
        /// <summary>
        /// ZLM服务状态
        /// </summary>
        public bool isMediaServerRunning { get; set; }
        /// <summary>
        /// Http端口
        /// </summary>
        public int httpPort { get; set; }
    }
}
