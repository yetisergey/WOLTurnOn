var ViewModel = {
    List: ko.observableArray(),
    Id: ko.observable(),
    IpAddress: ko.observable(),
    MacAddress: ko.observable(),
    Status:ko.observable(),
    Name:ko.observable()
}
$(function () {

    ko.applyBindings(ViewModel);

    var hub = $.connection.myHub;

    hub.client.sendList = function (list) {
        ViewModel.List(list);
    };

    hub.client.onConnected = function (id, userName, allUsers) {
        console.log(id)
        hub.server.getList(id);
    }

    $.connection.hub.start().done(function () {
        hub.server.connect();
    });
});