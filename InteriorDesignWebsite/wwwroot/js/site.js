document.addEventListener("DOMContentLoaded", function () {
    let images = document.querySelectorAll(".slideshow img");
    let index = 0;

    function switchImage() {
        images[index].classList.remove("active"); // Hide current image
        index = (index + 1) % images.length; // Move to next image
        images[index].classList.add("active"); // Show new image
    }

    setInterval(switchImage, 5000); // Change image every 5 seconds
});
