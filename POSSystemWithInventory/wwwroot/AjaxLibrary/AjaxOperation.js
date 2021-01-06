

class AjaxOperation {
    constructor() { }
    SaveAjax(destination, jsonData) {
        let data;
        $.ajax({
            url: destination,
            type: "post",
            data: jsonData,
            async: false,
            processData: false,
            contentType: false,
            success: function (response) {
                data = response;
            }
        });
        return data;
    }

    DeleteAjaxById(destination, jsonData) {
        let data;
        $.ajax({
            url: destination,
            data: ({ "id": jsonData }),
            async: false,
            success: function (response) {
                data = response;
            }
        });
        return data;
    }
}