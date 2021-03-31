$(document).ready(function () {

    $('#Tweets').on('click', '.like_btn, .dislike_btn', function() {
        var button = $(this);
        var tweetID = parseInt(button.closest('.post_rightCol_footer_col1').find('.tweetID').val());
        var icon = button.find('i');

        if ($(this).hasClass('like_btn')) {
            like_Tweet(button, icon, tweetID);
        } else {
            dislike_Tweet(button, icon, tweetID);
        }
    });

    function like_Tweet(button, icon, tweetID) {
        $.ajax({
            url: '/tweet/Like',
            type: 'POST',
            data: {
                ID: tweetID,
            },
            success: function () {
                button.toggleClass('like_btn dislike_btn');
                icon.toggleClass('far fas');
            }
        });
    }

    function dislike_Tweet(button, icon, tweetID) {
        $.ajax({
            url: '/tweet/Dislike',
            type: 'POST',
            data: {
                ID: tweetID,
            },
            success: function () {
                button.toggleClass('dislike_btn like_btn');
                icon.toggleClass('fas far');
            }
        });
    }

});