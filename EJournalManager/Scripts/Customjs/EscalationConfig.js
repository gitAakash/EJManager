$(document).ready(function () {
    var SubDomain = $("#SubDomain").val();
    $.get(SubDomain + "Configuration/GetEsacalationInterval", function (data) {
        $("#hidIntervals").val(data);
    });
    var option = [];
    //remove the roles from dropdown which are alredy there in table
    if ($('#escalationTable tr').length > 1) {

        $(".RoleIds").each(function () {
            option.push($(this).val());
        });

        for (var i = 0; i < option.length; i++) {
            $("#Role option[value=" + option[i] + ']').remove();
        }

    }
    $("#btn_AddRole").click(function (e) {

        e.preventDefault();
        //Add role to the grid on click of add button
        if ($("#Role").val() == "") {
            alert("Please select Role first");
        }
        else {
            if ($('#escalationTable tr').length <= 4) {
                var role = $("#Role option:selected").text();
                var roleId = $("#Role option:selected").val();
                var optionValues = "<option value=''>Select</option>";
                $("#Role option[value=" + $("#Role").val() + ']').remove();//roles should not get repeated
                //var optionValues = "<option value=''>Select</option><option value='0'>0</option><option value='20'>20</option><option value='40'>40</option><option value='60'>60</option><option value='80'>80</option>";

                var arr = $("#hidIntervals").val().split(',');
                $.each(arr, function (index, value) {
                    optionValues += "<option value='" + value + "'>" + value + "</option>";
                });

                var tablerow = "<tr><td>" + role + "<input type='hidden' value='" + roleId + "'/></td><td><div class='col-sm-10 col-md-3'><select name='Interval' class='form-control interval'>" + optionValues + "</select></div></td>";
                $('#escalationTable tr:last').after(tablerow);
                if ($('#escalationTable tr').length == 2) {
                    $(".interval").val("0");//set default intervel level for first role as 0
                }
            }
            else {
                alert("Maximum levels are reached");// allow assinging issue upto 4 roles only
            }
        }
    });


    $(".interval").live('change', function (e) {

        //validation to check Inteval level of currents selected role is greater than its previous roles
        var intervals = [];
        var error = false;
        $(".interval option:selected").each(function () {
            if ($(this).val() != "")
                intervals.push($(this).val());
        });
        var selectedValue = $(this).val();

        for (var i = 0; i <= intervals.length - 2; i++) {
            if (intervals[i + 1] <= intervals[i]) {
                error = true;
            }
        }

        if (error == true) {
            alert("Please select Interval greater than previous levels");

        }
    });

    $("#btn_Submit").click(function () {
        var isEditMode = false;
        if (option.length > 0) {
            isEditMode = true;
        }
        if ($('#escalationTable tr').length < 2) {
            alert("Please add escalation levels");
            return false;
        }
        else {
            var intervals = [];
            var selectValueError = false;
            var error = false;
            $(".interval option:selected").each(function () {
                if ($(this).val() != "")
                    intervals.push($(this).val());
                else
                    selectValueError = true
            });


            for (var i = 0; i <= intervals.length - 1; i++) {
                if (intervals[i + 1] <= intervals[i]) {
                    error = true;
                }
            }
            if (error == true) {
                alert("Please select Interval greater than previous levels");
                return false;

            }
            if (selectValueError == true) {
                alert("Please select all Interval levels");
                return false;
            }

            //post the list of escalation model
            var escalationModel = [];
            $("#escalationTable tr").each(function (i, item) {

                if (i > 0) {
                    var curRow = {};
                    curRow.RoleId = $(this).find('td').eq(0).find('input[type="hidden"]').val();
                    curRow.IntervalLevel = $(this).find('td').eq(1).find(".interval option:selected").val();
                    escalationModel.push(curRow);
                }
            })


            escalationModel = JSON.stringify({ 'escalationModel': escalationModel, 'isEditMode': isEditMode });

            $.ajax({
                url: SubDomain + 'Configuration/SaveEscalationConfig',
                type: 'POST',
                data: escalationModel,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert("Record added successfully");
                    location.reload();
                }


            });
        }
    });

    $("#btn_reset").click(function () {

        //bind the dropdwon for roles and remove the rows from table
        $.ajax({
            type: 'POST',
            url: SubDomain + "Configuration/GetRoles",
            async: false,
            dataType: "json",
            success: function (data) {
                $("#Role").empty();
                $("#Role").append("<option value=''>Select</option>");
                $.each(data, function (index, Option) {
                    $("#Role").append("<option value='" + Option.Id + "'>" + Option.Name + "</option>");
                });

            }
        });

        $("#escalationTable").find("tr:gt(0)").remove();
    });

});