<template>
  <a-modal
    title="编辑设备管理"
    :width="900"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleSubmit"
    @cancel="handleCancel">
    <a-spin :spinning="confirmLoading">
      <a-form :form="form">
        <a-form-item v-show="false"><a-input v-decorator="['id']" /></a-form-item>
        <a-form-item label="无人断流" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['noPlayerBreak', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="是否启用" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['enabled', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="默认端口" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['defaultRtpPort', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="rtsp地址" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入rtsp地址" v-decorator="['videoSrcUrl']" />
        </a-form-item>
        <a-form-item label="TCP协议" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['rtpWithTcp', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="通道ID" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入通道ID" v-decorator="['channelId']" />
        </a-form-item>
        <a-form-item label="设备ID" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入设备ID" v-decorator="['deviceId']" />
        </a-form-item>
        <a-form-item label="云台" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['hasPtz', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="ipv6地址" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入ipv6地址" v-decorator="['ipV6Address']" />
        </a-form-item>
        <a-form-item label="ipv4地址" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入ipv4地址" v-decorator="['ipV4Address']" />
        </a-form-item>
        <a-form-item label="录制计划模板名称" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入录制计划模板名称" v-decorator="['recordPlanName']" />
        </a-form-item>
        <a-form-item label="台账ID" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入台账ID" v-decorator="['spjkTZID']" />
        </a-form-item>
        <a-form-item label="自动录制" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['autoRecord', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="设备类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select style="width: 100%" placeholder="请选择设备类型" v-decorator="['videoDeviceType', {rules: [{ required: true, message: '请选择设备类型！' }]}]">
            <a-select-option v-for="(item,index) in videoDeviceTypeData" :key="index" :value="item.code">{{ item.name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="非rtp设备拉流方式" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select style="width: 100%" placeholder="请选择非rtp设备拉流方式" v-decorator="['methodByGetStream', {rules: [{ required: true, message: '请选择非rtp设备拉流方式！' }]}]">
            <a-select-option v-for="(item,index) in methodByGetStreamData" :key="index" :value="item.code">{{ item.name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="设备的流类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select style="width: 100%" placeholder="请选择设备的流类型" v-decorator="['deviceStreamType', {rules: [{ required: true, message: '请选择设备的流类型！' }]}]">
            <a-select-option v-for="(item,index) in deviceStreamTypeData" :key="index" :value="item.code">{{ item.name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="设备的网络类型" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <a-select style="width: 100%" placeholder="请选择设备的网络类型" v-decorator="['deviceNetworkType', {rules: [{ required: true, message: '请选择设备的网络类型！' }]}]">
            <a-select-option v-for="(item,index) in deviceNetworkTypeData" :key="index" :value="item.code">{{ item.name }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="上级部门名称" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入上级部门名称" v-decorator="['pDepartmentName']" />
        </a-form-item>
        <a-form-item label="上级部门代码" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入上级部门代码" v-decorator="['pDepartmentId']" />
        </a-form-item>
        <a-form-item label="部门名称" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入部门名称" v-decorator="['departmentName']" />
        </a-form-item>
        <a-form-item label="部门代码" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入部门代码" v-decorator="['departmentId']" />
        </a-form-item>
        <a-form-item label="通道名称" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入通道名称" v-decorator="['channelName']" />
        </a-form-item>
        <a-form-item label="app" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入app" v-decorator="['app']" />
        </a-form-item>
        <a-form-item label="vhost" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入vhost" v-decorator="['vhost']" />
        </a-form-item>
        <a-form-item label="流媒体服务器ID" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入流媒体服务器ID" v-decorator="['mediaServerId']" />
        </a-form-item>
        <a-form-item label="设备ID" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input placeholder="请输入设备ID" v-decorator="['mainId']" />
        </a-form-item>
        <a-form-item label="自动推拉流" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-switch v-decorator="['autoVideo', { valuePropName: 'checked' }]" />
        </a-form-item>
        <a-form-item label="录制时长（秒）" :labelCol="labelCol" :wrapperCol="wrapperCol" has-feedback>
          <a-input-number placeholder="请输入录制时长（秒）" style="width: 100%" v-decorator="['recordSecs']" />
        </a-form-item>
      </a-form>
    </a-spin>
  </a-modal>
</template>

<script>
  import moment from 'moment'
  import {
    videochannelsEdit
  } from '@/api/modular/main/videochannelsManage'
  export default {
    data () {
      return {
        labelCol: {
          xs: { span: 24 },
          sm: { span: 5 }
        },
        wrapperCol: {
          xs: { span: 24 },
          sm: { span: 15 }
        },
        updateTimeDateString: '',
        createTimeDateString: '',
        videoDeviceTypeData: [],
        methodByGetStreamData: [],
        deviceStreamTypeData: [],
        deviceNetworkTypeData: [],
        visible: false,
        confirmLoading: false,
        form: this.$form.createForm(this)
      }
    },
    methods: {
      moment,
      // 初始化方法
      edit (record) {
        this.visible = true
        const videoDeviceTypeOption = this.$options
        this.videoDeviceTypeData = videoDeviceTypeOption.filters['dictData']('device_type')
        const methodByGetStreamOption = this.$options
        this.methodByGetStreamData = methodByGetStreamOption.filters['dictData']('getstream_type')
        const deviceStreamTypeOption = this.$options
        this.deviceStreamTypeData = deviceStreamTypeOption.filters['dictData']('device_stream_type')
        const deviceNetworkTypeOption = this.$options
        this.deviceNetworkTypeData = deviceNetworkTypeOption.filters['dictData']('device_net_type')
        setTimeout(() => {
          this.form.setFieldsValue(
            {
              id: record.id,
              noPlayerBreak: record.noPlayerBreak,
              enabled: record.enabled,
              defaultRtpPort: record.defaultRtpPort,
              videoSrcUrl: record.videoSrcUrl,
              rtpWithTcp: record.rtpWithTcp,
              channelId: record.channelId,
              deviceId: record.deviceId,
              hasPtz: record.hasPtz,
              ipV6Address: record.ipV6Address,
              ipV4Address: record.ipV4Address,
              recordPlanName: record.recordPlanName,
              spjkTZID: record.spjkTZID,
              autoRecord: record.autoRecord,
              videoDeviceType: record.videoDeviceType,
              methodByGetStream: record.methodByGetStream,
              deviceStreamType: record.deviceStreamType,
              deviceNetworkType: record.deviceNetworkType,
              pDepartmentName: record.pDepartmentName,
              pDepartmentId: record.pDepartmentId,
              departmentName: record.departmentName,
              departmentId: record.departmentId,
              channelName: record.channelName,
              app: record.app,
              vhost: record.vhost,
              mediaServerId: record.mediaServerId,
              mainId: record.mainId,
              autoVideo: record.autoVideo,
              recordSecs: record.recordSecs
            }
          )
        }, 100)
        this.form.getFieldDecorator('updateTime', { initialValue: moment(record.updateTime, 'YYYY-MM-DD') })
        this.updateTimeDateString = moment(record.updateTime).format('YYYY-MM-DD')
        this.form.getFieldDecorator('createTime', { initialValue: moment(record.createTime, 'YYYY-MM-DD') })
        this.createTimeDateString = moment(record.createTime).format('YYYY-MM-DD')
      },
      handleSubmit () {
        const { form: { validateFields } } = this
        this.confirmLoading = true
        validateFields((errors, values) => {
          if (!errors) {
            for (const key in values) {
              if (typeof (values[key]) === 'object') {
                values[key] = JSON.stringify(values[key])
              }
            }
            values.updateTime = this.updateTimeDateString
            values.createTime = this.createTimeDateString
            videochannelsEdit(values).then((res) => {
              if (res.success) {
                this.$message.success('编辑成功')
                this.confirmLoading = false
                this.$emit('ok', values)
                this.handleCancel()
              } else {
                this.$message.error('编辑失败：' + JSON.stringify(res.message))
              }
            }).finally((res) => {
              this.confirmLoading = false
            })
          } else {
            this.confirmLoading = false
          }
        })
      },
      onChangeupdateTime(date, dateString) {
        this.updateTimeDateString = dateString
      },
      onChangecreateTime(date, dateString) {
        this.createTimeDateString = dateString
      },
      handleCancel () {
        this.form.resetFields()
        this.visible = false
      }
    }
  }
</script>
