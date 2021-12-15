var GroupsViewModel = function (groupsString) {
    var self = this;

    self.groups = ko.observable(groupsString.Split("|"));
}