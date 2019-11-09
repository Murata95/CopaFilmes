viewModel = function () {
    var that = this;
    this.Filmes = ko.observableArray(new Array());
    this.Count = ko.observable(0);
    this.selectedMovies = ko.observableArray(new Array());

    this.load = function () {
        $.get("/Filme/GetFilmes", function (data) {
            if (data.error == null) {
                that.Filmes(data.data);
            }
            else {
                showDialogOK(data.error.message);
            }
        });
    };

    this.checkMovie = function (e) {
        if (!that.canCheck() && !e.isChecked) {
            showDialogOK("Número máximo de filmes selecionado.");
            if (that.selectedMovies().length > 8)
                that.selectedMovies.remove(e.id);
            return false;
        } else {

            e.isChecked = !e.isChecked;

            that.Count(that.selectedMovies().length);

            return true;
        }
    };

    this.canCheck = function () {
        if (that.selectedMovies().length > 8)
            return false;

        return true;
    };

    this.GoToTournamentCommand = function () {
        console.log(that.selectedMovies().length);
        if (that.selectedMovies().length < 8) {
            showDialogOK("Por favor, selecione 8 filmes.");
            return false;
        }
        $.post("/Filme/copa", { ids: that.selectedMovies() }, function (data) {
            if (data.error == null && data.data.length == 2) {
                window.location.href = "Filme/Vencedor?first=" + data.data[0].titulo + "&second=" + data.data[1].titulo;
            }
            else {
                showDialogOK(data.error.message);
            }
        });
    };
};

ko.options.deferUpdates = false;
var vm = new viewModel();

$(document).ready(function () {
    ko.applyBindings(vm);
    vm.load();
});