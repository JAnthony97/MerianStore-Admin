var colorTagIndex = 0;
function TagInput(id, type, callback) {
    this.inputId = id + '-taginput';
    this.id = id;
    this.type = type;
    this.callback = callback;

    $('#' + id).wrap('<div class="tag-input"></div>');
    $('#' + id).parent().append('<div class="content"><div class="tags"></div><span class="edit" contenteditable="true"></span></div>');
    var name = $('#' + id).attr('name');
    var className = $('#' + id).attr('class');
    $('#' + id).attr('name', '');
    $('#' + id).attr('class', '');
    $('#' + id).addClass('hidden');
    $('#' + id).parent().append('<input type="hidden" class="' + className + '" name="' + name + '" id="' + id + '" value="' + $('#' + id).val().trim() + '" />');
    $('#' + id).attr('id', '');

    var tagInput = this;
    $('#' + id).parent().find('.edit').keydown(function (event) {
        if (event.keyCode == 13 || event.keyCode == 9) {
            tagInput.addTag();
        }
        else if (event.keyCode == 8) {
            if ($(this).html() == '') {
                if ($('#' + id).parent().find('.tag:last-child').hasClass('selected'))
                    tagInput.removeTag($('#' + id).parent().find('.tag:last-child'));
                else
                    $('#' + id).parent().find('.tag:last-child').addClass('selected')
            }
        }
    });
    $('#' + id).parent().find('.edit').keypress(function (event) {
        if (event.which == 44 || event.which == 59) {
            tagInput.addTag();
        }
    });
    $('#' + id).parent().find('.edit').focusout(function (event) {
        tagInput.addTag();
    });
    $('#' + id).parent().find('.content').click(function (event) {
        if ($(event.target).is(this)) {
            setTimeout(function () {
                $('#' + id).parent().find('.edit').get(0).focus();
            }, 100);
        }
    });

    var values = $('#' + this.id).val().trim().split(',');
    for (var i = 0; i < values.length; i++) {
        if (values[i].trim() !== '') {
            var color = values[i].substring(values[i].lastIndexOf("[") + 1, values[i].lastIndexOf("]")).trim();
            var text = values[i].replace('[' + color + ']', '').trim();
            if (type == 'L')
                this.addLabelTag(text);
            else if (this.type == 'C') {
                if (color == '' || color == null || typeof color === 'undefined') {
                    color = this.getColorFromText(text);
                }

                this.addColorTag(text, color);
            }
        }
    }
}

TagInput.prototype.addTag = function () {
    var edit = $('#' + this.id).parent().find('.edit');
    var text = edit.html().trim();
    if (text !== '') {
        if (this.type == 'L')
            this.addLabelTag(text);
        else if (this.type == 'C') {
            var color = this.getColorFromText(text);
            this.addColorTag(text, color);
        }
    }
    edit.html('');
    this.updateInput();
    if (!(typeof this.callback === 'undefined' || this.callback == null))
        this.callback();

    event.preventDefault();
}

TagInput.prototype.getColorFromText = function (text) {
    var color = '#dddddd';

    if (text.toLowerCase() == 'negro')
        color = '#000000';
    else if (text.toLowerCase() == 'blanco')
        color = '#ffffff';
    else if (text.toLowerCase() == 'rojo')
        color = '#dd0000';
    else if (text.toLowerCase() == 'rosado')
        color = '#ffaaaa';
    else if (text.toLowerCase() == 'verde')
        color = '#00cc00';
    else if (text.toLowerCase() == 'azul')
        color = '#0000dd';
    else if (text.toLowerCase() == 'celeste')
        color = '#88eeff';
    else if (text.toLowerCase() == 'gris')
        color = '#aaaaaa';
    else if (text.toLowerCase() == 'naranja')
        color = '#ee9900';
    else if (text.toLowerCase() == 'morado')
        color = '#880088';
    else if (text.toLowerCase() == 'amarillo')
        color = '#ffee00';
    else if (text.toLowerCase() == 'fucsia')
        color = '#ff0099';

    return color;
}

TagInput.prototype.setType = function (type) {
    this.type = type;
}

TagInput.prototype.addLabelTag = function (text) {
    $('#' + this.id).parent().find('.tags').append('<span class="tag label-tag"><label>' + text + '</label><a class="delete"></a></span>');
    var tagInput = this;
    $('#' + this.id).parent().find('.tag:last-child a').click(function () {
        tagInput.removeTag($(this).parent());
    });
}

TagInput.prototype.addColorTag = function (text, color) {
    var tagInput = this;
    $('#' + this.id).parent().find('.tags').append('<span class="tag color-tag"><span id="colorTag-' + colorTagIndex + '" class="color" title="' + color + '" style="background-color: ' + color + '"></span><label>' + text + '</label><a class="delete"></a></span>');
    var id = 'colorTag-' + colorTagIndex;
    var colorPicker = new ColorPicker(id, function (color) {
        $('#' + id).attr('title', color);
        $('#' + id).css('background-color', color);
        tagInput.updateInput();
    });

    $('#' + this.id).parent().find('.tag:last-child a').click(function () {
        tagInput.removeTag($(this).parent());
    });
    colorTagIndex++;
}

TagInput.prototype.removeTag = function (item) {
    item.remove();
    this.updateInput();
    if (!(typeof this.callback === 'undefined' || this.callback == null))
        this.callback();
}

TagInput.prototype.updateInput = function () {
    var tags = '';
    $('#' + this.id).parent().find('.tag').each(function (i, item) {
        if (i > 0)
            tags += ',';
        if ($(item).is('.color-tag'))
            tags += '[' + $(item).children('.color').attr('title') + ']';
        tags += $(item).children('label').html();
    });
    $('#' + this.id).val(tags);
}

TagInput.prototype.destroy = function () {
    $('#' + this.id).parent().children('.content').remove();
    $('#' + this.id).parent().children('input.hidden').remove();
    $('#' + this.id).unwrap();
}