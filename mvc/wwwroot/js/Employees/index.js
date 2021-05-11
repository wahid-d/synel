jQuery(function() {

    $('.edit-datetime').datetimepicker({
        format: "L"
    });
    
    var table = $('#my-table').DataTable({
        "scrollX": true,
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "bAutoWidth": false,
        "oLanguage": {
            "sSearch": "Filter the table:"
        }
    });
    
    $('#my-table tbody').on('click', 'tr', function () {
        var data = table.row( this ).data();

        $('#edit-forename').val(data[0].split(' ')[0]);
        $('#edit-surname').val(data[0].split(' ')[1]);
        $('#edit-birthdate').val(data[2]);
        
        $('#edit-phone').val(data[3]);
        $('#edit-mobile').val(data[4]);
        $('#edit-email').val(data[7]);

        $('#edit-address').val($('#employee-address').html().trim());
        $('#edit-address2').val($('#employee-address2').html().trim());
        $('#edit-postal').val(data[6]);

        $('#edit-startdate').val(data[8]);
        $('#edit-payroll').val(data[1]);

        console.log(data[5])

        $('#edit-employee-modal').modal('show');
    } );


});