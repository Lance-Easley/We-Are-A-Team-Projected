function ProjectViewModel(id) {
    let self = this;
    

    self.selectedId = ko.observable();
    self.profile = ko.observable();
    self.projectName = ko.observable();
    self.permissionGroups = ko.observableArray();


    self.getGroups = function () {

        $.ajax({
            url: "../api/profiles/" + id,
            type: 'GET',
            success: function (data) {
                self.profile(data);
                console.log(self.profile());
                self.projectName(self.profile().projectName);
                self.permissionGroups(self.profile().permissionGroups);
                console.log(self.permissionGroups());

            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(jqXHR.responseText);
            },
            complete: function () {
            }
        });
    }


}