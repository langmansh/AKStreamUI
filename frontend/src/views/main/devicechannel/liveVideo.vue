<template>
	<div class="video-wrapper" v-show="isShowVideo">
		<video id="videoElement" controls autoplay muted width="100%" height="100%"></video>
		<div class="video-close" @click="onVideoFn(false)">x</div>
	</div>
</template>

<script>
  import flvjs from 'flv.js'
  import moment from 'moment'
  import { livevideoFun } from '@/api/modular/main/devicechannelManage'
  export default {
    data () {
      return {
		  msg: 'Welcome to Your Vue.js App',
		  visible: false,
		  confirmLoading: false,
		  isShowVideo: false
	  }
    },
	mounted() {

	},
    methods: {
      moment,
      // 初始化方法
      open (record) {
        
      },
	  play () {
	    this.flvPlayer.play();
	  },
      handleCancel () {
        this.form.resetFields()
        this.visible = false
      },
	  giveParams(record) {
		  this.isShowVideo = true;
		  var para = {deviceId:record.parentId,channelId:record.deviceId}
		  livevideoFun(para).then((res) => {
			if(res.code == 200)
			{
				this.url = res.data.playUrl[0];
				if (flvjs && flvjs.isSupported()) {
					 var videoElement = document.getElementById('videoElement');
					 this.flvPlayer = flvjs.createPlayer({
					   type: 'flv',
					   isLive: true,
					   hasAudio: false,
					   url: this.url
					 });
					 this.flvPlayer.attachMediaElement(videoElement);
					 this.flvPlayer.load();
					 this.flvPlayer.play();
					 
				}
			}
		  })
		
	  },
	  onVideoFn(url, flag = false) {
		  this.flvPlayer.pause();
		  this.flvPlayer.unload();  
		  this.flvPlayer.detachMediaElement();  
		  this.flvPlayer.destroy();  
		  this.flvPlayer = null;
		  this.isShowVideo = flag;
	  }
	}
  }
</script>

<style >
	.video-wrapper {
		position: fixed;
		top: 50%;
		left: 50%;
		width: 800px;
		height: 600px;
		transform: translate(-50%, -50%);
		background-color:gray;
		
	}
	.video-close {
		position: absolute;
		top: 0;
		right: 0;
		width:30px;
		height: 30px;
		background-color: red;
		border-radius:50%;
		text-align: center;
		line-height: 30px;
	}
</style>
