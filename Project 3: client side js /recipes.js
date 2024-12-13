const getData = async () => {
  try {
    const endpoint = "http://localhost:4003/fetch-data";

    const response = await fetch(endpoint, {
      method: "POST",
    });

    const data = await response.json();

    console.log(data)
  } catch (error) {
    console.error(error);
  }
};




getData()