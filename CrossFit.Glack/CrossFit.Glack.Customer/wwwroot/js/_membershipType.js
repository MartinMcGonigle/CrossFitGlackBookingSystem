$(document).ready(function () {
    // Hide the "Save & Continue" button by default
    $('#saveContinueButton').hide();

    // Attach a change event handler to the radio buttons
    $('input[name="SelectedMembershipTypeId"]').change(function () {
        // Check if any radio button is selected
        var anyRadioButtonSelected = $('input[name="SelectedMembershipTypeId"]:checked').length > 0;

        // Show or hide the "Save & Continue" button based on the radio button selection
        $('#saveContinueButton').toggle(anyRadioButtonSelected);
    });

    $('.toggle-description').click(function () {
        var descriptionId = $(this).data('description-id');
        var descriptionElement = $('#description-' + descriptionId);

        descriptionElement.toggle();

        // Toggle the icon class based on the visibility of the description
        var iconElement = $(this).find('i');
        iconElement.toggleClass('fa-eye', descriptionElement.is(':hidden'));
        iconElement.toggleClass('fa-eye-slash', !descriptionElement.is(':hidden'));
    });
});