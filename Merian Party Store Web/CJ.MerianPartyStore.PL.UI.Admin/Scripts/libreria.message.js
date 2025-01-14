﻿$(document).ready(function () {
    var htmlMessage = '<div class="popup" id="popupMessage"><div class="popup-box"><label class="text"></label><div class="form-actions"><a class="button btn-small background-color02 text-color-white btn-icon ic-ok-white">Aceptar</a></div></div></div>';
    var htmlConfirmation = '<div class="popup" id="popupConfirmation"><div class="popup-box"><label class="text"></label><div class="form-actions"><a class="button btn-small background-color01 text-color-white ic-cancel-white btn-icon">Cancelar</a><a class="button btn-small background-color02 btn-icon ic-ok-white text-color-white">Aceptar</a></div></div></div>';
    var htmlMultipleConfirmation = '<div class="popup" id="popupMultipleConfirmation"><div class="popup-box"><label class="text"></label><div class="form-actions"></div></div></div>';
    var htmlAlert = '<div class="alerts" id="alerts"></div>';
    $('body').append(htmlMessage);
    $('body').append(htmlConfirmation);
    $('body').append(htmlMultipleConfirmation);
    $('body').append(htmlAlert);
});

function showMessage(message, callback) {

    var popupMessage = new Popup('popupMessage');
    $('#popupMessage .text').html(message);
    $('#popupMessage a').click(function () {
        if (callback !== null)
            callback();

        popupMessage.hide();
    });
    popupMessage.show();
}

function showConfirmation(message, okText, okColorClass, okIconClass, callback) {

    var popupMessage = new Popup('popupConfirmation');
    $('#popupConfirmation .text').html(message);
    $('#popupConfirmation a:last-child').attr('class', 'button btn-small ' + okColorClass + ' btn-icon ' + okIconClass +' text-color-white');
    $('#popupConfirmation a:last-child').html(okText);

    $('#popupConfirmation a:first-child').unbind();
    $('#popupConfirmation a:first-child').click(function () {
        popupMessage.hide();
        $('#popupConfirmation a').off('click');
    });

    $('#popupConfirmation a:last-child').unbind();
    $('#popupConfirmation a:last-child').click(function () {
        if (callback != null)
            callback();
        $('#popupConfirmation a').off('click');
        popupMessage.hide();
    });
    popupMessage.show();
}

function showMultipleConfirmation(message, okTexts, okColorClasses, okIconClasses, callbacks) {

    var popupMessage = new Popup('popupMultipleConfirmation');
    $('#popupMultipleConfirmation .text').html(message);
    $('#popupMultipleConfirmation .form-actions').html('<a class="button btn-small">CANCELAR</a>');

    $('#popupMultipleConfirmation a:first-child').unbind();
    $('#popupMultipleConfirmation a:first-child').click(function () {
        popupMessage.hide();
        $('#popupMultipleConfirmation a').off('click');
    });

    for (var i = 0; i < okTexts.length; i++) {
        var okText = okTexts[i];
        var okColorClass = okColorClasses[i];
        var okIconClass = okIconClasses[i];
        var callback = callbacks[i];

        var html = '<a class="button btn-small ' + okColorClass + ' btn-icon ' + okIconClass + '">' + okText.toUpperCase() + '</a>'
        $('#popupMultipleConfirmation .form-actions').append(html);

        $('#popupMultipleConfirmation a:last-child').unbind();
        $('#popupMultipleConfirmation a:last-child').click({ callback: callback }, function (event) {
            if (event.data.callback != null)
                event.data.callback();
            $('#popupMultipleConfirmation a').off('click');
            popupMessage.hide();
        });
    }
    popupMessage.show();
}

function showAlert(iconClass, text, miliseconds) {
    if (typeof miliseconds == 'undefined' || miliseconds == null)
        miliseconds = 3000;

    var html = '<div class="alert"><span class="' + iconClass + '"></span><label>' + text + '</label></div>';
    $('#alerts').append(html);

    var alertBox = $('#alerts .alert:last-child');

    setTimeout(function () {
        alertBox.addClass('active');
    }, 30)

    setTimeout(function () {
        alertBox.removeClass('active');

        setTimeout(function () {
            alertBox.remove();
        }, 300)
    }, miliseconds);
}