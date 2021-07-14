<template>
  <div>
    <a-card :bordered="false" :bodyStyle="tstyle">

      <div class="table-page-search-wrapper" v-if="hasPerm('videochannels:page')">
        <a-form layout="inline">
          <a-row :gutter="48"><template v-if="advanced">
              <a-col :md="8" :sm="24">
                <a-form-item label="通道ID">
                  <a-input v-model="queryParam.channelId" allow-clear placeholder="请输入通道ID"/>
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="设备ID">
                  <a-input v-model="queryParam.deviceId" allow-clear placeholder="请输入设备ID"/>
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="台账ID">
                  <a-input v-model="queryParam.spjkTZID" allow-clear placeholder="请输入台账ID"/>
                </a-form-item>
              </a-col><a-col :md="8" :sm="24">
                <a-form-item label="设备类型">
                  <a-select :allowClear="true" style="width: 100%" v-model="queryParam.videoDeviceType" placeholder="请选择设备类型">
                    <a-select-option v-for="(item,index) in videoDeviceTypeData" :key="index" :value="item.code">{{ item.name }}</a-select-option>
                  </a-select>
                </a-form-item>
              </a-col><a-col :md="8" :sm="24">
                <a-form-item label="设备编号">
                  <a-input v-model="queryParam.mainId" allow-clear placeholder="请输入设备编号"/>
                </a-form-item>
              </a-col>          </template>

            <a-col :md="8" :sm="24" >
              <span class="table-page-search-submitButtons">
                <a-button type="primary" @click="$refs.table.refresh(true)" >查询</a-button>
                <a-button style="margin-left: 8px" @click="() => queryParam = {}">重置</a-button>
                <a @click="toggleAdvanced" style="margin-left: 8px"> {{ advanced ? '收起' : '展开' }}
                  <a-icon :type="advanced ? 'up' : 'down'"/>
                </a>
              </span>
            </a-col>

          </a-row>
        </a-form>
      </div>
    </a-card>
    <a-card :bordered="false">
      <s-table
        ref="table"
        :columns="columns"
        :data="loadData"
        :alert="true"
        :rowKey="(record) => record.id"
        :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }">
        <template class="table-operator" slot="operator" v-if="hasPerm('videochannels:add')" >
          <a-button type="primary" v-if="hasPerm('videochannels:add')" icon="plus" @click="$refs.addForm.add()">新增设备</a-button>
        </template>
        <span slot="videoDeviceTypescopedSlots" slot-scope="text">
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
        </span>
        <span slot="action" slot-scope="text, record">
          <a v-if="hasPerm('videochannels:edit')" @click="$refs.editForm.edit(record)">编辑</a>
          <a-divider type="vertical" v-if="hasPerm('videochannels:edit') & hasPerm('videochannels:delete')"/>
          <a-popconfirm v-if="hasPerm('videochannels:delete')" placement="topRight" title="确认删除？" @confirm="() => videochannelsDelete(record)">
            <a>删除</a>
          </a-popconfirm>
        </span>
      </s-table>
      <add-form ref="addForm" @ok="handleOk" />
      <edit-form ref="editForm" @ok="handleOk" />
    </a-card>
  </div>
</template>
<script>
  import { STable } from '@/components'
  import moment from 'moment'
  import { videochannelsPage, videochannelsDelete } from '@/api/modular/main/videochannelsManage'
  import addForm from './addForm.vue'
  import editForm from './editForm.vue'
  export default {
    components: {
      STable,
      addForm,
      editForm
    },
    data () {
      return {
        advanced: false, // 高级搜索 展开/关闭
        queryParam: {},
        columns: [
		  {
			title: '设备ID',
			align: 'center',
			sorter: true,
			dataIndex: 'mainId'
		  },
		  {
		    title: '台账ID',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'spjkTZID'
		  },
		  {
		    title: '流媒体服务器ID',
		    align: 'center',
			sorter: true,
		    dataIndex: 'mediaServerId'
		  },
		  {
		    title: '通道名称',
		    align: 'center',
			sorter: true,
		    dataIndex: 'channelName'
		  },
		  {
		    title: '通道ID',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'channelId'
		  },
		  {
		    title: '设备ID',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'deviceId'
		  },
          {
            title: '无人断流',
            align: 'center',
			sorter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">启用</a-tag> : <a-tag color="volcano">禁用</a-tag>),
            dataIndex: 'noPlayerBreak'
          },
          {
            title: '是否启用',
            align: 'center',
			sorter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">启用</a-tag> : <a-tag color="volcano">禁用</a-tag>),
            dataIndex: 'enabled'
          },
          {
            title: '云台',
            align: 'center',
			orter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">启用</a-tag> : <a-tag color="volcano">禁用</a-tag>),
            dataIndex: 'hasPtz'
          },
          {
            title: '自动录制',
            align: 'center',
			sorter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">启用</a-tag> : <a-tag color="volcano">禁用</a-tag>),
            dataIndex: 'autoRecord'
          },
          {
            title: '自动推拉流',
            align: 'center',
			sorter: true,
            customRender: (vlaue) => (vlaue ? <a-tag color="green">启用</a-tag> : <a-tag color="volcano">禁用</a-tag>),
            dataIndex: 'autoVideo'
          }
        ],
        tstyle: { 'padding-bottom': '0px', 'margin-bottom': '10px' },
        // 加载数据方法 必须为 Promise 对象
        loadData: parameter => {
          return videochannelsPage(Object.assign(parameter, this.switchingDate())).then((res) => {
            return res.data
          })
        },
        videoDeviceTypeData: [],
        methodByGetStreamData: [],
        deviceStreamTypeData: [],
        deviceNetworkTypeData: [],
        selectedRowKeys: [],
        selectedRows: []
      }
    },
    created () {
      if (this.hasPerm('videochannels:edit') || this.hasPerm('videochannels:delete')) {
        this.columns.push({
          title: '操作',
          width: '150px',
          dataIndex: 'action',
          scopedSlots: { customRender: 'action' }
        })
      }
      const videoDeviceTypeOption = this.$options
      this.videoDeviceTypeData = videoDeviceTypeOption.filters['dictData']('device_type')
      const methodByGetStreamOption = this.$options
      this.methodByGetStreamData = methodByGetStreamOption.filters['dictData']('getstream_type')
      const deviceStreamTypeOption = this.$options
      this.deviceStreamTypeData = deviceStreamTypeOption.filters['dictData']('device_stream_type')
      const deviceNetworkTypeOption = this.$options
      this.deviceNetworkTypeData = deviceNetworkTypeOption.filters['dictData']('device_net_type')
    },
    methods: {
      moment,
      /**
       * 查询参数组装
       */
      switchingDate () {
        const queryParamupdateTime = this.queryParam.updateTimeDate
        if (queryParamupdateTime != null) {
            this.queryParam.updateTime = moment(queryParamupdateTime).format('YYYY-MM-DD')
            if (queryParamupdateTime.length < 1) {
                delete this.queryParam.updateTime
            }
        }
        const queryParamcreateTime = this.queryParam.createTimeDate
        if (queryParamcreateTime != null) {
            this.queryParam.createTime = moment(queryParamcreateTime).format('YYYY-MM-DD')
            if (queryParamcreateTime.length < 1) {
                delete this.queryParam.createTime
            }
        }
        const obj = JSON.parse(JSON.stringify(this.queryParam))
        return obj
      },
      videochannelsDelete (record) {
        videochannelsDelete(record).then((res) => {
          if (res.success) {
            this.$message.success('删除成功')
            this.$refs.table.refresh()
          } else {
            this.$message.error('删除失败') // + res.message
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
