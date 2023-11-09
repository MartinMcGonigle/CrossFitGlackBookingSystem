$(document).ready(function () {
    $(".add-register-class").click(function () {

        var classId = $(this).data("id");
        var startDate = $(this).data("startdate");
        var endDate = $(this).data("enddate");
        var startTime = $(this).data("starttime");
        var endTime = $(this).data("endtime");

        $.ajax({
            type: "POST",
            url: "/Class/ReserveClass",
            data: { classId: classId },
            success: function (data) {
                if (data.success === true) {
                    $('#notificationModal .modal-title').text("Class Registration");
                    $('#notificationModal .modal-body p').text(`Successfully registered for class at ${startDate}  ${startTime} - ${endDate}  ${endTime}`);
                } else {
                    $('#notificationModal .modal-title').text("Class Registration");
                    $('#notificationModal .modal-body p').text(`There was a problem registering for class at ${startDate}  ${startTime} - ${endDate}  ${endTime}`);
                    $('#notificationModal .modal-header').css('background-color', 'red');
                    $('#notificationModal .modal-header').css('color', 'white');
                }
            },
            error: function () {
                $('#notificationModal .modal-title').text("Class Registration");
                $('#notificationModal .modal-body p').text(`There was a problem registering for class at ${startDate}  ${startTime} - ${endDate}  ${endTime}`);
                $('#notificationModal .modal-header').css('background-color', 'red');
                $('#notificationModal .modal-header').css('color', 'white');
            }
        });

        $("#notificationModal").modal("show");

        $("#notificationModal").on("hidden.bs.modal", function () {
            location.reload();
        });
    });

    $(".cancel-class-reservation").click(function () {
        var startDate = $(this).data("startdate");
        var endDate = $(this).data("enddate");
        var startTime = $(this).data("starttime");
        var endTime = $(this).data("endtime");
        var reservationId = $(this).data("reservationid");

        $.ajax({
            type: "POST",
            url: "/Class/DeleteClassReservation",
            data: { reservationId: reservationId },
            success: function (data) {
                if (data.success === true) {
                    $('#notificationModal .modal-title').text("Cancel Reservation");
                    $('#notificationModal .modal-body p').text(`Your reservation for class  at ${startDate}  ${startTime} - ${endDate}  ${endTime} has been canceled`);
                } else {
                    $('#notificationModal .modal-title').text("Cancel Reservation");
                    $('#notificationModal .modal-body p').text(`There was a problem canceling your reservation for class  at ${startDate}  ${startTime} - ${endDate}  ${endTime}`);
                    $('#notificationModal .modal-header').css('background-color', 'red');
                    $('#notificationModal .modal-header').css('color', 'white');
                }
            },
            error: function () {
                $('#notificationModal .modal-title').text("Cancel Reservation");
                $('#notificationModal .modal-body p').text(`There was a problem canceling your reservation for class  at ${startDate}  ${startTime} - ${endDate}  ${endTime}`);
                $('#notificationModal .modal-header').css('background-color', 'red');
                $('#notificationModal .modal-header').css('color', 'white');
            }
        });

        $("#notificationModal").modal("show");

        $("#notificationModal").on("hidden.bs.modal", function () {
            location.reload();
        });
    });
});