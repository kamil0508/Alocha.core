$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#dataTables tfoot th').each(function () {
        var title = $(this).text();
        if (title.trim() === 'Opcje')
            $(this).html('');
        else
            $(this).html('<input type="text" style="width: 100%;" class = "form-control" placeholder=' + title + ' />');
    });

    // DataTable
    var table = $('#dataTables').DataTable();

    // Restore state
    var state = table.state.loaded();
    if (state) {
        table.columns().eq(0).each(function (colIdx) {
            var colSearch = state.columns[colIdx].search;

            if (colSearch.search) {
                $('input', table.column(colIdx).footer()).val(colSearch.search);
            }
        });
    }

    // Apply the search
    table.columns().eq(0).each(function (colIdx) {
        $('input', table.column(colIdx).footer()).on('keyup change', function () {
            table
                .column(colIdx)
                .search(this.value)
                .draw();
        });
    });
});