document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('loginForm');
    form.addEventListener('submit', function (event) {
        if (!validateForm()) {
            event.preventDefault();
        }
    });

    function validateForm() {
        var username = document.getElementById('username').value;
        var password = document.getElementById('password').value;

        var isValid = true;

        if (username.trim() === '') {
            isValid = false;
            alert('Por favor, insira um nome de usuário.');
        }

        if (password.trim() === '') {
            isValid = false;
            alert('Por favor, insira uma senha.');
        }

        return isValid;
    }
});