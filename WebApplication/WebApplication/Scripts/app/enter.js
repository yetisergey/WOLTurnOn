$(function () {
    $('#Submit').click(function () {
        if ($("#Login").val()!="" && $("#Password").val()!="") {
            var user = {
                Login: $("#Login").val(),
                Password: $("#Password").val()
            }
            $.ajax({
                url: "/api/EnterAPI",
                type: 'POST',
                data: JSON.stringify(user),
                contentType: 'application/json; charset=UTF-8',
                success: function () {
                    location.href = '/home/index'
                },
                error: function (e) {
                    $('#Message').text(e.responseJSON.Message);
                    setTimeout(clearMessage, 1000);
                }
            });
        }
    });
});
function clearMessage() {
    $('#Message').text("");
}