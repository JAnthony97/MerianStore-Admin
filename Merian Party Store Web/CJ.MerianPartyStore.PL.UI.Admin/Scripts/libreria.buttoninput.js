function ButtonInput(id, action) {
    $('#' + id).click(function () {
        var x = event.clientX + $(document).scrollLeft() - $(this).offset().left;
        var y = event.clientY + $(document).scrollTop() - $(this).offset().top;

        var minX = $(this).outerWidth() - 40;
        var maxX = $(this).outerWidth();
        var minY = 0;
        var maxY = $(this).outerHeight();

        if (x >= minX && x <= maxX &&
            y >= minY && y <= maxY) {
            action();
            event.preventDefault();
        }
    });

    $('#' + id).mousemove(function () {
        var x = event.clientX + $(document).scrollLeft() - $(this).offset().left;
        var y = event.clientY + $(document).scrollTop() - $(this).offset().top;

        var minX = $(this).outerWidth() - 40;
        var maxX = $(this).outerWidth();
        var minY = 0;
        var maxY = $(this).outerHeight();

        if (x >= minX && x <= maxX &&
            y >= minY && y <= maxY)
            $(this).css('cursor', 'pointer');
        else
            $(this).css('cursor', 'initial');
    });
}