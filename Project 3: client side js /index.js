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

    console.log(photos);
  } catch (error) {
    console.log(error);
  }
};




const fetchSerachImages = async(query , page) =>{

try {
    



} catch (error) {
    
}


}


const displayImages = (photos) => {


const targetDiv = document.getElementById("target-div")
targetDiv.innerHTML = ""


  photos.forEach((photo) => {
    const card = document.createElement("div");
    card.classList.add("card");

    const image = document.createElement("img");
    image.classList.add("card-img-top");
    image.src = photo.src.medium;

    const cardBody = document.createElement("div");
    cardBody.classList.add("card-body");

    const h4 = document.createElement("h4");
    h4.classList.add("card-title");

    h4.innerText = photo.photographer;

    // dom heirarachy
    targetDiv.appendChild(card)
    card.appendChild(image);
    card.appendChild(cardBody);

    cardBody.appendChild(h4);
  });
};



const prev = document.getElementById("prev")

let count = 1

prev.addEventListener("click" , ()=>{

    if(count > 1){
        fetchCuratedImages(count--);
    }

})



fetchCuratedImages(1)






