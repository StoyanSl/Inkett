
$(document).ready(function () {
    $('#followButton').click(function () {
        $("#followButton").toggleClass("liked");
        let profileId = $('#profileId').val();
        if ($(".button-like").hasClass("liked")) {
            $.ajax({
                url: "/FollowProfile",
                type: 'POST',
                dataType:'html',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("RequestVerificationToken",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { profileId: profileId }
            });
        }
        else {
            $.ajax({
                url: "/UnFollowProfile",
                type: 'POST',
                dataType: 'html',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("RequestVerificationToken",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { profileId: profileId }
            });
        }
    });
});
