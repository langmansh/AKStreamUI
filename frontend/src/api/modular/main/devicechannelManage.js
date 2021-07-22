import { axios } from '@/utils/request'

/**
 * 获取流媒体列表
 *
 * @author langmansh
 */
export function devicechannelPage (parameter) {
  return axios({
    url: '/SipGate/GetSipDeviceList',
    method: 'get',
    params: parameter
  })
}

/**
 * 请求直播
 *
 * @author langmansh
 */
export function livevideoFun (parameter) {
  return axios({
    url: '/SipGate/LiveVideo',
    method: 'get',
    params: parameter
  })
}