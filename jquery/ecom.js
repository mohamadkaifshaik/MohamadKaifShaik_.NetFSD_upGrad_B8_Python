// $(document).ready(function () {
//   let count = 0;

//   $("#product-list").on("click", ".add-btn", function () {
//     count++;

//     $("#cart-count").text(count);

//     $(this).prop("disabled", true);

//     $(this).attr("data-added", "true");

//     $(this).siblings(".msg").text("Added to cart");
//   });
// });

// ------------------------------------------------------------------------------------------

$(document).ready(function () {
  let count = 0;
  $("#product-list").on("click", ".add-btn", function () {
    count++;
    $("#cart-count").text(count);
    $(this).prop("disabled", true);
    $(this).attr("data-added", "true");
    $(this).siblings(".msg").text("✔ Added to cart");
  });
});
