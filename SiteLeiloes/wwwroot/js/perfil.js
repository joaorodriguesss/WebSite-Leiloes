document.addEventListener('DOMContentLoaded', function () {
    var stars = document.querySelectorAll('.star');
    var starRating = document.querySelector('.star-rating');

    stars.forEach(function (star) {
        star.addEventListener('click', function () {
            stars.forEach(function (s) {
                s.classList.remove('hover');
            });

            this.classList.add('active');
            var previousStars = Array.from(this.previousSiblings());
            previousStars.forEach(function (previousStar) {
                previousStar.classList.add('active');
            });
            var nextStars = Array.from(this.nextElementSibling ? this.nextElementSibling.nextSiblings() : []);
            nextStars.forEach(function (nextStar) {
                nextStar.classList.remove('active');
            });
            starRating.setAttribute('data-rating', star.getAttribute('data-value'));
        });

        star.addEventListener('mouseover', function () {
            var previousStars = Array.from(this.previousSiblings());
            previousStars.forEach(function (previousStar) {
                previousStar.classList.add('hover');
            });
        });

        star.addEventListener('mouseout', function () {
            stars.forEach(function (s) {
                s.classList.remove('hover');
            });
        });
    });

    HTMLElement.prototype.previousSiblings = function () {
        var siblings = Array.from(this.parentNode.children);
        return siblings.slice(0, siblings.indexOf(this));
    };
});