$(() => {
    // Initialize SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalrServer")  // Ensure the URL is correct
        .build();

    // Function to start the connection and handle errors
    async function startConnection() {
        try {
            await connection.start();
            /*alert('Connected to the db');*/
            await InvokeProducts();
        } catch (err) {
            console.error("Connection error:", err.toString());
        }
    }

    // Function to invoke server method
    async function InvokeProducts() {
        try {
            
            await connection.invoke("SendProducts");
        } catch (error) {
            console.error("Invoke error:", error.toString());
        }
    }

    // Function to bind data to the table
    function BindData(products) {
        
        const rows = products.map(product => `
            <tr>
                <td>${product.productName}</td>
                <td>${product.category}</td>
                <td>${product.unitPrice}</td>
                <td>${product.stockQuantity}</td>
                <td>
                    <a href='../Products/Edit?ProductID=${product.productID}'>Edit</a>
                    <a href='../Products/Delete?ProductID=${product.productID}'>Delete</a>
                    <a href='../Products/Details?ProductID=${product.productID}'>Details</a>
                </td>
            </tr>`
        ).join('');
        $("#tableBody").html(rows);
    }

    // Set up SignalR event handlers
    connection.on("ReceivedProducts", products => {
        BindData(products);
    });

    // Start the connection
    startConnection();
});



//$(() => {
//    
//  /*  LoadProductData();*/
//    var connection = new signalR.HubConnectionBuilder().withUrl("signalrServer").build();
//    connection.start().then(function () {
//        alert('connected to the db');
//        InvokeProducts();
//    }).catch(function (err) {
//        return console.error(err.toString());
//    });
    
//    //connection.on("LoadProducts", function () {
//    //    LoadProductData();
//    //})
    
//    connection.on("ReceivedProducts", function (products) {
//        
//        BindData(products)
//    })
//    /*LoadProductData();*/
//    //function LoadProductData(){
//    //  /*  var tr = '';*/
//    //    $.ajax({
//    //        url: "/Products/GetProducts",
//    //        method: "GET",
//    //        success: (result) => {
//    //            //$.each(result, (k, v) => {
//    //            //    tr += `<tr>
//    //            //    <td>${v.ProductName}</td>
//    //            //    <td>${v.Category}</td>
//    //            //    <td>${v.UnitPrice}</td>
//    //            //    <td>${v.StockQuantity}</td>
//    //            //    <td>
//    //            //    <a href='../Products/Edit?ProductID=${v.ProductID}'>Edit</a>
//    //            //    <a href='../Products/Delete?ProductID=${v.ProductID}'>Delete</a>
//    //            //    <a href='../Products/Details?ProductID=${v.ProductID}'>Details</a>
//    //            //    </td>
//    //            //    </tr>`
//    //            //})
//    //            //$("#tableBody").html(tr);
//    //            BindData(result);
//    //        },
//    //        error: (error) => {
//    //            console.log(error);
//    //        }
//    //    });
//    //}
//    function InvokeProducts() {
//        connection.invoke("SendProducts").catch(function (error) {
//            return console.error(error.toString());
//        })
//    }
//    function BindData(result) {
//        var tr = '';
//        $.each(result, (k, v) => {
//            tr += `<tr>
//                    <td>${v.ProductName}</td>
//                    <td>${v.Category}</td>
//                    <td>${v.UnitPrice}</td>
//                    <td>${v.StockQuantity}</td>
//                    <td>
//                    <a href='../Products/Edit?ProductID=${v.ProductID}'>Edit</a>
//                    <a href='../Products/Delete?ProductID=${v.ProductID}'>Delete</a>
//                    <a href='../Products/Details?ProductID=${v.ProductID}'>Details</a>
//                    </td>
//                    </tr>`
//        })
//        $("#tableBody").html(tr);
//    }
//})