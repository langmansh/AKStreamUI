using Furion.ClayObject.Extensions;
using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 通用方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TSearchDto"></typeparam>
    /// <typeparam name="TAddDto"></typeparam>
    /// <typeparam name="TUpdateDto"></typeparam>
    /// <typeparam name="TImportDto"></typeparam>
    /// <typeparam name="TDetailDto"></typeparam>
    /// <typeparam name="TPageListDto"></typeparam>
    /// <typeparam name="TExportDto"></typeparam>
    /// <typeparam name="TPrintDto"></typeparam>
    public class BaseService<TEntity, TSearchDto, TAddDto, TUpdateDto, TImportDto, TDetailDto, TPageListDto, TExportDto, TPrintDto>
        where TEntity : DEntityBase, new()
        where TUpdateDto : BaseDto
        where TSearchDto : PageInputBase
        where TPageListDto : new()
        where TExportDto : class, new()
        where TImportDto : class, new()
    {
        /// <summary>
        /// 数据仓储
        /// </summary>
        protected readonly IRepository<TEntity> Repository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        #region 查询/分页查询

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDetailDto> Get(long id)
        {
            var entity = await Repository.DetachedEntities.FirstOrDefaultAsync(e => e.Id == id);
            return entity.Adapt<TDetailDto>();
        }

        /// <summary>
        /// 分页搜索前
        /// </summary>
        protected Func<TSearchDto, Expression<Func<TEntity, bool>>> SearchExpression = null;

        /// <summary>
        /// 自定义分页搜索（复杂查询）
        /// </summary>
        protected Func<TSearchDto, IQueryable<TEntity>> SearchQueryable = null;

        /// <summary>
        /// 分页数据返回前处理
        /// </summary>
        /// <returns></returns>
        protected Action<PagedList<TEntity>> PageListHandle = null;

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("page")]
        public virtual async Task<PageResult<TPageListDto>> PageList(TSearchDto searchDto)
        {
            IQueryable<TEntity> queryable;
            if (SearchQueryable != null)
            {
                // 通过派生类中定义的委托方法自定义查询条件
                queryable = SearchQueryable(searchDto);
            }
            else
            {
                // 动态构建查询条件
                GetSearchParameters(searchDto);
                queryable = Repository.DetachedEntities.Search(searchDto);

                // 有自定义的查询条件
                if (SearchExpression != null)
                    queryable = queryable.Where(SearchExpression(searchDto));
            }

            PagedList<TEntity> pageList = await queryable.ToPagedListAsync(searchDto.PageNo, searchDto.PageSize);

            PageListHandle?.Invoke(pageList);

            return XnPageResult<TEntity>.PageResult<TPageListDto>(pageList);
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增前验证或处理
        /// </summary>
        protected Action<TAddDto> BeforeAddAction = null;

        /// <summary>
        /// 新增后处理
        /// </summary>
        protected Action<TEntity> AfterAddAction = null;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="addDto"></param>
        public virtual async Task Add(TAddDto addDto)
        {
            // 新增前操作
            BeforeAddAction?.Invoke(addDto);

            // 写数据
            var entity = await addDto.Adapt<TEntity>().InsertAsync();

            // 新增后操作
            AfterAddAction?.Invoke(entity.Entity);
        }

        #endregion

        #region 删除/假删除

        /// <summary>
        /// 删除前验证或处理
        /// </summary>
        protected Action<List<long>> BeforeDeleteAction = null;

        /// <summary>
        /// 删除后处理
        /// </summary>
        protected Action<List<long>, int> AfterDeleteAction = null;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public virtual async Task Delete(List<long> ids)
        {
            BeforeDeleteAction?.Invoke(ids);

            var count = await Repository.Context.DeleteRangeAsync<TEntity>(x => ids.Contains(x.Id));

            AfterDeleteAction?.Invoke(ids, count);
        }

        /// <summary>
        /// 假删除前验证或处理
        /// </summary>
        protected Action<List<long>> BeforeFakeDeleteAction = null;

        /// <summary>
        /// 假删除后处理
        /// </summary>
        protected Action<List<long>, int> AfterFakeDeleteAction = null;

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="ids"></param>
        [HttpDelete("fakeDelete")]
        public virtual async Task FakeDelete(List<long> ids)
        {
            BeforeFakeDeleteAction?.Invoke(ids);

            var count = await Repository.Context.BatchUpdate<TEntity>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => ids.Contains(x.Id))
                .ExecuteAsync();

            AfterFakeDeleteAction?.Invoke(ids, count);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 更新前验证或处理
        /// </summary>
        protected Action<TUpdateDto> BeforeUpdateAction = null;

        /// <summary>
        /// 更新后处理
        /// </summary>
        protected Action<TEntity> AfterUpdateAction = null;

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="updateDto"></param>
        [HttpPut("edit")]
        public virtual async Task Update(TUpdateDto updateDto)
        {
            BeforeUpdateAction?.Invoke(updateDto);
            var entity = await updateDto.Adapt<TEntity>().UpdateAsync(true);
            AfterUpdateAction?.Invoke(entity.Entity);
        }

        #endregion

        #region 导入

        /// <summary>
        /// 导入模版下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        public virtual async Task<FileContentResult> ImportTemplate()
        {
            // 创建Excel导入对象
            IImporter importer = new ExcelImporter();
            var byteArray = await importer.GenerateTemplateBytes<TImportDto>();

            // 文件名称
            var fileName = typeof(TEntity).GetDescriptionValue<CommentAttribute>() + "导入模版.xlsx";

            return await Task.FromResult(
                new FileContentResult(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                });
        }

        /// <summary>
        /// 导入前验证或处理
        /// </summary>
        protected Action<IEnumerable<TImportDto>> BeforeImportAction = null;

        /// <summary>
        /// 导入后处理
        /// </summary>
        protected Action<IEnumerable<TImportDto>> AfterImportAction = null;

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <exception cref="Exception"></exception>
        [UnitOfWork]
        public virtual async Task Import(IFormFile file)
        {
            var path = Path.Combine(Path.GetTempPath(), $"{YitIdHelper.NextId()}.xlsx");
            await using (var stream = File.Create(path))
            {
                await file.CopyToAsync(stream);
            }

            // 创建Excel导入对象
            IImporter importer = new ExcelImporter();
            var import = await importer.Import<TImportDto>(path);

            if (import == null)
                throw Oops.Oh("导入模版解析异常");
            if (import.Exception != null)
                throw Oops.Oh("导入异常:" + import.Exception);
            if (import.RowErrors.Count > 0)
                throw Oops.Oh("数据校验:" + JSON.Serialize(import.RowErrors));

            BeforeImportAction?.Invoke(import.Data);

            await Repository.InsertAsync(import.Data.Adapt<ICollection<TEntity>>());

            AfterImportAction?.Invoke(import.Data);
        }

        #endregion

        #region 导出

        /// <summary>
        /// 导出搜索前
        /// </summary>
        protected Func<TSearchDto, Expression<Func<TEntity, bool>>> ExportSearchExpression = null;

        /// <summary>
        /// 自定义导出搜索（复杂查询）
        /// </summary>
        protected Func<TSearchDto, IQueryable<TEntity>> ExportSearchQueryable = null;

        /// <summary>
        /// 导出数据返回前处理
        /// </summary>
        /// <returns></returns>
        protected Action<PagedList<TEntity>> ExportHandle = null;

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [HttpGet("export")]
        public virtual async Task<FileContentResult> Export(TSearchDto searchDto)
        {
            IQueryable<TEntity> queryable;
            if (ExportSearchQueryable != null)
            {
                // 通过派生类中定义的委托方法自定义查询条件
                queryable = ExportSearchQueryable(searchDto);
            }
            else
            {
                // 动态构建查询条件
                GetSearchParameters(searchDto);
                queryable = Repository.DetachedEntities.Search(searchDto);

                // 有自定义的查询条件
                if (ExportSearchExpression != null)
                    queryable = queryable.Where(ExportSearchExpression(searchDto));
            }

            var entitys = await queryable.ToListAsync();

            // 创建Excel导出对象
            IExporter exporter = new ExcelExporter();

            // 导出文件
            var byteArray = await exporter.ExportAsByteArray(entitys.Adapt<List<TExportDto>>());

            // 文件名称
            var fileName = typeof(TEntity).GetDescriptionValue<CommentAttribute>() + DateTime.Now.ToString("yyyyMMddHHssmm") + ".xlsx";

            return await Task.FromResult(
                new FileContentResult(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                });
        }

        #endregion

        #region 打印

        /// <summary>
        /// 获取打印数据 todo: 最简单的主键查询单条数据，后续实现单据打印模版模块
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("print")]
        public virtual async Task<TPrintDto> Print(long id)
        {
            var entity = await Repository.DetachedEntities.FirstOrDefaultAsync(e => e.Id == id);
            return entity.Adapt<TPrintDto>();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 将查询dto组装成搜索参数
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        private void GetSearchParameters(TSearchDto searchDto)
        {
            // 如果没有复杂查询条件，把自定义查询dto中有内容的项加入查询条件
            if (searchDto.SearchParameters != null && searchDto.SearchParameters.Any()) return;

            // 查询dto转为字典
            var searchDictionary = searchDto.ToDictionary();

            // 取实体中的字段名称
            var entityPropertieNames =
                typeof(TEntity).GetProperties().Select(x => x.Name).ToList();

            // 将searchDto中有值的属性加入复杂查询条件
            foreach (var keyValuePair in searchDictionary)
            {
                // 跳过自定义属性和空值属性，自定义属性可在查询前委托中定义查询条件
                if (!entityPropertieNames.Contains(keyValuePair.Key) || keyValuePair.Value == null) continue;
                searchDto.SearchParameters.Add(new Condition()
                {
                    Field = keyValuePair.Key,
                    Op = QueryTypeEnum.Equals,
                    Value = keyValuePair.Value
                });
            }
        }

        #endregion
    }
}