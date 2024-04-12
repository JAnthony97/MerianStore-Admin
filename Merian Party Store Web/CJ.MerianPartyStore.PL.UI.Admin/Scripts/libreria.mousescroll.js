$(window).on('load', function () {
    $('.mouse-scroll').wrapInner('<div class="scroll"></div>');
    $('.mouse-scroll .scroll').wrapInner('<div class="content"></div>');

    mouseScrollCalculateSize();
    $('.mouse-scroll').mousemove(function (event) {
        var percentage = (event.pageX - $(this).offset().left) / $(this).outerWidth();
        var totalScroll = $(this).children('.scroll').children('.content').outerWidth() - $(this).outerWidth();

        $(this).children('.scroll').scrollLeft(totalScroll * percentage);
    });
});

$(window).resize(mouseScrollCalculateSize);

function mouseScrollCalculateSize() {

    $('.mouse-scroll').each(function (i, item) {
        var width = 0;
        $(item).children('.scroll').children('.content').children(':not(.hidden)').each(function (i, item) {
            width += Math.ceil($(item).outerWidth() + parseInt($(item).css('marginLeft').replace('px', '')) + parseInt($(item).css('marginRight').replace('px', '')));
        });
        $(item).children('.scroll').children('.content').css('width', width + 1);
    });
}