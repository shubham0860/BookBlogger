function setPasswordEditor(container, options) {
    $('<input type="password" id="Password" name="' + options.field + '" title="Password" required="required" autocomplete="off" aria-labelledby="Password-form-label" data-bind="value: Password" aria-describedby="Password-form-hint"/>')
        .appendTo(container)
        .kendoTextBox();
}
function setNameEditor(container, options) {
    $('<input type="text" id="txtUsername" name="' + options.field + '" title="Name" required="required" autocomplete="off" aria-labelledby="Password-form-label" data-bind="value: Username" aria-describedby=" "/>')
        .appendTo(container)
        .kendoTextBox();
}

function onFormValidateField(e) {
    $("#validation-success").html("");
}

function onFormSubmit(e) {
    e.preventDefault();
    $("#validation-success").html("<div class='k-messagebox k-messagebox-success'>Login Details are Provided</div>");
    $.ajax({
        url: 'https://localhost:44367/api/account/login',
        method: 'POST',
        data: {
            username: $('#txtUsername').val(),
            password: $('#Password').val(),
        },
        success: function (res) {
            
            $("#validation-success").html("<div class='k-messagebox k-messagebox-success'>Login Details are Validated</div>");
            sessionStorage.setItem("User", res.user);
            sessionStorage.setItem("UserName", res.userName);
            sessionStorage.setItem("IsAdmin", res.isAdmin);

            window.location.href = res.RedirectUrl;
        },
        error: function () {
        },


        error: function (jqXHR) {
            //$('#divErrorText').text(jqXHR.responseText);
            //$('#divError').show('fade');
            $("#validation-success").html("<div class='k-messagebox k-messagebox-error'>Wrong Password or Username</div>");
        }
    });
}

function onFormClear(e) {
    $("#validation-success").html("");
}