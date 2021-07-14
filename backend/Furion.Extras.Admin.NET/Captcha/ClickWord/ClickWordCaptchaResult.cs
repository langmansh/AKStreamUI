using Furion.DependencyInjection;
using System.Collections.Generic;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 验证码输出参数
    /// </summary>
    public class ClickWordCaptchaResult
    {
        public string RepCode { get; set; } = "0000";

        public string RepMsg { get; set; }

        public RepData RepData { get; set; } = new RepData();

        public bool Error { get; set; }

        public bool Success { get; set; } = true;
    }

    public class RepData
    {
        public string CaptchaId { get; set; }

        public string ProjectCode { get; set; }

        public string CaptchaType { get; set; }

        public string CaptchaOriginalPath { get; set; }

        public string CaptchaFontType { get; set; }

        public string CaptchaFontSize { get; set; }

        public string SecretKey { get; set; }

        public string OriginalImageBase64 { get; set; }

        public List<PointPosModel> Point { get; set; } = new List<PointPosModel>();

        public string JigsawImageBase64 { get; set; }
        public List<string> WordList { get; set; } = new List<string>();

        public string PointList { get; set; }

        public string PointJson { get; set; }

        public string Token { get; set; }

        public bool Result { get; set; }

        public string CaptchaVerification { get; set; }
    }
}