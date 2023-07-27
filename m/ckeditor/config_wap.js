/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    config.uiColor = '#C9DEFA';

    config.language = 'zh-cn'; //中文
    config.font_names = '宋体;楷体_GB2312;新宋体;黑体;隶书;幼圆;微软雅黑;Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana';
    config.scaytautoStartup = false; //取消拼写检查
    config.toolbar_Full =
    [
        ['Bold', 'Italic', 'Underline', 'Strike'],
        ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote'], 
        ['Image', 'Table', 'SpecialChar'],
        ['TextColor', 'BGColor'],
        ['ShowBlocks']
    ];
    config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹
    config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?Type=Images'; //上传图片时浏览服务文件夹
    config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?Type=Flash';  //上传Flash时浏览服务文件夹
    config.filebrowserUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)
    config.filebrowserImageUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签)
    config.filebrowserFlashUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)
};
