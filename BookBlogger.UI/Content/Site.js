$(document).ready(function () {

    var isAdmin = sessionStorage.getItem("IsAdmin");
    var ISAdmin = false;
    if (isAdmin == "true") {
        ISAdmin = true;
    }
    $.ajax({
        url: 'https://localhost:44367/api/books/GetUsername',
        method: 'GET',
        success: function (res) {
            //return res;
            $("#getUser").html(res);
        },
        error: function () {
        },
    });

    $("#btnLogout").click(function (){
        $.ajax({
            url: 'https://localhost:44367/api/account/Logout',
            method: 'GET',

            success: function (res) {
                //return res;
                window.location.href = res.RedirectUrl;
            },
            error: function () {
            },
        });
    });

    dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            idField: "ID",
            read: {
                url: "https://localhost:44367/api/Books",
                type: "GET"

            },
            create: {
                url: "https://localhost:44367/api/Books",
                type: "POST"
            },
            update: {
                url: "https://localhost:44367/api/Books",
                type: "PUT"
            },
            destroy: {

                url: "https://localhost:44367/api/Books",
                type: "delete"
            }

        },
        schema: {
            data: "Data",
            model: {
                id: "ID",
                fields: {
                    ID: { type: "number" },
                    ISBN: { type: "string", validation: { required: true } },
                    BookName: { type: "string", validation: { required: true } },
                    Price: { type: "number", validation: { required: true } },
                    Details: { type: "string", validation: { required: true } },
                    ImageUrl: { type: "string" },
                    DownloadUrl: { type: "string" },
                    AuthorName: { type: "string", validation: { required: true } },
                    Surname: { type: "string", validation: { required: true } }
                }
            }
        },
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        serverGrouping: true,
        serverAggregates: true,
    });

    $("#btnGrid").click(function () {
        $("#Add").hide();
        $('#audit').hide();
        if (ISAdmin) {
            $('#grid').hide();
            $("#Admin").show();
        }
        else {
            $('#Admin').hide();
            $("#grid").show();
        }
    });
    var column = [
        { field: "ISBN" },
        { field: "BookName" },
        { field: "Price" },
        { field: "Details" },
        { field: "ImageUrl" },
        { field: "DownloadUrl" },
        { field: "AuthorName" },
        { field: "Surname" },
        {
            command: ["edit"], width: 90
        }
    ];

    if (ISAdmin) {
        $("#Admin").kendoGrid({
            height: 530,
            toolbar: ["create", "search"],
            columns: [
                { field: "ISBN" },
                { field: "BookName" },
                { field: "Price" },
                { field: "Details" },
                { field: "ImageUrl" },
                { field: "DownloadUrl" },
                { field: "AuthorName" },
                { field: "Surname" },
                {
                    command: ["edit", "destroy"], width: 180
                }
            ],
            dataSource: dataSource,
            editable: "popup",
            selectable: "single row",
            resizable: true,
            destroy: true,
            scrollable: true
        }).data("kendoGrid");
    }
    else {
        $("#grid").kendoGrid({
            height: 530,
            toolbar: ["create"],
            columns: column,
            dataSource: dataSource,
            editable: "popup",
            selectable: "single row",
            resizable: true,
            destroy: true,
            scrollable: true
        }).data("kendoGrid");
    }




$("#btnAdd").click(function () {
    $("#grid").hide();
    $('#Admin').hide();
    $("#audit").hide();
    $("#Add").toggle();

$("#Add").kendoGrid({
    height: 530,
    columns: [
        { field: "ISBN" },
        { field: "BookName" },
        { field: "Price" },
        { field: "Details" },
        { field: "ImageUrl" },
        { field: "DownloadUrl" },
        { field: "AuthorName" },
        { field: "Surname" }
    ],

    dataSource: {
        type: "aspnetmvc-ajax",
        transport: {
            idField: "ID",
            
            read: {
               
                url: "https://localhost:44367/api/Books/UserBooks",
                type:"GET"
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "ID",
                fields: {
                    ID: { type: "number" },
                    ISBN: { type: "string" },
                    BookName: { type: "string" },
                    Price: { type: "number" },
                    Details: { type: "string" },
                    ImageUrl: { type: "string" },
                    DownloadUrl: { type: "string" },
                    AuthorName: { type: "string" },
                    Surname: { type: "string" }
                }
            }
        },
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        serverGrouping: true,
        serverAggregates: true,
    },
    scrollable: true
})
});
$("#btnAudit").click(function () {
    $("#grid").hide();
    $('#Admin').hide();
    $("#Add").hide();
    $("#audit").toggle();

$("#audit").kendoGrid({
    height: 530,
    toolbar: ["search"],
    columns: [
        { field: "Username" },
        { field: "BookName" },
        { field: "Operation" },
        { field: "NewValue" },
        { field: "OldValue" },
        { field: "CreatedDateTime" }
    ],
    dataSource: {
        type: "aspnetmvc-ajax",
        transport: {
            idField: "ID",
            read: {
                url: "https://localhost:44367/api/Books/GetAudit",
                type: "GET"
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "ID",
                fields: {
                    ID: { type: "number" },
                    Username: { type: "string" },
                    BookName: { type: "string" },
                    Operation: { type: "string" },
                    NewValue: { type: "string" },
                    OldValue: { type: "string" },
                    CreatedDateTime: { type: "date" }
                }
            }
        },
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        serverGrouping: true,
        serverAggregates: true,
    },
    scrollable: true
})
});
});
