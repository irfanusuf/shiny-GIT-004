const getData = async () => {
  try {
    const endpoint = "http://localhost:4003/fetch-data";

    const response = await fetch(endpoint, {
      method: "POST",
    });

    const data = await response.json();
    // const hits = data.hits

    const { hits } = data;

    displayData(hits);

    console.log(data);
  } catch (error) {
    console.error(error);
  }
};

function displayData(data) {
  const targetDiv = document.getElementById("target-div");
  targetDiv.innerHTML = "";

  data.forEach((element) => {
    const title = document.createElement("h2");
    title.innerText = element.label;
    title.classList.add("h2");

    const image = document.createElement("img");
    image.src = element.image;
    image.alt = element.title;
    image.classList.add("image-fluid");

    element.ingredients.forEach((ingredient) => {
      // const unorderedList = document.createElement
      console.log(ingredient);
    });
  });
}


getData()