document.addEventListener('DOMContentLoaded', function () {
    const registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', function (event) {
        event.preventDefault(); 

        if (validarFormulario()) {
 
            submitRegisterForm();
        }
    });
});

function validarFormulario() {
    const senha = document.getElementsByName('Password')[0].value;
    const confirmarSenha = document.getElementsByName('ConfirmPassword')[0].value;

    if (senha !== confirmarSenha) {
        alert('As senhas não coincidem.');
        return false;
    }

    return true; 
}

function submitRegisterForm() {
    const registerForm = document.getElementById('registerForm');

    fetch(registerForm.action, {
        method: 'POST',
        body: new FormData(registerForm),
    })
        .then(response => {
            if (response.ok) {
                return response.json(); 
            }
            throw new Error('Ocorreu um problema ao enviar o formulário.');
        })
        .then(data => {
            console.log(data);
            alert('Registro concluído com sucesso!');

        })
        .catch(error => {
            console.error('Erro ao enviar o formulário:', error);
            alert(error.message);
        });
}