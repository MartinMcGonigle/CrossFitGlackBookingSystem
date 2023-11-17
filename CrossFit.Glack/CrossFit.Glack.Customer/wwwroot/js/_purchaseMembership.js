$(document).ready(function () {
    var selectedMembershipType = $('input[name="selectedMembershipTypeId"]');
    var saveContinueButton = $('#saveContinueButton');

    var anyRadioButtonSelected = selectedMembershipType.is(':checked');
    saveContinueButton.toggle(anyRadioButtonSelected);

    selectedMembershipType.change(function () {
        var anyRadioButtonSelected = selectedMembershipType.is(':checked');
        saveContinueButton.toggle(anyRadioButtonSelected);
    });

    $('.toggle-description').click(function () {
        var descriptionId = $(this).data('description-id');
        var descriptionElement = $('#description-' + descriptionId);

        descriptionElement.toggle();

        var iconElement = $(this).find('i');
        iconElement.toggleClass('fa-eye', descriptionElement.is(':hidden'));
        iconElement.toggleClass('fa-eye-slash', !descriptionElement.is(':hidden'));
    });

    var selectedMembershipAutoRenew = $('input[name="selectedMembershipAutoRenew"]');
    var saveContinueButtonMembershipDetails = $('#saveContinueButtonMembershipDetails');

    var anyRadioButtonSelectedAutoRenew = selectedMembershipAutoRenew.is(':checked');
    saveContinueButtonMembershipDetails.toggle(anyRadioButtonSelectedAutoRenew);

    selectedMembershipAutoRenew.change(function () {
        var anyRadioButtonSelected = selectedMembershipAutoRenew.is(':checked');
        saveContinueButtonMembershipDetails.toggle(anyRadioButtonSelected);
    });
});