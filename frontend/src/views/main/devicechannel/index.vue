<template>
  <div>
    <a-card :bordered="false" v-show="indexOpenShow">
      <a-spin :spinning="Loading">
        <s-table
          ref="table"
          :columns="columns"
          :data="loadData"
          :alert="true"
          :rowKey="(record) => record.id"
          :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
        >
         <!-- <div slot="operator" v-if="hasPerm('codeGenerate:add')" >
            <a-button type="primary" v-if="hasPerm('codeGenerate:add')" icon="plus" @click="$refs.addForm.add()">新增</a-button>
          </div> -->
  
          <span slot="action" slot-scope="text, record">
            <a v-if="hasPerm('devicechannel:detail')" @click="indexConfigOpen(record.sipChannels)">通道管理</a>
          </span>
        </s-table>
      </a-spin>
    </a-card>
    <index-config ref="indexConfig" @ok="handleResetOpen" v-if="hasPerm('devicechannel:detail')"/>
  </div>
</template>
<script>
  import { STable } from '@/components'
  import { devicechannelPage } from '@/api/modular/main/devicechannelManage'
  import indexConfig from './indexConfig.vue'

  export default {
    components: {
	  indexConfig,
      STable
    },
    data () {
      return {
        // 查询参数
        queryParam: {},
        // 表头
        columns: [
          {
            title: 'Device ID',
            align: 'center',
            sorter: true,
            dataIndex: 'deviceId'
          },
          {
            title: 'IP地址',
            align: 'center',
            sorter: true,
            dataIndex: 'ipAddress'
          },
          {
            title: '端口',
            align: 'center',
            sorter: true,
            dataIndex: 'port'
          },
          {
            title: '厂商',
            align: 'center',
            sorter: true,
            dataIndex: 'deviceInfo.manufacturer'
          },
		  {
		    title: '通道数',
		    align: 'center',
		    sorter: true,
		    dataIndex: 'deviceInfo.channel'
		  },
		  {
		    title: '状态',
		    align: 'center',
		    sorter: true,
			customRender: (vlaue) => (vlaue == 'ONLINE' ? <a-tag color="green">在线</a-tag> : <a-tag color="volcano">离线</a-tag>),
		    dataIndex: 'deviceStatus.online'
		  },
		  {
		    title: '录制',
		    align: 'center',
		    sorter: true,
		  	customRender: (vlaue) => (vlaue == 'ON' ? <a-tag color="green">开启</a-tag> : <a-tag color="volcano">关闭</a-tag>),
		    dataIndex: 'deviceStatus.record'
		  }
        ],
        tstyle: { 'padding-bottom': '0px', 'margin-bottom': '10px' },
        loadData: parameter => {
          return devicechannelPage(Object.assign(parameter, this.queryParam)).then((res) => {
            return res.data
          })
        },
        selectedRowKeys: [],
        selectedRows: [],
        Loading: false,
        jdbcDriverList: [],
        indexOpenShow: true
      }
    },
    created () {
      if (this.hasPerm('codeGenerate:edit') || this.hasPerm('codeGenerate:delete')) {
        this.columns.push({
          title: '操作',
          width: '230px',
          dataIndex: 'action',
          scopedSlots: { customRender: 'action' }
        })
      }
    },
    methods: {
      /**
       * 开始生成代码（生成压缩包）
       */
      runDownCodeGenerate (record) {
        this.Loading = true
        codeGenerateRunDown({ id: record.id }).then((res) => {
          this.Loading = false
          this.downloadfile(res)
          // eslint-disable-next-line handle-callback-err
        }).catch((err) => {
          this.Loading = false
          this.$message.error('下载错误：获取文件流错误')
        })
      },
      downloadfile (res) {
        var blob = new Blob([res.data], { type: 'application/octet-stream;charset=UTF-8' })
        var contentDisposition = res.headers['content-disposition']
        var patt = new RegExp('filename=([^;]+\\.[^\\.;]+);*')
        var result = patt.exec(contentDisposition)
        var filename = result[1]
        var downloadElement = document.createElement('a')
        var href = window.URL.createObjectURL(blob) // 创建下载的链接
        var reg = /^["](.*)["]$/g
        downloadElement.style.display = 'none'
        downloadElement.href = href
        downloadElement.download = decodeURI(filename.replace(reg, '$1')) // 下载后文件名
        document.body.appendChild(downloadElement)
        downloadElement.click() // 点击下载
        document.body.removeChild(downloadElement) // 下载完成移除元素
        window.URL.revokeObjectURL(href)
      },
      /**
       * 开始生成代码（本地项目）
       */
      runLocalCodeGenerate (record) {
        codeGenerateRunLocal(record).then((res) => {
          if (res.success) {
            this.$message.success('生成成功')
            this.$refs.table.refresh()
          } else {
            this.$message.error('生成失败：' + res.message)
          }
        })
      },
      /**
       * 删除
       */
      codeGenerateDelete (record) {
        this.Loading = true
        codeGenerateDelete([{ id: record.id }]).then((res) => {
          if (res.success) {
            this.$message.success('删除成功')
            this.$refs.table.refresh()
          } else {
            this.$message.error('删除失败：' + res.message)
          }
        }).catch((err) => {
          this.$message.error('删除错误：' + err.message)
        }).finally((res) => {
          this.Loading = false
        })
      },
      /**
       * 打开配置界面
       */
      indexConfigOpen (record) {
        this.indexOpenShow = false
        this.$refs.indexConfig.open(record)
      },
      /**
       * 详细配置界面返回
       */
      handleResetOpen () {
        this.indexOpenShow = true
        this.$refs.table.refresh()
      },
      /**
       * 其他界面返回
       */
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
