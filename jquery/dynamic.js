$(document).ready(function () {
  $(".question").click(function () {
    $(this).next(".answer").slideToggle();

    $(".question").not(this).removeClass("active");

    $(this).toggleClass("active");
  });
});
