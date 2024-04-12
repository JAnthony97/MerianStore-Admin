function DatePicker(id, minDay, minMonth, minYear, maxDay, maxMonth, maxYear) {

    this.inputId = id;
    this.id = 'datepicker-' + id;

    this.minDay = minDay;
    this.minMonth = minMonth;
    this.minYear = minYear;

    this.maxDay = maxDay;
    this.maxMonth = maxMonth;
    this.maxYear = maxYear;

    this.loadContent();
}

DatePicker.prototype.loadContent = function () {

    var html = '<div id="' + this.id + '" class="datepicker"><div class="datepicker-box"><select class="year">';

    if (typeof this.minYear === 'undefined' || this.minYear == null)
        this.minYear = 1900;
    if (typeof this.maxYear === 'undefined' || this.maxYear == null)
        this.maxYear = parseInt((new Date().getFullYear() + 150) / 100) * 100;

    for (var i = this.minYear; i <= this.maxYear; i++) {
        html += '<option value="' + i + '"';
        
        if (i == new Date().getFullYear())
            html += 'selected';
        
        html += '>' + i + '</option>';
    }

    html += '</select><table><thead><tr><th colspan="7" class="month"><a class="prev"></a><h5>Setiembre 2019</h5><a class="next"></a></th></tr><tr><th>D</th><th>L</th><th>M</th><th>M</th><th>J</th><th>V</th><th>S</th></tr></thead><tbody>';

    var count = 0;
    for (var i = 0; i < 6; i++) {
        html += '<tr>';
        for (var j = 0; j < 7; j++) {
            html += '<td class="pos-' + count + '"></td>';
            count++;
        }
        html += '</tr>';
    }
    html += '</tbody></table><div class="form-actions align-center"><a class="button btn-small btn-icon text-color-white background-color01 ic-cancel-white cancel">Cancelar</a><a class="button btn-small btn-icon text-color-white background-color02 ic-ok-white ok">Aceptar</a></div></div></div>';

    $('body').append(html);

    var datepicker = this;
    $('#' + this.inputId).focusin(function () {
        datepicker.show();
        this.blur();
    });
    $('#' + this.id + ' .cancel').click(function () {
        datepicker.hide();
    });
    $('#' + this.id + ' .ok').click(function () {
        datepicker.selectDate();
        datepicker.hide();
    });
    $('#' + this.id).click(function () {
        if (!$(event.target).is('.datepicker-box, .datepicker-box *'))
            datepicker.hide();
    });

    $(document).keyup(function (e) {
        if (e.which == 27) {
            datepicker.hide();
        }
    });

    $('#' + this.id + ' .prev').click(function () { datepicker.prevMonth() });
    $('#' + this.id + ' .next').click(function () { datepicker.nextMonth() });

    $('#' + this.id + ' .year').change(function () {
        datepicker.year = $(this).val();
        datepicker.loadMonth();
    });
}

DatePicker.prototype.loadMonth = function () {

    var mes;
    var dias = new Date(this.year, this.month + 1, 0).getDate();
    var diaSemana = new Date(this.year, this.month, 1).getDay();

    $('#' + this.id + ' .year').val(this.year);

    switch (this.month) {
        case 0:
            mes = 'ENERO';
            break;
        case 1:
            mes = 'FEBRERO';
            break;
        case 2:
            mes = 'MARZO';
            break;
        case 3:
            mes = 'ABRIL';
            break;
        case 4:
            mes = 'MAYO';
            break;
        case 5:
            mes = 'JUNIO';
            break;
        case 6:
            mes = 'JULIO';
            break;
        case 7:
            mes = 'AGOSTO';
            break;
        case 8:
            mes = 'SETIEMBRE';
            break;
        case 9:
            mes = 'OCTUBRE';
            break;
        case 10:
            mes = 'NOVIEMBRE';
            break;
        case 11:
            mes = 'DICIEMBRE';
            break;
    }
    mes += ' ' + this.year;

    $('#' + this.id + ' .month h5').html(mes);
    $('#' + this.id + ' td').html('');
    $('#' + this.id + ' td').removeClass('today');
    $('#' + this.id + ' td').removeClass('active');

    var calendar = this;

    for (var i = 1; i <= dias; i++) {
        var fecha = this.formatDate(this.year, this.month, i);
        $('#' + this.id + ' .pos-' + diaSemana).click({ diaSemana: diaSemana, day: i }, function (event) {
            calendar.selectedDate = calendar.formatDate(calendar.year, calendar.month, event.data.day);
            $('#' + calendar.id + ' td').removeClass('active');
            $('#' + calendar.id + ' .pos-' + event.data.diaSemana).addClass('active');
        });
        $('#' + this.id + ' .pos-' + diaSemana).html(i.toString());
        if (i == new Date().getDate() && this.month == new Date().getMonth() && this.year == new Date().getFullYear())
            $('#' + this.id + ' .pos-' + diaSemana).addClass('today');
        if (fecha == this.selectedDate)
            $('#' + this.id + ' .pos-' + diaSemana).addClass('active');
        diaSemana++;
    }
}

DatePicker.prototype.prevMonth = function () {

    this.month--;
    if (this.month < 0) {
        this.month = 11;
        this.year--;
    }
    this.loadMonth();
}

DatePicker.prototype.nextMonth = function () {

    this.month++;
    if (this.month > 11) {
        this.month = 0;
        this.year++;
    }
    this.loadMonth();
}

DatePicker.prototype.formatDate = function (year, month, day) {
    return ('0' + day).slice(-2) + '/' + ('0' + (month + 1)).slice(-2) + '/' + year;
}

DatePicker.prototype.selectDate = function () {
    $('#' + this.inputId).val(this.selectedDate);
    $('#' + this.inputId).change();
}

DatePicker.prototype.show = function () {

    this.selectedDate = $('#' + this.inputId).val();

    var date = null;

    if (this.selectedDate !== '' || !this.selectedDate == null)
        date = this.selectedDate.split('/');

    if (date == null) {
        this.month = new Date().getMonth();
        this.year = new Date().getFullYear();
    }
    else {
        this.month = parseInt(date[1]) - 1;
        this.year = parseInt(date[2]);
    }

    this.loadMonth();

    $('#' + this.id).addClass('active');
}

DatePicker.prototype.hide = function () {
    $('#' + this.id).removeClass('active');
}