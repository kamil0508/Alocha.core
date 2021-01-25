//User Setting set TODO
'use strict';

var epochTime = new Date().valueOf();
localStorage.setItem('DataTables_myTables_/Sow/IndexServerSide', '{"time": ' + epochTime + ', "start": 0, "length": 50, "order": [[0, "asc"]], "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true }, "columns": \
                    [{"visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } },\
                    { "visible": true, "search": { "search": "", "smart": true, "regex": false, "caseInsensitive": true } }]}');

//jQuery DataTables initialization
var otable = $('#myTables').DataTable({
    "processing": true, // for show processing bar
    "serverSide": true, // for process on server side
    "stateSave": true,
    "lengthMenu": [25, 50, 100, 200],
    "scrollY": "410px",
    "scrollCollapse": true,
    "dom": 'lBfrtip',
    "buttons": ['colvis'],
    "ajax": {
        "url": "/Sow/LoadData",
        "type": "POST",
        "datatype": "json"
    },
    "columns": [{
        //index 0
        "data": "number",
        "name": "Number",
        "autoWidth": true
    }, {
        //index 1
        "data": "status",
        "name": "Status",
        "autoWidth": true
    }, {
        //index 2
        "data": "dateHappening",
        "name": "DateHappening",
        "autoWidth": true
    }, {
        //index 3
        "data": "dateInsimination",
        "name": "DateInsimination",
        "autoWidth": true
    }, {
        //index 4
        "data": "dateDetachment",
        "name": "DateDetachment",
        "autoWidth": true
    }, {
        //index 5
        "data": "dateBorn",
        "name": "DateBorn",
        "autoWidth": true
    }, {
        //index 6
        "data": "vaccineDate",
        "name": "VaccineDate",
        "autoWidth": true
    }, {
        "render": function render(data, type, row) {
            var char = "'";
            var dropOptionButton = '<div class="dropdown btn-block pull-right"><button class="btn btn-xs btn-primary btn-block dropdown-toggle" data-toggle="dropdown"> Opcje<span class="caret"></span></button>';
            var dropDownMenu = '<ul class="dropdown-menu bg-dark"><li><a class="dropdown-item text-info" href="/Sow/Details/' + row.sowId + '">Szczegóły</a></li> <li><li><a class="dropdown-item text-info" href="/Sow/Edit/' + row.sowId + '">Edytuj</a></li><li><li><a class="dropdown-item text-info" href="/Sow/Remove/' + row.sowId + '" onclick = "return confirm(' + char + ' Czy napewno chcesz usunąc tą loche? ' + char + ');">Usuń</a></li></ul></div>';
            return dropOptionButton + dropDownMenu;
        }
    }],
    rowId: function rowId(row) {
        return row.number;
    },
    "language": {
        "processing": "Przetwarzanie...",
        "search": "Szukaj:",
        "lengthMenu": "Pokaż _MENU_ pozycji",
        "info": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
        "infoEmpty": "Pozycji 0 z 0 dostępnych",
        "infoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
        "loadingRecords": "Wczytywanie...",
        "processing": "Loading. Please wait...",
        "zeroRecords": "Nie znaleziono pasujących pozycji",
        "paginate": {
            "first": "Pierwsza",
            "previous": "Poprzednia",
            "next": "Następna",
            "last": "Ostatnia"
        },
        "aria": {
            "sortAscending": ": aktywuj, by posortować kolumnę rosnąco",
            "sortDescending": ": aktywuj, by posortować kolumnę malejąco"
        },
        "autoFill": {
            "cancel": "Anuluj",
            "fill": "Wypełnij wszystkie komórki <i>%d<\/i>",
            "fillHorizontal": "Wypełnij komórki w poziomie",
            "fillVertical": "Wypełnij komórki w pionie"
        },
        "buttons": {
            "collection": "Zbiór <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
            "colvis": "Widoczność kolumny",
            "colvisRestore": "Przywróć widoczność",
            "copy": "Kopiuj",
            "copyKeys": "Naciśnij Ctrl lub u2318 + C, aby skopiować dane tabeli do schowka systemowego. <br \/> <br \/> Aby anulować, kliknij tę wiadomość lub naciśnij Esc.",
            "copySuccess": {
                "1": "Skopiowano 1 wiersz do schowka",
                "_": "Skopiowano %d wierszy do schowka"
            },
            "copyTitle": "Skopiuj do schowka",
            "csv": "CSV",
            "excel": "Excel",
            "pageLength": {
                "-1": "Pokaż wszystkie wiersze",
                "1": "Pokaż 1 wiersz",
                "_": "Pokaż %d wierszy"
            }
        },
        "emptyTable": "Brak danych w tabeli",
        "searchBuilder": {
            "add": "Dodaj warunek",
            "clearAll": "Wyczyść wszystko",
            "condition": "Warunek",
            "data": "Dane"
        }
    }
});

function SearchTable() {
    otable.columns(0).search($('#number-filter').val().trim());
    otable.draw();
};

$(document).on('click', '#search-button', function () {
    SearchTable();
});

$(document).on('keypress', "#number-filter", function (e) {
    if (e.keyCode == 13) {
        SearchTable();
    }
});

