using Furion.Extras.Admin.NET;
using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Admin.NET.EntityFramework.Core;
using Microsoft.Extensions.Configuration;
using Furion.RemoteRequest.Extensions;
using System;

namespace Admin.NET.Application
{
    /// <summary>
    /// 设备管理服务
    /// </summary>
    [ApiDescriptionSettings("流媒体管理", Name = "videochannels", Order = 100)]
    public class videochannelsService : IvideochannelsService, IDynamicApiController, ITransient
    {
        private readonly IRepository<videochannels> _rep;
        private readonly IConfiguration _configuration;

        public videochannelsService(
            IRepository<videochannels> rep,
            IConfiguration configuration
        )
        {
            _rep = rep;
            _configuration = configuration;
        }

        /// <summary>
        /// 分页查询设备管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/videochannels/page")]
        public async Task<dynamic> Page([FromQuery] videochannelsInput input)
        {
            var entities = await _rep.DetachedEntities
                                     .Where(!string.IsNullOrEmpty(input.ChannelId), u => u.ChannelId == input.ChannelId)
                                     .Where(!string.IsNullOrEmpty(input.DeviceId), u => u.DeviceId == input.DeviceId)
                                     .Where(!string.IsNullOrEmpty(input.SpjkTZID), u => u.SpjkTZID == input.SpjkTZID)
                                     .Where(!string.IsNullOrEmpty(input.VideoDeviceType), u => u.VideoDeviceType == input.VideoDeviceType)
                                     .Where(!string.IsNullOrEmpty(input.ChannelName), u => u.ChannelName == input.ChannelName)
                                     .Where(!string.IsNullOrEmpty(input.MainId), u => u.MainId == input.MainId)
                                     .OrderBy(PageInputOrder.OrderBuilder<videochannelsInput>(input))
                                     .ToPagedListAsync(input.PageNo, input.PageSize);
            var result = XnPageResult<videochannels>.PageResult<videochannelsDto>(entities);
            await DtoMapper(result.Rows);
            return result;
        }

        /// <summary>
        /// 增加设备管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/videochannels/add")]
        public async Task Add(AddvideochannelsInput input)
        {
            var entity = input.Adapt<videochannels>();
            await entity.InsertAsync();
        }

        /// <summary>
        /// 删除设备管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/videochannels/delete")]
        public async Task Delete(DeletevideochannelsInput input)
        {
            var entity = await _rep.FirstOrDefaultAsync(u => u.Id == input.Id);
            await entity.DeleteAsync();
        }

        /// <summary>
        /// 更新设备管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/videochannels/edit")]
        public async Task Update(UpdatevideochannelsInput input)
        {
            var entity = input.Adapt<videochannels>();
            await entity.UpdateAsync(true);
        }

        /// <summary>
        /// 获取设备管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/videochannels/detail")]
        public async Task<videochannels> Get([FromQuery] QueryevideochannelsInput input)
        {
            return await _rep.DetachedEntities.FirstOrDefaultAsync(u => u.Id == input.Id);
        }

        /// <summary>
        /// 获取设备管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/videochannels/list")]
        public async Task<dynamic> List([FromQuery] videochannelsInput input)
        {
            return await _rep.DetachedEntities.ToListAsync();
        }    

        private async Task DtoMapper(ICollection<videochannelsDto> rows)
        {
            foreach (var item in rows)
            {
            }
        }

        #region AKStreamWeb Api
        /// <summary>
        /// 获取流媒体服务器列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/MediaServer/GetMediaServerList")]
        public async Task<dynamic> GetMediaServerList()
        {
            string apiUrl = _configuration["AKStream:ApiUrl"];
            var result = await $"{apiUrl}/MediaServer/GetMediaServerList".SetHeaders(new
            {
                AccessKey = _configuration["AKStream:AccessKey"]
            }).OnException((res, errors) => {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            }).GetAsAsync<List<mediaserverDto>>();

            var strJson = new
            {
                PageNo = 1,
                PageSize =10,
                Rows = result,
                TotalPage = Math.Ceiling(decimal.Parse((result.Count / 10).ToString())),
                TotalRows = result.Count
            };

            return strJson;
        }
        /// <summary>
        /// 启动流媒体服务
        /// </summary>
        /// <param name="mServerId">流媒体服务ID</param>
        /// <returns></returns>
        [HttpGet("/AKStreamKeeper/StartMediaServer")]
        public async Task StartMediaServer(string mServerId)
        {
            string apiUrl = _configuration["AKStream:ApiUrl"];
            var result = await $"{apiUrl}/AKStreamKeeper/StartMediaServer".SetHeaders(new
            {
                AccessKey = _configuration["AKStream:AccessKey"]
            }).SetQueries(new
            {
                mediaServerId = mServerId
            }).OnException((res, errors) => {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            }).GetAsync();
        }
        /// <summary>
        /// 停止流媒体服务
        /// </summary>
        /// <param name="mServerId">流媒体服务ID</param>
        /// <returns></returns>
        [HttpGet("/AKStreamKeeper/ShutdownMediaServer")]
        public async Task ShutdownMediaServer(string mServerId)
        {
            string apiUrl = _configuration["AKStream:ApiUrl"];
            var result = await $"{apiUrl}/AKStreamKeeper/ShutdownMediaServer".SetHeaders(new
            {
                AccessKey = _configuration["AKStream:AccessKey"]
            }).SetQueries(new
            {
                mediaServerId = mServerId
            }).OnException((res, errors) => {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            }).GetAsync();
        }
        /// <summary>
        /// 获取Sip设备列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/SipGate/GetSipDeviceList")]
        public async Task<dynamic> GetSipDeviceList([FromQuery] devicechannelDto input)
        {
            string apiUrl = _configuration["AKStream:ApiUrl"];
            var result = await $"{apiUrl}/SipGate/GetSipDeviceList".SetHeaders(new
            {
                AccessKey = _configuration["AKStream:AccessKey"]
            }).OnException((res, errors) => {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            }).GetAsAsync<List<devicechannelDto>>();

            var strJson = new
            {
                PageNo = input.PageNo,
                PageSize = 10,
                Rows = result,
                TotalPage = Math.Ceiling(decimal.Parse((result.Count / 10).ToString())),
                TotalRows = result.Count
            };

            return strJson;
        }
        /// <summary>
        /// 获取Sip设备列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/SipGate/LiveVideo")]
        public async Task<dynamic> GetSipDeviceList(string deviceId,string channelId)
        {
            string apiUrl = _configuration["AKStream:ApiUrl"];
            var result = await $"{apiUrl}/SipGate/LiveVideo".SetHeaders(new
            {
                AccessKey = _configuration["AKStream:AccessKey"]
            }).SetQueries(new
            {
                deviceId = deviceId,
                channelId = channelId
            }).OnException((res, errors) => {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            }).GetAsAsync<resPlayUrlDto>();

            return result;
        }
        #endregion

    }
}
