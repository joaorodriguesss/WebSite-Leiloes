document.getElementById('loginForm').addEventListener('submit', function (event) {
    event.preventDefault();
    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;
    console.log('Login with', username, password);
    // Here you should add your logic to handle the login
});

function createAccount() {
    // Here you can add your logic to handle account creation
    console.log('Redirect to account creation');
}