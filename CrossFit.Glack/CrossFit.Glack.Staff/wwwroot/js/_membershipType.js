$(document).ready(function () {
    $("input[name='Type']").change(function () {
        radioButtonChange();
    });

    radioButtonChange();
});

function radioButtonChange() {
    const selectedRadioButton = $("input[name='Type']:checked");

    if (selectedRadioButton.length > 0) {
        const selectedValue = selectedRadioButton.val();

        if (selectedValue === '1') {
            $("#numberOfClassesContainer").removeClass("collapse");
            $("#numberOfMonthsContainer").addClass("collapse");

            $("#NumberOfClasses").prop('required', true);
            $("#NumberOfMonths").prop('required', false);

            $("#NumberOfMonths").val("");
        } else if (selectedValue === '2') {
            $("#numberOfClassesContainer").removeClass("collapse");
            $("#numberOfMonthsContainer").removeClass("collapse");

            $("#NumberOfClasses").prop('required', true);
            $("#NumberOfMonths").prop('required', true);
        } else {
            $("#numberOfClassesContainer").addClass("collapse");
            $("#numberOfMonthsContainer").addClass("collapse");

            $("#NumberOfClasses").prop('required', false);
            $("#NumberOfMonths").prop('required', false);

            $("#NumberOfClasses").val("");
            $("#NumberOfMonths").val("");
        }
    } else {
        $("#numberOfClassesContainer").addClass("collapse");
        $("#numberOfMonthsContainer").addClass("collapse");

        $("#NumberOfClasses").prop('required', false);
        $("#NumberOfMonths").prop('required', false);

        $("#NumberOfClasses").val("");
        $("#NumberOfMonths").val("");
    }
}