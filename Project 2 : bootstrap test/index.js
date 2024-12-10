

const fetchData = async () => {
  try {
  
    const dataRequset = await fetch("https://jsonplaceholder.typicode.com/todos");   // fetch api call  // method : GET 

    const dataArr = await dataRequset.json();

    console.log(dataArr);


    document.getElementById("length").innerText = dataArr.length

    // dataArr.map((element) => 

    //  const div = 


    // )

  } catch (error) {
    console.error(error);
  }
};





