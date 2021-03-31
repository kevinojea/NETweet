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

});