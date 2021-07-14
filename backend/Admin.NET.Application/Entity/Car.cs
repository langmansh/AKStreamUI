using Furion.Extras.Admin.NET;
using Microsoft.EntityFrameworkCore;

namespace Admin.NET.Application.Entity
{
    /// <summary>
    /// 车辆信息（测试通用增删查改）
    /// </summary>
    [Comment("车辆信息")]
    public class Car : DEntityBase
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }
}