import { axios } from '@/utils/request'

/**
 * 获取流媒体列表
 *
 * @author langmansh
 */
export function mediaserverPage (parameter) {
  return axios({
    url: '/MediaServer/GetMediaServerList',
    method: 'get',
    params: parameter
  })
}

/**
 * 停止流媒体
 *
 * @author langmansh
 */
export function mediaserverStop (parameter) {
  return axios({
    url: '/AKStreamKeeper/ShutdownMediaServer',
    method: 'get',
    params: parameter
  })
}

/**
 * 启动流媒体
 *
 * @author langmansh
 */
export function mediaserverStart (parameter) {
  return axios({
    url: '/AKStreamKeeper/StartMediaServer',
    method: 'get',
    data: parameter
  })
}
