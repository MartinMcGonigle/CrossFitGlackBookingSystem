$(document).ready(function () {
    $("#Input_Role").change(function () { roleChange(); });
    roleChange();
});

function roleChange() {
    var selectedRole = $("#Input_Role option:selected").text();

    if (selectedRole === 'Customer') {
        $("#membershipTypeContainer").removeClass("collapse");
        $("#Input_MembershipTypeId").prop('required', true);
    } else {
        $("#membershipTypeContainer").addClass("collapse");
        $("#Input_MembershipTypeId").prop('required', false);
        $("#Input_MembershipTypeId").val("");
    }
}