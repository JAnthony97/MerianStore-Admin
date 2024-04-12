$(document).ready(function () {
    $('#btnMenu').click(function () {
        if ($('#pnlMenu').hasClass('active'))
            $('#pnlMenu').removeClass('active');
        else
            $('#pnlMenu').addClass('active');
    });
});