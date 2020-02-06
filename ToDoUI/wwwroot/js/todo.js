var Todo = function(name, isComplete, callback) {
    var self = this;
    self.Name = ko.observable(name);
    self.IsComplete = ko.observable(isComplete);
    self.updateCallback = ko.computed(function() {
        callback(self);
        return true;
    });
}

var viewModel = function() {
    var self = this;
    self.todos = ko.observableArray([]);
    self.inputName = ko.observable("");
    self.completeTodos = ko.observable(0);
    self.markAll = ko.observable(false);

    self.addOne = function() {
    var order = self.todos().length;
    var t = new Todo(self.inputName(), false, self.countUpdate);
        self.todos.push(t);
    };

    self.createOnEnter = function(item, event) {
        if (event.keyCode == 13 && self.inputTitle()) {
            self.addOne();
            self.inputTitle("");
        } else {
            return true;
        };
    }

    self.toggleEditMode = function(item, event) {
        $(event.target).closest('li').toggleClass('editing');
    }

    self.editOnEnter = function(item, event) {
        if (event.keyCode == 13 && item.title) {
            item.updateCallback();
            self.toggleEditMode(item, event);
        } else {
            return true;
        };
    }

    self.markAll.subscribe(function(newValue) {
        ko.utils.arrayForEach(self.todos(), function(item) {
            return item.IsComplete(newValue);
        });
    });

    self.countUpdate = function(item) {
        var completeArray = ko.utils.arrayFilter(self.todos(), function(it) {
            return it.IsComplete();
        });
        self.completeTodos(completeArray.length);
        return true;
    };

    self.countCompleteText = function(bool) {
        var cntAll = self.todos().length;
        var cnt = (bool ? self.completeTodos() : cntAll - self.completeTodos());
        var text = "<span class='count'>" + cnt.toString() + "</span>";
        text += (bool ? " completed" : " remaining");
        text += (self.completeTodos() > 1 ? " items" : " item");
        return text;
    }

    self.clear = function() {
        self.todos.remove(function(item) {
            return item.IsComplete();
        });
    }

    $.getJSON("localhost:5000/api/todo", function(allData) {
        var mappedTasks = $.map(allData, function(item) { return ko.mapping.fromJS(item); });
        self.todos(mappedTasks);
    });  
};

ko.applyBindings(new viewModel());
  