document.addEventListener('DOMContentLoaded', function () {
    const registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', function (event) {
        event.preventDefault(); 
        submitRegisterForm();
    });
});

function submitRegisterForm() {
    const registerForm = document.getElementById('registerForm');

    fetch(registerForm.action, {
        method: 'POST',
        body: new FormData(registerForm),
       
    })
        .then(response => {
            console.log('Status da resposta:', response.status);
            console.log('Cabeçalhos da resposta:', response.headers.get('content-type'));

            if (!response.ok) {
                throw new Error('Ocorreu um problema ao enviar o formulário.');
            }

            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return response.json();
            } else {
                console.error('Resposta do servidor não é JSON válido.');
                throw new Error('Resposta do servidor não é JSON válido.');
            }
        })
        .then(data => {
            console.log('Resposta do servidor:', data);

            if (data.success) {
                alert('Registro concluído com sucesso! Ano: ' + data.ano);

            } else {
                console.error('Ocorreu um problema ao adicionar o carro:', data.message);
                alert('Ocorreu um problema ao adicionar o carro: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Erro ao enviar o formulário:', error);
            alert('Ocorreu um erro ao processar a solicitação. Por favor, tente novamente mais tarde.');
        });
}