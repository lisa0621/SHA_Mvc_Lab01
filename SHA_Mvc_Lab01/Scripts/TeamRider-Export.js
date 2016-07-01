;
(function (windows) {
    if (typeof (jQuery) === 'undefined') { alert('jQuery Library NotFound.'); return; }

    var HasData = 'False';

    $(function () {

        //顯示匯出選項視窗
        $('#ButtonExport').click(function () {
            $('#ExportDataDialog').modal('show');
        });

        $('#SelectAllColumns').unbind('click').click(function () {
            //選取全部欄位
            $('input:checkbox[name=Checkbox_ExportColumns]').prop('checked', 'checked');
        });

        $('#UnselectAllColumns').unbind('click').click(function () {
            //不選取全部欄位
            $('input:checkbox[name=Checkbox_ExportColumns]').removeAttr('checked');
        });

        //匯出資料
        $('#ButtonExecuteExport').click(function () {
            //匯出 Excel 檔名
            var exportFileName = $.trim($('#ExportFileName').val());

            //匯出自訂檔名
            //ExportData(exportFileName);

            //匯出的資料欄位
            var selectedColumns = $('input:checkbox[name=Checkbox_ExportColumns]:checked').map(function () {
                return $(this).val();
            }).get().join(',');

            if (selectedColumns.length == 0) {
                alert("必須選取匯出資料的欄位.");
                return false;
            }

            //匯出 sortOrder
            var sortOrder = getParameterByName('sortOrder');

            //匯出 searchString
            var searchString = $.trim($('#SearchString').val());

            ExportSelectedData(exportFileName, selectedColumns, sortOrder, searchString);
        });

    });

    function AlertErrorMessage(title, message) {
        /// <summary>
        /// 使用 Bootbox.js 的錯誤訊息顯示
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>

        bootbox.dialog({
            title: title,
            message: message,
            buttons: {
                danger: {
                    label: "確認",
                    className: "btn-danger"
                }
            }
        });
    }

    function ExportData(exportFileName) {
        /// <summary>
        /// 資料匯出自訂檔名
        /// </summary>

        $.ajax({
            type: 'post',
            url: Router.action('Team_Rider', 'HasData'),
            dataType: 'json',
            cache: false,
            async: false,
            success: function (data) {
                if (data.Msg) {
                    HasData = data.Msg;
                    if (HasData == 'False') {
                        AlertErrorMessage("錯誤", "尚未建立任何資料, 無法匯出資料.");
                    }
                    else {
                        window.location = exportFileName.length == 0
                            ? Router.action('Team_Rider', 'ExportCustomFilename')
                            : Router.action('Team_Rider', 'ExportCustomFilename', { fileName: exportFileName });

                        $('#ExportFileName').val('');
                        $('#ExportDataDialog').modal('hide');
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                AlertErrorMessage("錯誤", "資料匯出錯誤");
            }
        });
    }

    function ExportSelectedData(exportFileName, selectedColumns, sortOrder, searchString) {
        /// <summary>
        /// 資料匯出選擇的欄位
        /// </summary>

        $.ajax({
            type: 'post',
            url: Router.action('Team_Rider', 'HasData'),
            dataType: 'json',
            cache: false,
            async: false,
            success: function (data) {
                if (data.Msg) {
                    HasData = data.Msg;
                    if (HasData == 'False') {
                        AlertErrorMessage("錯誤", "尚未建立任何資料, 無法匯出資料.");
                    }
                    else {
                        window.location = exportFileName.length == 0
                            ? Router.action('Team_Rider', 'ExportSelectedColumns', { selectedColumns: selectedColumns, sortOrder: sortOrder, searchString: searchString })
                            : Router.action('Team_Rider', 'ExportSelectedColumns', { fileName: exportFileName, selectedColumns: selectedColumns, sortOrder: sortOrder, searchString: searchString });

                        $('#ExportFileName').val('');
                        $('#ExportDataDialog').modal('hide');
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                AlertErrorMessage("錯誤", "資料匯出錯誤");
            }
        });
    }

    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
})
(window);