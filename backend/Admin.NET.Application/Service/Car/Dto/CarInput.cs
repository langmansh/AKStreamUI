using Furion.Extras.Admin.NET;
using Furion.Extras.Admin.NET.Service;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Filters;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application.Dto
{
    //TSearchDto, TAddDto, TDelDto, TUpdateDto, TImportDto
    public class CarSeaarch : PageInputBase
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }

    public class CarAdd
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }

    public class CarUpdate : BaseDto
    {
        public string CarName { get; set; }
        public string CarNo { get; set; }
    }

    /// <summary>
    /// 导入支持各种配置
    /// </summary>
    [ExcelImporter(MaxCount = 500, ImportHeaderFilter = typeof(CarImport), ImportResultFilter = typeof(CarImport))]
    public class CarImport : IImportResultFilter, IImportHeaderFilter
    {
        [ImporterHeader(Name = "车辆名称", Description = "必须填", IsInterValidation = true)]
        [Required(ErrorMessage = "此项必填")]
        public string CarName { get; set; }

        [ImporterHeader(Name = "车牌", Description = "必须填，只能是数字，小于10位数", IsInterValidation = true)]
        [MaxLength(10, ErrorMessage = "不能大于10位数")]
        [RegularExpression(@"^\d*$", ErrorMessage = "只能是数字")]
        public string CarNo { get; set; }

        /// <summary>
        /// bool和枚举会自动生成下拉选择框，也可自动定义文本内容
        /// </summary>
        [ImporterHeader(Name = "已删除")]
        [ValueMapping(text: "删了", true)]
        [ValueMapping(text: "没删", false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 导入结果筛选器
        /// </summary>
        /// <param name="importResult"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ImportResult<T> Filter<T>(ImportResult<T> importResult) where T : class, new()
        {
            // 可以做自定义校验
            return importResult;
        }

        /// <summary>
        /// 导入列头筛选器
        /// </summary>
        /// <param name="importerHeaderInfos"></param>
        /// <returns></returns>
        public List<ImporterHeaderInfo> Filter(List<ImporterHeaderInfo> importerHeaderInfos)
        {
            foreach (var item in importerHeaderInfos)
            {
                // 支持调整显示名称
                if (item.PropertyName == "Name")
                {
                    item.Header.Name = "Student";
                }
                // 支持绑定值映射
                else if (item.PropertyName == "Gender")
                {
                    item.MappingValues = new Dictionary<string, dynamic>() { { "男", 0 }, { "女", 1 } };
                }
            }

            return importerHeaderInfos;
        }
    }
}