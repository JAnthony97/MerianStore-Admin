function ImageInput(inputId, className, url, callback) {
    this.id = inputId;

    if (url !== null)
        $('#' + this.id).wrap('<div class="image-input ' + className + '" style="background-image: url(\'' + url + '\');"></div>');
    else
        $('#' + this.id).wrap('<div class="image-input ' + className + '"></div>');

    var imageInput = this;
    $('#' + this.id).parent().click(function () {
        $('#' + imageInput.id).click(function (event) {
            event.stopPropagation();
        })
        $('#' + imageInput.id).click();
    });

    $('#' + this.id).change(function () {
        if (document.getElementById(imageInput.id).files.length > 1) {
            var images = document.getElementById(imageInput.id).files;
            $.each(images, function (index, file) {

                var imageUrl = window.URL.createObjectURL(file);

                if (callback == null) {
                    $(this).parent().attr('style', 'background-image: url("' + imageUrl + '");');
                }
                else
                    callback(imageUrl);
            });
        } else {
            var file = document.getElementById(imageInput.id).files[0];

            var imageUrl = window.URL.createObjectURL(file);

            if (callback == null) {
                $(this).parent().attr('style', 'background-image: url("' + imageUrl + '");');
            }
            else
                callback(imageUrl);
        }
    });
}

ImageInput.prototype.setImageUrl = function (imageUrl) {
    if (imageUrl !== null)
        $('#' + this.id).parent().attr('style', 'background-image: url("' + imageUrl + '");');
    else
        $('#' + this.id).parent().attr('style', '');
}