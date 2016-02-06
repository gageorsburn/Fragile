$('#ProfileImageUrl').on("change", function () {
    $('#ProfileImagePreview').attr("src", $(this).val());
});

$('#btnDelete').on("click", function (event) {
    event.preventDefault();

    if (confirm("Do you want to proceed with deleting this?")) { window.location = $(this).prop('href'); }
});

$('#PostAfterDatePicker').datetimepicker();