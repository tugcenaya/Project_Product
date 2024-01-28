"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalServer").build();

$(function () {
    connection.start().then(function () {
		alert('Connected to Hub');

    saveProduct();

    }).catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("ProductSaved", function (product) {
      console.log("Product saved:", product);

  });
  connection.on("ReceiveProductsWithCategory", function (products) {
    console.log("Received updated products:", products);
});
  
  // Save işlemini gerçekleştiren fonksiyon
  function saveProduct(product) {
      connection.invoke("SaveProduct", product).catch(function (err) {
          return console.error(err.toString());
      });
  }

});

