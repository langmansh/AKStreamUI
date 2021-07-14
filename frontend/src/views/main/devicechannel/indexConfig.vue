<template>
  <a-card :bordered="false" v-show="indexConfigShow">
    <div class="table-operator">
      <a-button class="but_item" type="dashed" @click="handleCancel" icon="rollback">返回</a-button>
    </div>
    <a-table
      ref="table"
      size="middle"
      :columns="columns"
      :dataSource="loadData"
      :pagination="true"
      :alert="true"
      :loading="tableLoading"
      :rowKey="(record) => record.id">
	    <span slot="action" slot-scope="text, record">
	      <a v-if="hasPerm('devicechannel:detail')" @click="$refs.livevideo.giveParams(record)">直播</a>
		  <a-divider type="vertical"/>
		  <a v-if="hasPerm('devicechannel:detail')" @click="recordOpen(record)">回放</a>
	    </span>
    </a-table>
	<!-- <edit-form ref="livevideo1" @ok="handleOk" /> -->
	<live-video ref="livevideo"></live-video>
  </a-card>
</template>
<script>
import liveVideo from './liveVideo.vue'
import recordVideo from './record.vue'

  export default {
    components: {
      liveVideo
    },
    data() {
      return {
        // 表头
        columns: [{
            title: 'Device ID',
            align: 'center',
            dataIndex: 'parentId'
          },
          {
            title: 'Channel ID',
            align: 'center',
            dataIndex: 'deviceId'
          },
		  {
		    title: '名称',
		    align: 'center',
		    dataIndex: 'sipChannelDesc.name'
		  },
		  {
		    title: '设备状态',
		    align: 'center',
			sorter: true,
		    customRender: (vlaue) => (vlaue == 'ON' ? <a-tag color="green">在线</a-tag> : <a-tag color="volcano">离线</a-tag>),
		    dataIndex: 'sipChannelStatus'
		  },
		  {
		    title: '推流状态',
		    align: 'center',
		    customRender: (vlaue) => (vlaue == 'IDLE' ? <a-tag color="green">空闲</a-tag> : <a-tag color="volcano">推流</a-tag>),
		    dataIndex: 'pushStatus'
		  }
        ],
        indexConfigShow: false,
        tableLoading: false,
        visible: false,
        loadData: []
      }
    },
	created () {
	  this.columns.push({
	    title: '操作',
	    width: '230px',
	    dataIndex: 'action',
	    scopedSlots: { customRender: 'action' }
	  })
	},
    methods: {
      /**
       * 打开界面
       */
      open(data) {
        this.indexConfigShow = true
        this.tableLoading = true
				this.loadData = data
				this.tableLoading = false
      },
	  livevideoOpen(record) {
		this.indexConfigShow = false
		this.$refs.livevideo.edit(record)
	  },
	  recordOpen (record) {
	    this.indexOpenShow = false
	    this.$refs.recordVideo.open(record)
	  },
	  handleOk () {
	    this.$refs.table.refresh()
	  },
      handleCancel() {
        this.$emit('ok')
        this.loadData = []
        this.indexConfigShow = false
      },
    }
  }
</script>
