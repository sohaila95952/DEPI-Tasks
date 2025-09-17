const adviceText = document.getElementById("adviceText");
const adviceId = document.getElementById("adviceId");
const diceBtn = document.getElementById("btnId");

diceBtn.addEventListener("click", () => {
    fetch("https://api.adviceslip.com/advice", { cache: "no-cache" })
        .then(response => response.json())
        .then(data => {
            adviceId.textContent = data.slip.id;
            adviceText.textContent = `"${data.slip.advice}"`;
        })
        .catch(error => {
            adviceText.textContent = "Could not load advice";
            console.error(error);
        });
});