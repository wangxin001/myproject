/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config)
{
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.language = 'zh-cn';
    config.font_names = "宋体;楷体_GB2312;新宋体;黑体;隶书;幼圆;微软雅黑;Arial";
    config.skin = 'office2003';

    config.toolbar_Topic =
    [
        ['Preview'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        ['TextColor', 'BGColor'],
        ['Image', 'Table', 'HorizontalRule', 'SpecialChar', 'PageBreak'],
        ['Maximize'],
        '/',
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Format', 'Font', 'FontSize'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent']
    ];
};
