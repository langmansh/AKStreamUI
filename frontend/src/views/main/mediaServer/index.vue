<template>
  <div>
    <a-card :bordered="false">
      <s-table
        ref="table"
        :columns="columns"
        :data="loadData"
        :alert="true"
        :rowKey="(record) => record.id"
        :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }">
<!--        <template class="table-operator" slot="operator" v-if="hasPerm('mediaserver:start')" >
          <a-button type="primary" v-if="hasPerm('mediaserver:start')" icon="plus" @click="$refs.addForm.add()">启动</a-button>
        </template> -->
<!--        <span slot="videoDeviceTypescopedSlots" slot-scope="text">
          {{ 'device_type' | dictType(text) }}
        </span>
        <span slot="methodByGetStreamscopedSlots" slot-scope="text">
          {{ 'getstream_type' | dictType(text) }}
        </span>
        <span slot="deviceStreamTypescopedSlots" slot-scope="text">
          {{ 'device_stream_type' | dictType(text) }}
        </span>
        <span slot="deviceNetworkTypescopedSlots" slot-scope="text">
          {{ 'device_net_type' | dictType(text) }}
        </span> -->
        <span slot="action" slot-scope="text, record">
          <a-popconfirm v-if="hasPerm('mediaserver:start')" placement="topRight" title="确认启动？" @confirm="() => mediaserverStart(record.mediaServerId)">
            <a>启动</a>
          </a-popconfirm>
          <a-divider type="vertical" v-if="hasPerm('mediaserver:start') & hasPerm('mediaserver:stop')"/>
          <a-popconfirm v-if="hasPerm('mediaserver:stop')" placement="topRight" title="确认停止？" @confirm="() => mediaserverStop(record.mediaServerId)">
            <a>停止</a>
          </a-popconfirm>
        </span>
      </s-table>
     <!-- <add-form ref="addForm" @ok="handleOk" />
      <edit-form ref="editForm" @ok="handleOk" /> -->
    </a-card>
  </div>
</template>
<script>
  import { STable } from '@/components'
  import moment from 'moment'
  import { mediaserverPage,mediaserverStop,mediaserverStart } from '@/api/modular/main/mediaserverManage'
  //import addForm from './addForm.vue'
  //import editForm from './editForm.vue'
  export default {
    components: {
      STable
      //addForm,
      //editForm
    },
    data () {
      return {
        advanced: false, // 高级搜索 展开/关闭
        queryParam: {},
        columns: [
		  {
			title: '流媒体ID',
			align: 'center',
			sorter: true,
			dataIndex: 'mediaServerId'
		  },
		  {
		    title: 'ipV4',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'ipV4Address'
		  },
		  {
		    title: 'ipV6',
		    align: 'center',
			sorter: true,
		    dataIndex: 'ipV6Address'
		  },
		  {
		    title: 'http端口',
		    align: 'center',
			sorter: true,
		    dataIndex: 'httpPort'
		  },
		  {
		    title: 'keeper服务',
		    align: 'center',
		    sorter: true,
			customRender: (vlaue) => (vlaue ? <a-tag color="green">运行</a-tag> : <a-tag color="volcano">停止</a-tag>),
		    dataIndex: 'isKeeperRunning'
		  },
		  {
		    title: 'keeper端口',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'keeperPort'
		  },
          {
            title: 'zlm服务',
            align: 'center',
			sorter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">运行</a-tag> : <a-tag color="volcano">停止</a-tag>),
            dataIndex: 'isMediaServerRunning'
          },
          {
            title: '秘钥',
            align: 'center',
			sorter: true,
            dataIndex: 'secret'
          }
        ],
        tstyle: { 'padding-bottom': '0px', 'margin-bottom': '10px' },
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          // return mediaserverPage(Object.assign(parameter, this.switchingDate())).then((res) => {
          //   return res.data
          // })
		  return mediaserverPage(Object.assign(parameter, null)).then((res) => {
		    return res.data
		  })
        },
        // videoDeviceTypeData: [],
        // methodByGetStreamData: [],
        // deviceStreamTypeData: [],
        // deviceNetworkTypeData: [],
        selectedRowKeys: [],
        selectedRows: []
      }
    },
    created () {
      if (this.hasPerm('mediaserver:start') || this.hasPerm('mediaserver:stop')) {
        this.columns.push({
          title: '操作',
          width: '150px',
          dataIndex: 'action',
          scopedSlots: { customRender: 'action' }
        })
      }
      // const videoDeviceTypeOption = this.$options
      // this.videoDeviceTypeData = videoDeviceTypeOption.filters['dictData']('device_type')
      // const methodByGetStreamOption = this.$options
      // this.methodByGetStreamData = methodByGetStreamOption.filters['dictData']('getstream_type')
      // const deviceStreamTypeOption = this.$options
      // this.deviceStreamTypeData = deviceStreamTypeOption.filters['dictData']('device_stream_type')
      // const deviceNetworkTypeOption = this.$options
      // this.deviceNetworkTypeData = deviceNetworkTypeOption.filters['dictData']('device_net_type')
    },
    methods: {
      moment,
      /**
       * 查询参数组装
       */
      // switchingDate () {
      //   const queryParamupdateTime = this.queryParam.updateTimeDate
      //   if (queryParamupdateTime != null) {
      //       this.queryParam.updateTime = moment(queryParamupdateTime).format('YYYY-MM-DD')
      //       if (queryParamupdateTime.length < 1) {
      //           delete this.queryParam.updateTime
      //       }
      //   }
      //   const queryParamcreateTime = this.queryParam.createTimeDate
      //   if (queryParamcreateTime != null) {
      //       this.queryParam.createTime = moment(queryParamcreateTime).format('YYYY-MM-DD')
      //       if (queryParamcreateTime.length < 1) {
      //           delete this.queryParam.createTime
      //       }
      //   }
      //   const obj = JSON.parse(JSON.stringify(this.queryParam))
      //   return obj
      // },
	  mediaserverStart (record) {
	    mediaserverStart(record).then((res) => {
	      if (res.success) {
	        this.$message.success('启动成功')
	        this.$refs.table.refresh()
	      } else {
	        this.$message.error('启动失败') // + res.message
	      }
	    })
	  },
      mediaserverStop (record) {
        mediaserverStop(record).then((res) => {
          if (res.success) {
            this.$message.success('停止成功')
            this.$refs.table.refresh()
          } else {
            this.$message.error('停止失败') // + res.message
          }
        })
      },
      toggleAdvanced () {
        this.advanced = !this.advanced
      },
      onChangeupdateTime(date, dateString) {
        this.updateTimeDateString = dateString
      },
      onChangecreateTime(date, dateString) {
        this.createTimeDateString = dateString
      },
      handleOk () {
        this.$refs.table.refresh()
      },
      onSelectChange (selectedRowKeys, selectedRows) {
        this.selectedRowKeys = selectedRowKeys
        this.selectedRows = selectedRows
      }
    }
  }
</script>
<style lang="less">
  .table-operator {
    margin-bottom: 18px;
  }
  button {
    margin-right: 8px;
  }
</style>
