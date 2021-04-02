$(document).ready(function () {

    $(".follow_btn, .unfollow_btn").click( function () {
        var button = $(this);
        var userID = button.closest('.user_rightCol, .profile_rightCol_footer').find('.userID').val();

        if ($(this).hasClass('follow_btn')) {
            follow(button, userID);
        } else {
            unfollow(button, userID);
        }
    });

    function follow(button, userID) {
        $.ajax({
            url: '/search/Follow',
            type: 'POST',
            data: {
                Id: userID,
            },
            success: function () {
                button.toggleClass('follow_btn unfollow_btn').toggleClass('btn-info btn-danger');
                $(button).html('Dejar de seguir');
            }
        });
    }

    function unfollow(button, userID) {
        $.ajax({
            url: '/search/Unfollow',
            type: 'POST',
            data: {
                Id: userID,
            },
            success: function () {
                button.toggleClass('unfollow_btn follow_btn').toggleClass('btn-danger btn-info');
                $(button).html('Seguir');
            }
        });
    }

});