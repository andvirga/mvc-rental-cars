$(function () {
    var form = $('#newClientForm');
    form.submit(function () {
        var form = $(this);
        var client = { FirstName: $('#FirstName').val(), LastName: $('#LastName').val(), Email: $('#email').val() };
        var json = JSON.stringify(client);

        $.ajax({
            url: '/api/clients',
            cache: false,
            type: 'POST',
            data: json,
            contentType: 'application/json; charset=utf-8',
            statusCode: {
                201 /*Created*/: function (data) {
                    viewModel.clients.push(data);
                }
            }
        });

        return false;
    });
});