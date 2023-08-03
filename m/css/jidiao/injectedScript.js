window.easydoc = {
	convertDataURLAsBlob(dataurl){
		let arr = dataurl.split(',')
        if(arr.length < 2){
            return new Blob()
        }
		let mimeRes = arr[0].match(/:(.*?);/)
		let mime = mimeRes?mimeRes[1]:''
		let bstr = atob(arr[1])
		let n = bstr.length
		let u8arr = new Uint8Array(n)
		while(n--){
			u8arr[n] = bstr.charCodeAt(n)
		}
		return new Blob([u8arr], {type:mime})
	},
	convertBlobAsDataURL(blob){
		return new Promise((resolve,reject)=>{
			if(blob instanceof Blob){
				let reader = new FileReader()
				reader.onload = (e)=>{
					resolve(e.target.result)
				}
				reader.readAsDataURL(blob)
			}else{
				reject('参数不是Blob Prototype')
			}
		})
	},
	request(config){
		return window.sendMsg(config)
	},
	async axiosRequest(config){
		// 假如发送了formdata数据，那么需要使用json serializable的数据
		if(config.data instanceof FormData){
			config.isFormData = true
			let formData = {}
			for(let k of config.data.keys()){
				let v = config.data.get(k)
				// 如果是文件，那么需要转换
				let data = v
				if(v instanceof File){
					data = {
						type: 'file',
						name: v.name,
						data: await this.convertBlobAsDataURL(v)
					}
				}else{
					data = {
						data: v
					}
				}
				formData[k] = data
			}
			config.data = formData
		}
		return window.sendMsg({
			version: 2,
			config,
		}).then(async res=>{
			if(res.isBlob){
				res.data = await this.convertDataURLAsBlob(res.data)
			}
			return res
		})
	},
	version: 2,
}
