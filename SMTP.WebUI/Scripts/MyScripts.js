$(function () {
    $.ajaxSetup({ cache: false });
    $(".EmployeeAdd").click(function (e) {

        $('#modDialog').modal({
            //установим модальному окну следующие параметры:
            backdrop: 'static'
        });

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});
/*================================================ Modal Project window  =========================================================*/
$(function () {
    $.ajaxSetup({ cache: false });
    $(".ProjectAdd").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});

/*================================================Saccess=============================*/
$(function () {
    $('.grey-circle').mouseover(function () {
        $(this).css('background-image', 'url(/Content/Images/dwcheckyes.svg)');
    }).mouseout(function () {
        $(this).css('background-image', 'url(/Content/Images/grey.svg)');
    });
});

/*======================================================DatePicer======================*/

$(function () {

    $.datepicker.setDefaults($.datepicker.regional['ru']);
    
    $('.text-editor-calendar').datepicker({
        dateFormat: "yy/mm/dd",
        showOn: "both",
        buttonImage: "/Content/Images/calendrier.svg",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true
    });

    $.datepicker.regional['ru'] = {
        closeText: 'Закрыть',
        prevText: 'Пред',
        nextText: 'След',
        currentText: 'Сегодня',
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
        'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
        'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
        dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
        dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
        dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        weekHeader: 'Не',
        dateFormat: "yy/mm/dd",
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['ru']);

});

/*============================================validation form=====================================*/

//$('#myForm').validate({
//    rules: {
//        taskName: {
//            required: true
//        }
//    }
//});

//$('#myeditForm').validate({
//    rules: {
//        taskName: {
//            required: true
//        }
//    }
//});
/*============================================datapicer for money=====================================*/
