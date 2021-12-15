function GroupViewModel() {
    let self = this;

    self.selectedId = ko.observable()

    self.profiles = ko.observableArray();
    self.groups = ko.observableArray();
    

    self.getProfiles = function () {
        $.ajax({
            url: "../api/profiles/",
            type: 'GET',
            success: function (data) {
                self.profiles(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(jqXHR.responseText);
            },
            complete: function () {
            }
        });
    }

    self.getGroups = function (obj) {
        $.ajax({
            url: "../api/profiles/" + obj.id,
            type: 'GET',
            success: function (data) {
                self.groups(data);
                //window.location.href = "/home/projects";
                console.log(self.groups(data));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(jqXHR.responseText);
            },
            complete: function () {
            }
        });
    }

}