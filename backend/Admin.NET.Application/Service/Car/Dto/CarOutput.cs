using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Filters;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;

namespace Admin.NET.Application.Dto
{
    //TDetailDto, TPageListDto, TExportDto, TPrintDto
    public class CarDetail
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }

    public class CarPageList
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }

    /// <summary>
    /// 导出支持各种配置
    /// </summary>
    [ExcelExporter(Name = "车辆资料", TableStyle = TableStyles.Light10, ExporterHeaderFilter = typeof(CarExport))]
    public class CarExport : IExporterHeaderFilter
    {
        [ExporterHeader(DisplayName = "车名", IsBold = true, IsIgnore = true)]
        public string CarName { get; set; }

        [ExporterHeader("车牌号")]
        public string CarNo { get; set; }

        [ExporterHeader("已删除")]
        [ValueMapping(text: "是", true)]
        [ValueMapping(text: "否", false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 表头筛选器
        /// </summary>
        /// <param name="exporterHeaderInfo"></param>
        /// <returns></returns>
        public ExporterHeaderInfo Filter(ExporterHeaderInfo exporterHeaderInfo)
        {
            // 修改显示名称
            if (exporterHeaderInfo.DisplayName.Equals("车名"))
                exporterHeaderInfo.DisplayName = "禁止开车";

            // 忽略的改为不忽略
            if (exporterHeaderInfo.ExporterHeaderAttribute.IsIgnore)
                exporterHeaderInfo.ExporterHeaderAttribute.IsIgnore = false;

            return exporterHeaderInfo;
        }
    }

    public class CarPrint
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }
}