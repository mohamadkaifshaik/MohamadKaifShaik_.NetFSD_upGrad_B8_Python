// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Form validation
(function () {
    'use strict';

    // Bootstrap form validation
    var forms = document.querySelectorAll('.needs-validation');
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
})();

// Confirm delete
function confirmDelete() {
    return confirm('Are you sure you want to delete this item? This action cannot be undone.');
}

// Toast notifications (if needed)
function showToast(message, type) {
    var alertClass = 'alert-' + (type || 'info');
    var alertHTML = '<div class="alert ' + alertClass + ' alert-dismissible fade show" role="alert">' +
        message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert"></button>' +
        '</div>';
    document.querySelector('.container-fluid').insertAdjacentHTML('afterbegin', alertHTML);

    // Auto close after 5 seconds
    setTimeout(function () {
        var alert = document.querySelector('.alert');
        if (alert) {
            alert.remove();
        }
    }, 5000);
}

// Initialize tooltips
document.addEventListener('DOMContentLoaded', function () {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});
