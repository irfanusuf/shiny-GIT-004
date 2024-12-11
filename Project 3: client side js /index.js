const fetchCuratedImages = async (page) => {
  try {
    const apiKey = "EbvixQkEPE7kqKI2qHd0ackQCu5roaHHF5jiJvSykgsxxKTcf80F2vKY";
    const endpoint = `https://api.pexels.com/v1/curated?page=${page}&per_page=40`;

    // network call
    const response = await fetch(endpoint, {
      method: "GET",
      headers: {
        Authorization: apiKey,
      },
    });

    // cpu intensive task
    const data = await response.json();

    const photos = data.photos; // photos is  array which comes from pexels api

    displayImages(photos); // invoke function

    // console.log(photos);
  } catch (error) {
    console.log(error);
  }
};

const fetchSearchImages = async (query) => {
  try {
    const apiKey = "EbvixQkEPE7kqKI2qHd0ackQCu5roaHHF5jiJvSykgsxxKTcf80F2vKY";
    const endpoint = `https://api.pexels.com/v1/search?query=${query}&per_page=40`;

    const response = await fetch(endpoint, {
      method: "GET",
      headers: {
        Authorization: apiKey,
      },
    });

    const data = await response.json();
    const photos = data.photos; // photos is  array which comes from pexels api

    displayImages(photos); // invoke function
  } catch (error) {
    console.error(error);
  }
};

const displayImages = (photos) => {
  const targetDiv = document.getElementById("target-div");
  targetDiv.innerHTML = "";

  photos.forEach((photo) => {
    const card = document.createElement("div");
    card.classList.add("card");

    const image = document.createElement("img");
    image.classList.add("card-img-top");
    image.src = photo.src.original;

    const cardBody = document.createElement("div");
    cardBody.classList.add("card-body");

    const h4 = document.createElement("h4");
    h4.classList.add("card-title");

    h4.innerText = photo.photographer;

    // dom heirarachy
    targetDiv.appendChild(card);
    card.appendChild(image);
    card.appendChild(cardBody);

    cardBody.appendChild(h4);
  });
};

const prev = document.getElementById("prev");
const next = document.getElementById("next");
const search = document.getElementById("search");

search.addEventListener("click", () => {
  const query = document.getElementById("query").value;
  // console.log(query)

  if (query) {
    fetchSearchImages(query);
  }
});

let page = 1;

prev.addEventListener("click", () => {
  page--; //count =0

  if (page > 0) {
    console.log("page no :" + page);
    fetchCuratedImages(page);
  }
});

next.addEventListener("click", () => {
  page++; //count =0

  if (page < 80) {
    console.log("page no :" + page);
    fetchCuratedImages(page);
  }
});

fetchCuratedImages(page);
