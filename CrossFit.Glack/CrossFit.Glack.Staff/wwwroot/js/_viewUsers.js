$(document).ready(function () {

    $("#propert-details-search").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#propert-details-table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    $(".resend-verification").click(function () {
        var id = $(this).data("id");
        $.get("Resend-Verification-Link/" + id, function (data) {
            var message = '';
            if (data.success) {
                message = 'Successfully resent verification email.';
            } else {
                message = 'Failed to resend verification email.';
            }
            $("#notification-modal .modal-body p").text(message);
            $("#notification-modal").modal("show");
        })
            .fail(function () {
                var message = 'An error occurred while resending the verification email. Please try again later.';
                $("#notification-modal .modal-body p").text(message);
                $("#notification-modal").modal("show");
            });
    });
});