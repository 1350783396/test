﻿/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.dialog.add('link',function(a){var b=function(){var A=this.getDialog(),B=A.getContentElement('target','popupFeatures'),C=A.getContentElement('target','linkTargetName'),D=this.getValue();if(!B||!C)return;B=B.getElement();if(D=='popup'){B.show();C.setLabel(a.lang.link.targetPopupName);}else{B.hide();C.setLabel(a.lang.link.targetFrameName);this.getDialog().setValueOf('target','linkTargetName',D.charAt(0)=='_'?D:'');}},c=function(){var A=this.getDialog(),B=['urlOptions','anchorOptions','emailOptions'],C=this.getValue(),D=A.definition.getContents('upload'),E=D&&D.hidden;if(C=='url'){if(a.config.linkShowTargetTab)A.showPage('target');if(!E)A.showPage('upload');}else{A.hidePage('target');if(!E)A.hidePage('upload');}for(var F=0;F<B.length;F++){var G=A.getContentElement('info',B[F]);if(!G)continue;G=G.getElement().getParent().getParent();if(B[F]==C+'Options')G.show();else G.hide();}},d=/^mailto:([^?]+)(?:\?(.+))?$/,e=/subject=([^;?:@&=$,\/]*)/,f=/body=([^;?:@&=$,\/]*)/,g=/^#(.*)$/,h=/^(?!javascript)((?:http|https|ftp|news):\/\/)?(.*)$/,i=/^(_(?:self|top|parent|blank))$/,j=/^javascript:void\(location\.href='mailto:'\+String\.fromCharCode\(([^)]+)\)(?:\+'(.*)')?\)$/,k=/^javascript:([^(]+)\(([^)]+)\)$/,l=/\s*window.open\(\s*this\.href\s*,\s*(?:'([^']*)'|null)\s*,\s*'([^']*)'\s*\)\s*;\s*return\s*false;*\s*/,m=/(?:^|,)([^=]+)=(\d+|yes|no)/gi,n=function(A,B){var C=B?B.getAttribute('_cke_saved_href')||B.getAttribute('href'):'',D,E,F,G={};if(E=C.match(g)){G.type='anchor';G.anchor={};G.anchor.name=G.anchor.id=E[1];}else if(C&&(F=C.match(h))){G.type='url';G.url={};G.url.protocol=F[1];G.url.url=F[2];}else if(!w||w=='encode'){if(w=='encode')C=C.replace(j,function(W,X,Y){return 'mailto:'+String.fromCharCode.apply(String,X.split(','))+(Y&&u(Y));});D=C.match(d);if(D){var H=C.match(e),I=C.match(f);G.type='email';var J=G.email={};J.address=D[1];H&&(J.subject=decodeURIComponent(H[1]));I&&(J.body=decodeURIComponent(I[1]));}}else if(w)C.replace(k,function(W,X,Y){if(X==x.name){G.type='email';var Z=G.email={},aa=/[^,\s]+/g,ab=/(^')|('$)/g,ac=Y.match(aa),ad=ac.length,ae,af;for(var ag=0;ag<ad;ag++){af=decodeURIComponent(u(ac[ag].replace(ab,'')));ae=x.params[ag].toLowerCase();Z[ae]=af;}Z.address=[Z.name,Z.domain].join('@');}});else G.type='url';if(B){var K=B.getAttribute('target');G.target={};G.adv={};if(!K){var L=B.getAttribute('_cke_pa_onclick')||B.getAttribute('onclick'),M=L&&L.match(l);if(M){G.target.type='popup';G.target.name=M[1];var N;while(N=m.exec(M[2])){if(N[2]=='yes'||N[2]=='1')G.target[N[1]]=true;
else if(isFinite(N[2]))G.target[N[1]]=N[2];}}}else{var O=K.match(i);if(O)G.target.type=G.target.name=K;else{G.target.type='frame';G.target.name=K;}}var P=this,Q=function(W,X){var Y=B.getAttribute(X);if(Y!==null)G.adv[W]=Y||'';};Q('advId','id');Q('advLangDir','dir');Q('advAccessKey','accessKey');Q('advName','name');Q('advLangCode','lang');Q('advTabIndex','tabindex');Q('advTitle','title');Q('advContentType','type');Q('advCSSClasses','class');Q('advCharset','charset');Q('advStyles','style');}var R=A.document.getElementsByTag('img'),S=new CKEDITOR.dom.nodeList(A.document.$.anchors),T=G.anchors=[];for(var U=0;U<R.count();U++){var V=R.getItem(U);if(V.getAttribute('_cke_realelement')&&V.getAttribute('_cke_real_element_type')=='anchor')T.push(A.restoreRealElement(V));}for(U=0;U<S.count();U++)T.push(S.getItem(U));for(U=0;U<T.length;U++){V=T[U];T[U]={name:V.getAttribute('name'),id:V.getAttribute('id')};}this._.selectedElement=B;return G;},o=function(A,B){if(B[A])this.setValue(B[A][this.id]||'');},p=function(A){return o.call(this,'target',A);},q=function(A){return o.call(this,'adv',A);},r=function(A,B){if(!B[A])B[A]={};B[A][this.id]=this.getValue()||'';},s=function(A){return r.call(this,'target',A);},t=function(A){return r.call(this,'adv',A);};function u(A){return A.replace(/\\'/g,"'");};function v(A){return A.replace(/'/g,'\\$&');};var w=a.config.emailProtection||'';if(w&&w!='encode'){var x={};w.replace(/^([^(]+)\(([^)]+)\)$/,function(A,B,C){x.name=B;x.params=[];C.replace(/[^,\s]+/g,function(D){x.params.push(D);});});}function y(A){var B,C=x.name,D=x.params,E,F;B=[C,'('];for(var G=0;G<D.length;G++){E=D[G].toLowerCase();F=A[E];G>0&&B.push(',');B.push("'",F?v(encodeURIComponent(A[E])):'',"'");}B.push(')');return B.join('');};function z(A){var B,C=A.length,D=[];for(var E=0;E<C;E++){B=A.charCodeAt(E);D.push(B);}return 'String.fromCharCode('+D.join(',')+')';};return{title:a.lang.link.title,minWidth:350,minHeight:230,contents:[{id:'info',label:a.lang.link.info,title:a.lang.link.info,elements:[{id:'linkType',type:'select',label:a.lang.link.type,'default':'url',items:[[a.lang.common.url,'url'],[a.lang.link.toAnchor,'anchor'],[a.lang.link.toEmail,'email']],onChange:c,setup:function(A){if(A.type)this.setValue(A.type);},commit:function(A){A.type=this.getValue();}},{type:'vbox',id:'urlOptions',children:[{type:'hbox',widths:['25%','75%'],children:[{id:'protocol',type:'select',label:a.lang.common.protocol,'default':'http://',style:'width : 100%;',items:[['http://'],['https://'],['ftp://'],['news://'],['<other>','']],setup:function(A){if(A.url)this.setValue(A.url.protocol||'');
},commit:function(A){if(!A.url)A.url={};A.url.protocol=this.getValue();}},{type:'text',id:'url',label:a.lang.common.url,onLoad:function(){this.allowOnChange=true;},onKeyUp:function(){var F=this;F.allowOnChange=false;var A=F.getDialog().getContentElement('info','protocol'),B=F.getValue(),C=/^(http|https|ftp|news):\/\/(?=.)/gi,D=/^((javascript:)|[#\/\.])/gi,E=C.exec(B);if(E){F.setValue(B.substr(E[0].length));A.setValue(E[0].toLowerCase());}else if(D.test(B))A.setValue('');F.allowOnChange=true;},onChange:function(){if(this.allowOnChange)this.onKeyUp();},validate:function(){var A=this.getDialog();if(A.getContentElement('info','linkType')&&A.getValueOf('info','linkType')!='url')return true;if(this.getDialog().fakeObj)return true;var B=CKEDITOR.dialog.validate.notEmpty(a.lang.link.noUrl);return B.apply(this);},setup:function(A){var C=this;C.allowOnChange=false;if(A.url)C.setValue(A.url.url);C.allowOnChange=true;var B=C.getDialog().getContentElement('info','linkType');if(B&&B.getValue()=='url')C.select();},commit:function(A){if(!A.url)A.url={};A.url.url=this.getValue();this.allowOnChange=false;}}],setup:function(A){if(!this.getDialog().getContentElement('info','linkType'))this.getElement().show();}},{type:'button',id:'browse',hidden:'true',filebrowser:'info:url',label:a.lang.common.browseServer,style:"display:none;"}]},{type:'vbox',id:'anchorOptions',width:260,align:'center',padding:0,children:[{type:'html',id:'selectAnchorText',html:CKEDITOR.tools.htmlEncode(a.lang.link.selectAnchor),setup:function(A){if(A.anchors.length>0)this.getElement().show();else this.getElement().hide();}},{type:'html',id:'noAnchors',style:'text-align: center;',html:'<div>'+CKEDITOR.tools.htmlEncode(a.lang.link.noAnchors)+'</div>',setup:function(A){if(A.anchors.length<1)this.getElement().show();else this.getElement().hide();}},{type:'hbox',id:'selectAnchor',children:[{type:'select',id:'anchorName','default':'',label:a.lang.link.anchorName,style:'width: 100%;',items:[['']],setup:function(A){var D=this;D.clear();D.add('');for(var B=0;B<A.anchors.length;B++){if(A.anchors[B].name)D.add(A.anchors[B].name);}if(A.anchor)D.setValue(A.anchor.name);var C=D.getDialog().getContentElement('info','linkType');if(C&&C.getValue()=='email')D.focus();},commit:function(A){if(!A.anchor)A.anchor={};A.anchor.name=this.getValue();}},{type:'select',id:'anchorId','default':'',label:a.lang.link.anchorId,style:'width: 100%;',items:[['']],setup:function(A){var C=this;C.clear();C.add('');for(var B=0;B<A.anchors.length;B++){if(A.anchors[B].id)C.add(A.anchors[B].id);
}if(A.anchor)C.setValue(A.anchor.id);},commit:function(A){if(!A.anchor)A.anchor={};A.anchor.id=this.getValue();}}],setup:function(A){if(A.anchors.length>0)this.getElement().show();else this.getElement().hide();}}],setup:function(A){if(!this.getDialog().getContentElement('info','linkType'))this.getElement().hide();}},{type:'vbox',id:'emailOptions',padding:1,children:[{type:'text',id:'emailAddress',label:a.lang.link.emailAddress,validate:function(){var A=this.getDialog();if(!A.getContentElement('info','linkType')||A.getValueOf('info','linkType')!='email')return true;var B=CKEDITOR.dialog.validate.notEmpty(a.lang.link.noEmail);return B.apply(this);},setup:function(A){if(A.email)this.setValue(A.email.address);var B=this.getDialog().getContentElement('info','linkType');if(B&&B.getValue()=='email')this.select();},commit:function(A){if(!A.email)A.email={};A.email.address=this.getValue();}},{type:'text',id:'emailSubject',label:a.lang.link.emailSubject,setup:function(A){if(A.email)this.setValue(A.email.subject);},commit:function(A){if(!A.email)A.email={};A.email.subject=this.getValue();}},{type:'textarea',id:'emailBody',label:a.lang.link.emailBody,rows:3,'default':'',setup:function(A){if(A.email)this.setValue(A.email.body);},commit:function(A){if(!A.email)A.email={};A.email.body=this.getValue();}}],setup:function(A){if(!this.getDialog().getContentElement('info','linkType'))this.getElement().hide();}}]},{id:'target',label:a.lang.link.target,title:a.lang.link.target,elements:[{type:'hbox',widths:['50%','50%'],children:[{type:'select',id:'linkTargetType',label:a.lang.link.target,'default':'notSet',style:'width : 100%;',items:[[a.lang.link.targetNotSet,'notSet'],[a.lang.link.targetFrame,'frame'],[a.lang.link.targetPopup,'popup'],[a.lang.link.targetNew,'_blank'],[a.lang.link.targetTop,'_top'],[a.lang.link.targetSelf,'_self'],[a.lang.link.targetParent,'_parent']],onChange:b,setup:function(A){if(A.target)this.setValue(A.target.type);},commit:function(A){if(!A.target)A.target={};A.target.type=this.getValue();}},{type:'text',id:'linkTargetName',label:a.lang.link.targetFrameName,'default':'',setup:function(A){if(A.target)this.setValue(A.target.name);},commit:function(A){if(!A.target)A.target={};A.target.name=this.getValue();}}]},{type:'vbox',width:260,align:'center',padding:2,id:'popupFeatures',children:[{type:'html',html:CKEDITOR.tools.htmlEncode(a.lang.link.popupFeatures)},{type:'hbox',children:[{type:'checkbox',id:'resizable',label:a.lang.link.popupResizable,setup:p,commit:s},{type:'checkbox',id:'status',label:a.lang.link.popupStatusBar,setup:p,commit:s}]},{type:'hbox',children:[{type:'checkbox',id:'location',label:a.lang.link.popupLocationBar,setup:p,commit:s},{type:'checkbox',id:'toolbar',label:a.lang.link.popupToolbar,setup:p,commit:s}]},{type:'hbox',children:[{type:'checkbox',id:'menubar',label:a.lang.link.popupMenuBar,setup:p,commit:s},{type:'checkbox',id:'fullscreen',label:a.lang.link.popupFullScreen,setup:p,commit:s}]},{type:'hbox',children:[{type:'checkbox',id:'scrollbars',label:a.lang.link.popupScrollBars,setup:p,commit:s},{type:'checkbox',id:'dependent',label:a.lang.link.popupDependent,setup:p,commit:s}]},{type:'hbox',children:[{type:'text',widths:['30%','70%'],labelLayout:'horizontal',label:a.lang.link.popupWidth,id:'width',setup:p,commit:s},{type:'text',labelLayout:'horizontal',widths:['55%','45%'],label:a.lang.link.popupLeft,id:'left',setup:p,commit:s}]},{type:'hbox',children:[{type:'text',labelLayout:'horizontal',widths:['30%','70%'],label:a.lang.link.popupHeight,id:'height',setup:p,commit:s},{type:'text',labelLayout:'horizontal',label:a.lang.link.popupTop,widths:['55%','45%'],id:'top',setup:p,commit:s}]}]}]},{id:'upload',label:a.lang.link.upload,title:a.lang.link.upload,hidden:true,filebrowser:'uploadButton',elements:[{type:'file',id:'upload',label:a.lang.common.upload,style:'height:40px',size:29},{type:'fileButton',id:'uploadButton',label:a.lang.common.uploadSubmit,filebrowser:'info:url','for':['upload','upload']}]},{id:'advanced',label:a.lang.link.advanced,title:a.lang.link.advanced,elements:[{type:'vbox',padding:1,children:[{type:'hbox',widths:['45%','35%','20%'],children:[{type:'text',id:'advId',label:a.lang.link.id,setup:q,commit:t},{type:'select',id:'advLangDir',label:a.lang.link.langDir,'default':'',style:'width:110px',items:[[a.lang.link.langDirNotSet,''],[a.lang.link.langDirLTR,'ltr'],[a.lang.link.langDirRTL,'rtl']],setup:q,commit:t},{type:'text',id:'advAccessKey',width:'80px',label:a.lang.link.acccessKey,maxLength:1,setup:q,commit:t}]},{type:'hbox',widths:['45%','35%','20%'],children:[{type:'text',label:a.lang.link.name,id:'advName',setup:q,commit:t},{type:'text',label:a.lang.link.langCode,id:'advLangCode',width:'110px','default':'',setup:q,commit:t},{type:'text',label:a.lang.link.tabIndex,id:'advTabIndex',width:'80px',maxLength:5,setup:q,commit:t}]}]},{type:'vbox',padding:1,children:[{type:'hbox',widths:['45%','55%'],children:[{type:'text',label:a.lang.link.advisoryTitle,'default':'',id:'advTitle',setup:q,commit:t},{type:'text',label:a.lang.link.advisoryContentType,'default':'',id:'advContentType',setup:q,commit:t}]},{type:'hbox',widths:['45%','55%'],children:[{type:'text',label:a.lang.link.cssClasses,'default':'',id:'advCSSClasses',setup:q,commit:t},{type:'text',label:a.lang.link.charset,'default':'',id:'advCharset',setup:q,commit:t}]},{type:'hbox',children:[{type:'text',label:a.lang.link.styles,'default':'',id:'advStyles',setup:q,commit:t}]}]}]}],onShow:function(){var G=this;
G.fakeObj=false;var A=G.getParentEditor(),B=A.getSelection(),C=B.getRanges(),D=null,E=G;if(C.length==1){var F=C[0].getCommonAncestor(true);D=F.getAscendant('a',true);if(D&&D.getAttribute('href'))B.selectElement(D);else if((D=F.getAscendant('img',true))&&D.getAttribute('_cke_real_element_type')&&D.getAttribute('_cke_real_element_type')=='anchor'){G.fakeObj=D;D=A.restoreRealElement(G.fakeObj);B.selectElement(G.fakeObj);}else D=null;}G.setupContent(n.apply(G,[A,D]));},onOk:function(){var A={href:'javascript:void(0)/*'+CKEDITOR.tools.getNextNumber()+'*/'},B=[],C={href:A.href},D=this,E=this.getParentEditor();this.commitContent(C);switch(C.type||'url'){case 'url':var F=C.url&&C.url.protocol!=undefined?C.url.protocol:'http://',G=C.url&&C.url.url||'';A._cke_saved_href=G.indexOf('/')===0?G:F+G;break;case 'anchor':var H=C.anchor&&C.anchor.name,I=C.anchor&&C.anchor.id;A._cke_saved_href='#'+(H||I||'');break;case 'email':var J,K=C.email,L=K.address;switch(w){case '':case 'encode':var M=encodeURIComponent(K.subject||''),N=encodeURIComponent(K.body||''),O=[];M&&O.push('subject='+M);N&&O.push('body='+N);O=O.length?'?'+O.join('&'):'';if(w=='encode'){J=["javascript:void(location.href='mailto:'+",z(L)];O&&J.push("+'",v(O),"'");J.push(')');}else J=['mailto:',L,O];break;default:var P=L.split('@',2);K.name=P[0];K.domain=P[1];J=['javascript:',y(K)];}A._cke_saved_href=J.join('');break;}if(C.target)if(C.target.type=='popup'){var Q=["window.open(this.href, '",C.target.name||'',"', '"],R=['resizable','status','location','toolbar','menubar','fullscreen','scrollbars','dependent'],S=R.length,T=function(ad){if(C.target[ad])R.push(ad+'='+C.target[ad]);};for(var U=0;U<S;U++)R[U]=R[U]+(C.target[R[U]]?'=yes':'=no');T('width');T('left');T('height');T('top');Q.push(R.join(','),"'); return false;");A._cke_pa_onclick=Q.join('');}else{if(C.target.type!='notSet'&&C.target.name)A.target=C.target.name;else B.push('target');B.push('_cke_pa_onclick','onclick');}if(C.adv){var V=function(ad,ae){var af=C.adv[ad];if(af)A[ae]=af;else B.push(ae);};if(this._.selectedElement)V('advId','id');V('advLangDir','dir');V('advAccessKey','accessKey');V('advName','name');V('advLangCode','lang');V('advTabIndex','tabindex');V('advTitle','title');V('advContentType','type');V('advCSSClasses','class');V('advCharset','charset');V('advStyles','style');}if(!this._.selectedElement){var W=E.getSelection(),X=W.getRanges();if(X.length==1&&X[0].collapsed){var Y=new CKEDITOR.dom.text(A._cke_saved_href,E.document);X[0].insertNode(Y);
X[0].selectNodeContents(Y);W.selectRanges(X);}var Z=new CKEDITOR.style({element:'a',attributes:A});Z.type=CKEDITOR.STYLE_INLINE;Z.apply(E.document);if(C.adv&&C.adv.advId){var aa=this.getParentEditor().document.$.getElementsByTagName('a');for(U=0;U<aa.length;U++){if(aa[U].href==A.href){aa[U].id=C.adv.advId;break;}}}}else{var ab=this._.selectedElement;if(CKEDITOR.env.ie&&A.name!=ab.getAttribute('name')){var ac=new CKEDITOR.dom.element('<a name="'+CKEDITOR.tools.htmlEncode(A.name)+'">',E.document);W=E.getSelection();ab.moveChildren(ac);ab.copyAttributes(ac,{name:1});ac.replace(ab);ab=ac;W.selectElement(ab);}ab.setAttributes(A);ab.removeAttributes(B);if(ab.getAttribute('name'))ab.addClass('cke_anchor');else ab.removeClass('cke_anchor');if(this.fakeObj)E.createFakeElement(ab,'cke_anchor','anchor').replace(this.fakeObj);delete this._.selectedElement;}},onLoad:function(){if(!a.config.linkShowAdvancedTab)this.hidePage('advanced');if(!a.config.linkShowTargetTab)this.hidePage('target');}};});
