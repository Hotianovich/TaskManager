/*=================Modal Window Login==================================================*/
$(function () {
    $.ajaxSetup({ cache: false });
    $(".LoginClick").click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});
/*=========================================================================================*/
/*Menu-toggle*/
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
});

/*Scroll Spy*/
$('body').scrollspy({ target: '#spy', offset: 80 });

/*Smooth link animation*/
$('a[href*=#]:not([href=#])').click(function () {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') || location.hostname == this.hostname) {

        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
        if (target.length) {
            $('html,body').animate({
                scrollTop: target.offset().top
            }, 1000);
            return false;
        }
    }
});

/*=================Modal Window AddEmployee==================================================*/
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

/*=================Modal Window AddEmployee Insert ==================================================*/
function check() {
    var inp1 = $('#input1').val()
    var inp2 = $('#input2')

    if (inp1.length != 0 && inp2.val().length != 0) {
        var pattern = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.([a-z]{1,6}\.)?[a-z]{2,6}$/i;
        if (inp2.val().search(pattern) == 0) {
            $('#send').removeAttr('disabled');
            $('#valid').text('');
        } else {
            $('#valid').css('color', 'red');
            $('#valid').text('Введите email');
            $('#send').attr('disabled', 'disabled');
        }
       
    } else {
        $('#send').attr('disabled', 'disabled');
    }
}

/*=======================================Add Employee========================================*/
function clearTextarea() {
    $('#modDialog').modal('hide');
}

/*========================================Del Imployee========================================*/
//$(document).ready(function () {
//    $('.del-empl').click(function () {
//        var eId = $('#BookId').val();
//        $.ajax({
//            type: "post",
//            url: "/Employee/Delete",
//            data: { Employeeid: eId },
//            success: function (data) {
                
               
//            },
//            error: function () {
//                alert('error')
//            }
//        });
//    });
//});

/*============================================== Add Project ===================================================================*/
function projectname() {
    var inp1 = $('#project-name').val()

    if (inp1.length != 0) {
        $('#send-project').removeAttr('disabled');
    } else {
        $('#send-project').attr('disabled', 'disabled');
    }
}

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
    $('#grey-circle').mouseover(function () {
        $(this).css('background-image', 'url(/Content/Images/dwcheckyes.svg)');
    }).mouseout(function () {
        $(this).css('background-image', 'url(/Content/Images/grey.svg)');
    });
});

