$(document).ready(function () {

    $(".tweet_send").click(function () {
        var text = $('#Tweet_Textarea').val();
        $('.input_rightCol').slideToggle();
        $.ajax({
            url: '/tweet/CreateTweet',
            type: 'POST',
            data: {
                Text: text
            },
            success: function (result) {
                tweet = $(result).hide().delay(128).show('fast');
                $('#Tweets').prepend(tweet);
                $('#Tweet_Textarea').val('');
                $('.input_rightCol').slideToggle();
            }
        });
    });
    
    $('#Tweets').on('click', '.tweet_delete', function () {
        var tweet = $(this).closest('.tweetRow');
        var tweetID = parseInt($(this).closest('.post_rightCol_header_col3').find('.tweetID').val());
        $.ajax({
            url: '/tweet/DeleteTweet',
            type: 'POST',
            data: {
                ID: tweetID
            },
            success: function () {
                $(tweet).hide('fast', function() {$(this).remove()});
            }
        });
    });

});