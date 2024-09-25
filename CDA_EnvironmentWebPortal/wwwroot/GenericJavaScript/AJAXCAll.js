//these function use as e.g

//var mPaginationViewModel = { LastRowID: 0, PageSize: 1, Direction: 1 };
//App.AjaxFormEncode("../../T_R/MemberShip/GetAllUsers", "POST", { mPaginationViewModel: mPaginationViewModel, Search: "Hello", PropertyTypeID: 1, OwnerTypeID: 2 }, true,
//    function (response) {
//        debugger
//
//    }
//);


var App = {

    Ajax: function (URL, Type, Data, async, callback) {

        jQuery.ajax({
            type: Type != null ? Type : "GET",
            url: URL,
            data: Data,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            cache: false,
            async: async,
            //beforeSend: function () {
            //    if (typeof beforeSendP == 'function')
            //        beforeSendP();
            //},
            success: function (response) {
                callback(response);
            },
            error: function (response) {
                callback(response);
            },
            //complete: function () {
            //    if (typeof completeP == 'function')   
            //        completeP();
            //},
            statusCode: {
                404: function () {
                    console.log('Resource not found');
                }
            }

        });
    },
    AjaxSubmitData: function (URL, Data, async, callback) {
        jQuery.ajax({
            type: "POST",
            url: URL,
            data: Data,
            contentType: "application/json; charset=utf-8",
            cache: false,
            processData: false,
            async: async,
            //beforeSend: function () {
            //    if (typeof beforeSendP == 'function')
            //        beforeSendP();
            //},
            success: function (response) {
                callback(response);
            },
            error: function (response) {
                callback(response);
            },
            //complete: function () {
            //    if (typeof completeP == 'function')
            //        completeP();
            //},
            statusCode: {
                404: function () {
                    console.log('Resource not found');
                }
            }

        });
    },

    AjaxSubmitFormData: function (URL, Data, async, callback) {
        jQuery.ajax({
            type: "POST",
            url: URL,
            data: Data,
            contentType: false,
            dataType: 'JSON',
            cache: false,
            processData: false,
            async: async,
            //beforeSend: function () {
            //    if (typeof beforeSendP == 'function')
            //        beforeSendP();
            //},
            success: function (response) {
                callback(response);
            },
          
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    callback('Not connect.\n Verify Network.');
                } else if (jqXHR.status == 404) {
                    callback('Requested page not found. [404]');
                } else if (jqXHR.status == 500) {
                    callback('Internal Server Error [500].');
                } else if (exception === 'parsererror') {
                    callback('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    callback('Time out error.');
                } else if (exception === 'abort') {
                    callback('Ajax request aborted.');
                } else {
                    callback('Uncaught Error.\n' + jqXHR.responseText);
                }
            },
            //complete: function () {
            //    if (typeof completeP == 'function')
            //        completeP();
            //},
            statusCode: {
                404: function () {
                    console.log('Resource not found');
                }
            }

        });
    },
    AjaxFormEncode: function (URL, Type, Data, async, callback) {

        jQuery.ajax({
            type: Type != null ? Type : "GET",
            url: URL,
            data: Data,
            contentType: "application/x-www-form-urlencoded",
            dataType: 'json',
            cache: false,
            async: async,
            //beforeSend: function () {
            //    if (typeof beforeSendP == 'function')
            //        beforeSendP();
            //},
            success: function (response) {
                callback(response);
            },
            error: function (response) {
                callback(response);
            },
            //complete: function () {
            //    if (typeof completeP == 'function')
            //        completeP();
            //},
            statusCode: {
                404: function () {
                    console.log('Resource not found');
                }
            }

        });
    },
    AjaxPartail: function (URL, ID) {

        jQuery.ajax({
            url: URL,
            type: "GET",
            dataType: "html",
            async: false,
            success: function (data) {
                debugger
                $("#" + ID).html('');
                $("#" + ID).html(data);
            },
            error: function (data) {
            }
        });
    },
    AjaxPartailWithData: function (URL, ID, Data) {
        jQuery.ajax({
            url: URL,
            type: "GET",
            dataType: "html",
            data: Data,
            async: false,
            success: function (data) {
                debugger
                $("#" + ID).html('');
                $("#" + ID).html(data);
            },
            error: function (data) {
            }

        });
    },
    AjaxPartailWithResponse: function (URL, ID, Data,callback) {
        jQuery.ajax({
            url: URL,
            type: "GET",
            dataType: "html",
            data: Data,
            async: false,
            success: function (data) {
                $("#" + ID).html('');
                $("#" + ID).html(data);
            },
            error: function (data) {
            }

        });
    }


}


