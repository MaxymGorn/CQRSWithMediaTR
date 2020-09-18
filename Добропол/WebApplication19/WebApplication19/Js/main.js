$(document).ready(function () {
    $('#SentData').click(() => {
        SaveInDb();
    }); 
});

function SaveInDb() {
    var formData = new FormData();
    var jobId = $("#field option:selected").html();
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    options.timeZone = 'UTC';
    options.timeZoneName = 'short';
    var myDate = new Date().toISOString().slice(0, 19).replace('T', ' ');
    var name = $('#name').val();
    var surname = $('#surname').val();
    var lastsurname = $('#lastsurname').val();
    var number = $('#number').val();
    var email = $('#email').val();
    var selectedtime = $("#Time option:selected").html();
    formData.append("terminalId", 1);
    formData.append("authCode", "0000");
    formData.append("clientId", -1);
    formData.append("segmentId", 9);
    formData.append("setTime", myDate);
    formData.append("jobId", jobId);
    formData.append("employeeId", 0);
    formData.append("needApply", 0);
    formData.append("clientPhone", number);
    formData.append("clientEmail", email);
    formData.append("commentary", "");
    formData.append("clientName", name + surname + lastsurname);
    formData.append("information", "");
    formData.append("typeInformation", "");
    formData.append("timeInHold", selectedtime);
    formData.append("setId", 9);
    formData.append("notificationType", 0);
    formData.append("notificationEvt", 0);
    $.ajax({
        type: "POST",
        url: "/Home/SaveDb",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result, status, xhr) {

        },
        error: function (xhr, status, error) {
        }
    });

}


