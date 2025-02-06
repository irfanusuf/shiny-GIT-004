document.getElementById("login").addEventListener("click", async (e) => {
  try {
    e.preventDefault()
    // const username = document.getElementById("username").value;  
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const formData = { email, password };

    console.log(formData)
    const url = "http://localhost:4003/user/login";

    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });
      
    const data = await response.json();

    console.log(data);

    if (data.success) {
    
      setTimeout(() => {
        window.location.href = "/dashboard.html";
      }, 3000);

      window.alert(data.message);  
     
    }
    else{
        window.alert(data.message)
    }
  } catch (error) {
    console.log(error);
  }
});
