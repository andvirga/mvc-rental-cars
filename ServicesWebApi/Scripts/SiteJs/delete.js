$(function () {

    $(document).on("click", "a.delete", function () {
     
        var id = $(this).data('client-id');

        $.ajax({
            url: "/api/clients/" + id,
            type: 'DELETE',
            cache: false,
            statusCode: {
                200: function (data) {
                    viewModel.clients.remove(

                        function (client) {
                            return client.ClientID == data.ClientID;
                        }
                    );
                }
            }
        });

        return true;
    });
});
