$(function () {
    //---------------------------------------------------------
    // Using Queryable to page
    //---------------------------------------------------------
    $("#getClientsQueryable").click(function () {
        viewModel.clients([]);

        var pageSize = $('#pageSize').val();
        var pageIndex = $('#pageIndex').val();

        var url = "/api/clients?$top=" + pageSize + '&$skip=' + (pageIndex * pageSize);

        $.getJSON(url, function (data) {
            // Update the Knockout model (and thus the UI) with the comments received back 
            // from the Web API call.
            viewModel.clients(data);
        });

        return false;
    });
});