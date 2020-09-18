var day = 0;
$(document).ready(function () {
    $('.w-button').click(function () {
        day = $(this).text();
    });
    $('#SentData').click(() => {
        SaveInDb();
    }); 
});

function SaveInDb() {
    var formData = new FormData();
    var jobId = 1;
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    options.timeZone = 'UTC';
    options.timeZoneName = 'short';
    var myDate = new Date().toISOString().slice(0, 19).replace('T', ' ');
    var pad = function (num) { return ('00' + num).slice(-2) };

    var name = $('#name').val();
    var surname = $('#surname').val();
    var lastsurname = $('#lastsurname').val();
    var number = $('#number').val();
    var email = $('#email').val();
    alert(myDate);
    formData.append("terminalId", 1);
    formData.append("authCode", "0000");
    formData.append("clientId", -1 );
    formData.append("segmentId", 9);
    formData.append("setTime", myDate);
    formData.append("jobId", jobId);
    formData.append("employeeId", 0);
    formData.append("needApply", 0);
    formData.append("clientPhone", number);
    formData.append("clientEmail",email );
    formData.append("commentary","" );
    formData.append("clientName", name + surname + lastsurname);
    formData.append("information","" );
    formData.append("typeInformation", "");
    formData.append("timeInHold", myDate);
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
                alert
            },
            error: function (xhr, status, error) {
                alert("Error!");
            }
        });

}

