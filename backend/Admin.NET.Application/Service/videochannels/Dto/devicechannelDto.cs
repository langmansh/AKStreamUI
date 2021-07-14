using System;
using Furion.Extras.Admin.NET;

namespace Admin.NET.Application
{
    /// <summary>
    /// Sip设备
    /// </summary>
    public class devicechannelDto: PageInputBase
    {
        public string ipAddress { get; set; }
        public string deviceId { get; set; }
        public int port { get; set; }
        public Sipchannel[] sipChannels { get; set; }
        public Deviceinfo deviceInfo { get; set; }
        public string registerTime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string keepAliveTime { get; set; }
        public int keepAliveLostTime { get; set; }
        public Devicestatus deviceStatus { get; set; }
        public bool isReday { get; set; }
    }

    public class Deviceinfo
    {
        public int cmdType { get; set; }
        public int sn { get; set; }
        public string deviceID { get; set; }
        public string result { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string firmware { get; set; }
        public int channel { get; set; }
    }

    public class Devicestatus
    {
        public string cmdType { get; set; }
        public int sn { get; set; }
        public string deviceID { get; set; }
        public string result { get; set; }
        public string online { get; set; }
        public string status { get; set; }
        public string encode { get; set; }
        public string record { get; set; }
        public string deviceTime { get; set; }
        public Alarmstatus alarmstatus { get; set; }
    }

    public class Alarmstatus
    {
        public object[] items { get; set; }
    }

    public class Sipchannel
    {
        public string ssrcId { get; set; }
        public string stream { get; set; }
        public string parentId { get; set; }
        public string deviceId { get; set; }
        public string pushStatus { get; set; }
        public string lastUpdateTime { get; set; }
        public string sipChannelType { get; set; }
        public string sipChannelStatus { get; set; }
        public Sipchanneldesc sipChannelDesc { get; set; }
    }

    public class Sipchanneldesc
    {
        public string channelID { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string owner { get; set; }
        public string civilCode { get; set; }
        public string address { get; set; }
        public int parental { get; set; }
        public string parentalValue { get; set; }
        public int safetyWay { get; set; }
        public string safetyWayValue { get; set; }
        public int registerWay { get; set; }
        public string registerWayValue { get; set; }
        public int secrecy { get; set; }
        public string secrecyValue { get; set; }
        public string status { get; set; }
    }

}
