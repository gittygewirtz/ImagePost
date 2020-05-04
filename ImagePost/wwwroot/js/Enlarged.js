$(() => {

    setInterval(() => {
        updateLikes();
    }, 500);

    $("#images").on('click', '#like-btn', function () {
        let imgId = $("#imgId").val();
        $.post('/Home/AddLike', { imgId }, function () {
            updateLikes();
            $("#like-btn").prop('disabled', true);
        });
    });    

    function updateLikes() {
        let imgId = $("#imgId").val();
        console.log(imgId);
        $.get('/Home/GetLikes', {imgId}, result => {
            $("#like-span").text(result);
        });
    };
});