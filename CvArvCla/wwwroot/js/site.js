
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        const msg = document.getElementById("likeMessage");
        if (msg) {
            msg.style.transition = "opacity 0.5s";
            msg.style.opacity = "0";
            setTimeout(() => msg.style.display = "none", 500); 
        }
    }, 1500); 
    // Hide the likes counter after 2 seconds
    setTimeout(function () {
        const counter = document.getElementById("likesCounter");
        if (counter) {
            counter.style.transition = "opacity 0.5s";
            counter.style.opacity = "0";
            setTimeout(() => counter.style.display = "none", 500);
        }
    }, 3000);
});

