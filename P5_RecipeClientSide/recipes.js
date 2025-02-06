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

    console.log(hits);
  } catch (error) {
    console.error(error);
  }
};

function displayData(data) {
  const targetDiv = document.getElementById("target-div");
  targetDiv.innerHTML = "";

  data.forEach((element) => {
    const title = document.createElement("h2");
    title.innerText = element.recipe.label;
    title.classList.add("h2");
    targetDiv.appendChild(title)



    const image = document.createElement("img");
    image.src = element.recipe.image;
    image.alt = element.recipe.title;
    image.classList.add("image-fluid");
    targetDiv.appendChild(image)


    const ingredientList = document.createElement("ul");
    ingredientList.classList.add("ingredient-list");



    element.recipe.ingredients.forEach((ingredient) => {
      const ingredientItem = document.createElement("li");
      ingredientItem.innerText = ingredient.text
      ingredientList.appendChild(ingredientItem);
    });

    targetDiv.appendChild(ingredientList);

  });
}


getData()