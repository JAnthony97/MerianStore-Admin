function ColorPicker(id, callback) {
    this.id = id;
    this.callback = callback;
    this.selectedColor;

    $('#' + id).addClass('color-picker');
    $('#' + id).append('<div class="content"><div class="color-wheel"><img class="wheel" id="wheel-' + id + '" src="' + getRootUrl() + 'Images/Controls/color_wheel.svg" /></div><div class="color-result"></div></div>');

    var img = document.getElementById('wheel-' + id);
    var canvas = document.createElement('canvas');
    canvas.width = img.width * 8;
    canvas.height = img.height * 8;

    $('#' + id).click(function (event) {
        if (!$(event.target).is('#wheel-' + id)) {
            if (!$(this).hasClass('active')) {
                $(this).find('.color-result').css('background-color', $(this).css('background-color'));
                $(this).addClass('active');

                $('.color-tag').each(function (index) {
                    if (('colorTag-' + index) !== id)
                    $('#colorTag-' + index).removeClass('active');  
                });

                canvas.getContext('2d').drawImage(img, 0, 0, img.width * 8, img.height * 8);
            }
            else
                $(this).removeClass('active');
        }
    });

    var colorPicker = this;
    $('#wheel-' + id).mousemove(function (event) {
        var pixelData = canvas.getContext('2d').getImageData(event.offsetX, event.offsetY, 1, 1).data;
        var r = rgbToHex(pixelData[0]);
        var g = rgbToHex(pixelData[1]);
        var b = rgbToHex(pixelData[2]);

        colorPicker.selectedColor = '#' + r + g + b;

        $('#' + id).find('.color-result').css('background-color', colorPicker.selectedColor);
    });

    $('#wheel-' + id).click(function (event) {
        if ($('#' + id).hasClass('active')) {
            $('#' + id).removeClass('active');
            colorPicker.callback(colorPicker.selectedColor);
        }
    });
}

function rgbToHex(rgb) {
    var hex = Number(rgb).toString(16);
    if (hex.length < 2) {
        hex = "0" + hex;
    }
    return hex;
};