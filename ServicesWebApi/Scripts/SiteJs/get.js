$(function () {
        // We're using a Knockout model. This clears out the existing clients.
        viewModel.clients([]);

        $.get('/api/clients', function (data) {
            // Update the Knockout model (and thus the UI) with the clients received back 
            // from the Web API call.
            viewModel.clients(data);
        });

   
   
});