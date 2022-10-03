
$(document).ready(function () {

$("#btnGrid").click(function () {
    $("#Add").hide();
    $('#audit').hide();
    $("#grid").toggle();


$("#grid").kendoGrid({
    height: 500,
    toolbar: ["create"],

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
            field: "Image",
            width: 150,
            template: '<img src="' + "https://dictionary.cambridge.org/images/thumb/book_noun_001_01679.jpg?version=5.0.252" + '">'
        },
        { command: ["edit", "destroy"], width: 180 }

    ],

    dataSource: {
        type: "aspnetmvc-ajax",
        transport: {
            idField: "ID",
            read: {
                url: "/api/Books",
                type: "GET"
                
            },
            create: {
                url: "api/Books",
                type:"POST"
            },
            update: {
                url: "api/Books",
                type:"PUT"
            },
            destroy: {
                
                url: "api/Books",
                type: "delete"
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
    editable: "inline",
    destroy: true,
    pageable: true,
    scrollable: true
})
});

$("#btnAdd").click(function () {
    $("#grid").hide();
    $("#audit").hide();
    $("#Add").toggle();

$("#Add").kendoGrid({
    height: 500,
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
        autoSync: true,
        transport: {
            idField: "ID",
            
            read: {
               
                url: "/api/Books/UserBooks",
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
    editable: "inline",
    destroy: true,
    pageable: true,
    scrollable: true
})
});
$("#btnAudit").click(function () {
    $("#grid").hide();
    $("#Add").hide();
    $("#audit").toggle();

$("#audit").kendoGrid({
    height: 400,
    columns: [
        { field: "UserID" },
        { field: "BookID" },
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
                url: "/api/Books/GetAudit",
                type: "GET"
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "ID",
                fields: {
                    ID: { type: "number" },
                    UserID: { type: "number" },
                    BookID: { type: "number" },
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
    pageable: true,
    navigatable: true
})
});
});
