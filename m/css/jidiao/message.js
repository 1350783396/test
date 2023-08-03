
var promistList = {
};
//用来外部决议
var resolveList = {
};
var rejectList = {
}

window.sendMsg = function(msg){
    var id = Math.uuidFast();
    promistList[id] = new Promise(function(resolve, reject){
        window.postMessage({
            id:id,
            data:msg,
            type:'from-injected-script'
        }, '*');
        resolveList[id] = resolve;
        rejectList[id] = reject;
    });
    return promistList[id];
}

window.addEventListener('message', function(e){
    if(e.data.type === 'from-content-script'){
        console.debug('message js', e);
        if(e.data.result == 0){
            //success
            resolveList[e.data.id](e.data.data);
        }else{
            rejectList[e.data.id](e.data.data);
        }
    }
}, true);