document.addEventListener('DOMContentLoaded', function () {
    var starImage = document.querySelector('.secondary-image');
    if (starImage) {
        starImage.addEventListener('click', function () {
            toggleFavorite(this);
        });
    }

    window.toggleFavorite = function (element) {
        var leilaoId = element.getAttribute('data-leilao-id');
        if (!leilaoId) {
            console.error('ID do leilão não encontrado');
            return;
        }
        fetch('/Leilao/Leilao?handler=ToggleFavorite', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ leilaoId: leilaoId })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    var starImageSrc = element.src.includes('estrela_vazia.png') ? '/images/estrela_dourada.png' : '/images/estrela_vazia.png';
                    element.src = starImageSrc;
                }
            })
            .catch(error => console.error('Error:', error));
    };
});
