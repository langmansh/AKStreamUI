import { axios } from '@/utils/request'

/**
 * 查询设备管理
 *
 * @author langmansh
 */
export function videochannelsPage (parameter) {
  return axios({
    url: '/videochannels/page',
    method: 'get',
    params: parameter
  })
}

/**
 * 设备管理列表
 *
 * @author langmansh
 */
export function videochannelsList (parameter) {
  return axios({
    url: '/videochannels/list',
    method: 'get',
    params: parameter
  })
}

/**
 * 添加设备管理
 *
 * @author langmansh
 */
export function videochannelsAdd (parameter) {
  return axios({
    url: '/videochannels/add',
    method: 'post',
    data: parameter
  })
}

/**
 * 编辑设备管理
 *
 * @author langmansh
 */
export function videochannelsEdit (parameter) {
  return axios({
    url: '/videochannels/edit',
    method: 'post',
    data: parameter
  })
}

/**
 * 删除设备管理
 *
 * @author langmansh
 */
export function videochannelsDelete (parameter) {
  return axios({
    url: '/videochannels/delete',
    method: 'post',
    data: parameter
  })
}

/**
 * 添加设备管理
 *
 * @author langmansh
 */
export function AddVideoChannel (parameter) {
  return axios({
    url: '/MediaServer/AddVideoChannel',
    method: 'post',
    data: parameter
  })
}
