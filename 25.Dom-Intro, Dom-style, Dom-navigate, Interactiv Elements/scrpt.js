const card = document.getElementById("card");

const container = document.createElement("div");
container.className = "house-card";
card.appendChild(container);

const img = document.createElement("img");
img.src = "pexels-ahmetcotur-31817157.jpg";
img.className = "card-img";
container.appendChild(img);

const favBtn = document.createElement("div");
favBtn.className = "fav-btn";
favBtn.innerHTML = "♡";
container.appendChild(favBtn);

favBtn.addEventListener("click", () => {
    favBtn.classList.toggle("active");
    favBtn.innerHTML = favBtn.classList.contains("active") ? "❤️" : "♡";
});

const content = document.createElement("div");
content.className = "content";
container.appendChild(content);

const type = document.createElement("div");
type.className = "type";
type.innerText = "DETACHED HOUSE • 5Y OLD";
content.appendChild(type);

const price = document.createElement("div");
price.className = "price";
price.innerText = "$750,000";
content.appendChild(price);

const address = document.createElement("div");
address.className = "address";
address.innerText = "742 Evergreen Terrace";
content.appendChild(address);

const info = document.createElement("div");
info.className = "info-section";
info.innerHTML = `
    <div class="info-item">🛏️ 3 Bedrooms</div>
    <div class="info-item">🛁 2 Bathrooms</div>
`;
content.appendChild(info);

const realtorBox = document.createElement("div");
realtorBox.className = "realtor";
realtorBox.innerHTML = `
    <img class="agent-img" src="https://i.pravatar.cc/80?img=5">
    <div>
        <div class="agent-name">Tiffany Heffner</div>
        <div class="agent-phone">(555) 555-4321</div>
    </div>
`;
content.appendChild(realtorBox);

const style = document.createElement("style");
style.innerHTML = `
    .house-card {
        width: 360px;
        border-radius: 12px;
        overflow: hidden;
        background: #fff;
        position: relative;
        font-family: Arial, sans-serif;
        border: 1px solid #ddd;
    }

    .card-img {
        width: 100%;
        height: 210px;
        object-fit: cover;
    }

    .fav-btn {
        position: absolute;
        top: 14px;
        right: 14px;
        background: #ffffffcc;
        padding: 6px 10px;
        font-size: 20px;
        border-radius: 8px;
        cursor: pointer;
        transition: 0.2s;
    }

    .fav-btn.active {
        background: #ffe1e1;
    }

    .content {
        padding: 16px;
    }

    .type {
        color: #444;
        font-size: 12px;
        letter-spacing: 0.5px;
        margin-bottom: 5px;
    }

    .price {
        font-size: 28px;
        font-weight: bold;
        margin-bottom: 4px;
    }

    .address {
        color: #777;
        margin-bottom: 14px;
    }

    .info-section {
        display: flex;
        gap: 20px;
        padding: 12px 0;
        border-top: 1px solid #eee;
        border-bottom: 1px solid #eee;
        margin-bottom: 14px;
    }

    .info-item {
        font-size: 14px;
        display: flex;
        gap: 6px;
        align-items: center;
    }

    .realtor {
        display: flex;
        align-items: center;
        gap: 10px;
    }

    .agent-img {
        width: 42px;
        height: 42px;
        border-radius: 50%;
    }

    .agent-name {
        font-weight: bold;
        font-size: 15px;
    }

    .agent-phone {
        color: #666;
        font-size: 13px;
    }
`;
document.head.appendChild(style);