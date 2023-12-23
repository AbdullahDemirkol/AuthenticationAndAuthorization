
$(document).ready(function () {
    // Belirli bir düğmeye tıklandığında u sınıfını kaldır
    $('.navbar-toggler').on('click', function () {
        $('.navbar-collapsee').removeClass('show');
    });
});