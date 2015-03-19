$(document).ready(function () {

    $('#cbxActive').change(function () {

        var $cbx = $(this),
            isChecked = $cbx.is(':checked');
        if (isChecked == true)
            $(this).attr('checked', 'checked');
        else
            $(this).removeAttr('checked');
    });

    $('#cbxSMS').change(function () {

        var $cbx = $(this),
            isChecked = $cbx.is(':checked');
        if (isChecked == true)
            $(this).attr('checked', 'checked');
        else
            $(this).removeAttr('checked');
    });
    $('#cbxEmail').change(function () {

        var $cbx = $(this),
            isChecked = $cbx.is(':checked');
        if (isChecked == true)
            $(this).attr('checked', 'checked');
        else
            $(this).removeAttr('checked');
    });
    $("#btn_Submit").click(function () {


        //post the list of hardware status model
        var hardwareStatusModel = [];
        $("#hardwareStatusTable tr").each(function (i, item) {

            if (i > 0) {
                var curRow = {};
                curRow.Name = $(this).find('td').eq(0).text();
                curRow.IsActive = $(this).find('td').eq(1).find("#cbxActive").prop("checked");
                curRow.IsAlertSMS = $(this).find('td').eq(2).find('#cbxSMS').prop("checked");
                curRow.IsAlertEmail = $(this).find('td').eq(3).find('#cbxEmail').prop("checked");
                curRow.POS = $(this).find('td').eq(3).find('#hidPos').val();
                hardwareStatusModel.push(curRow);
            }
        })


        hardwareStatusModel = JSON.stringify({ 'hardwareStatusModel': hardwareStatusModel });

        $.ajax({
            url: '/TriggerConfiguration/UpdateHardwareStatus',
            type: 'POST',
            data: hardwareStatusModel,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert("Record added successfully");
                location.reload();
            },
            error: function (data) {
                alert("Error occured while updating records");
            }


        });
    });



    $('#btnReset').click(function () {

        location.reload();
    });
});