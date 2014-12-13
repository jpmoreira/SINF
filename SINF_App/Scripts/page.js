$(document).ready(setUp);

function setUp() {
    $("#login_submit").click(savelocalsession);
}

function savelocalsession(event) {
    localStorage.Username = $("#lblUser").val();
    console.log(localStorage.Username);
    localStorage.Password = $("#lblPassword").val();
    console.log(localStorage.Password);
    localStorage.Company = $("#lblCompany").val();
    console.log(localStorage.Company);
}


// Detail Order - Spinner ======================================
/*
$('.btn-number').click(function (e) {
    e.preventDefault();

    fieldName = $(this).attr('data-field');
    type = $(this).attr('data-type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                input.val(currentVal - 1).change();
            }
            if (parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }

        } else if (type == 'plus') {

            if (currentVal < input.attr('max')) {
                input.val(currentVal + 1).change();
            }
            if (parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }
});

$('.input-number').focusin(function () {
    $(this).data('oldValue', $(this).val());
});

$('.input-number').change(function () {

    minValue = parseInt($(this).attr('min'));
    maxValue = parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());

    name = $(this).attr('name');
    if (valueCurrent >= minValue) {
        $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
    } else {
        alert('Sorry, the minimum value was reached');
        $(this).val($(this).data('oldValue'));
    }
    if (valueCurrent <= maxValue) {
        $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
    } else {
        alert('Sorry, the maximum value was reached');
        $(this).val($(this).data('oldValue'));
    }


});

$(".input-number").keydown(function (e) {
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});
*/

$(".btn-number").click(actualizaQtd);
$(".input-number").change(changeQtd);

function changeQtd(event)
{
    minValue = parseInt($(this).attr('min'));
    maxValue = parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());

    name = $(this).attr('name');
    if (valueCurrent >= minValue)
    {
        $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
    }
    else
    {
        alert('O valor minimo foi atingido');
        $(this).val(minValue);
        $(this).attr("value", minValue);
        return;
    }

    if (valueCurrent <= maxValue)
    {
        $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
    }
    else
    {
        alert('O valor maximo foi atingido');
        $(this).val(maxValue);
        $(this).attr("value", maxValue);
        return;
    }

    $(this).attr("value", valueCurrent);

}

function actualizaQtd(event)
{
    event.preventDefault();

    fieldName = $(this).attr('data-field');
    type = $(this).attr('data-type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());

    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                
                input.val(currentVal - 1);
                input.attr("value", (currentVal - 1));

                $(".btn-number[data-type='plus'][data-field='" + fieldName + "']").removeAttr('disabled');
                
            }

            if (parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }

        } else if (type == 'plus') {

            if (currentVal < input.attr('max')) {
                
                input.val(currentVal + 1);
                input.attr("value", (currentVal + 1));

                $(".btn-number[data-type='minus'][data-field='" + fieldName + "']").removeAttr('disabled')
                
            }

            if (parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }
}

//======== Set Missing quantity =============
$(".btn-primary").click(setQtdEmFalta);

function setQtdEmFalta(event)
{
    var qtdEmFalta = parseInt($(event.target).closest(".row").find("#qtdEmFalta").html());
    $(event.target).closest(".row").find(".input-number").attr("value",qtdEmFalta);
}


