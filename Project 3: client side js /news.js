const endpoint =
  "https://newsapi.org/v2/everything?q=apple&from=2024-12-10&to=2024-12-10&sortBy=popularity&apiKey=d75ec0f277194bb6aa1b75d1ebeaf603";

const getNews = async () => {
  try {
    const response = await fetch(endpoint, {
      method: "GET",
    });

    const data = await response.json();

    console.log(data);
  } catch (error) {
    console.error(error);
  }
};


getNews()