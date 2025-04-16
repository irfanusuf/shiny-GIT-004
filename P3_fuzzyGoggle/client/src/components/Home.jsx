import axios from "axios";
import React, { useCallback, useEffect, useState } from "react";
import loadingGif from "../assets/loading.gif";

const Home = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);

  const fetchData = useCallback(async () => {
    try {
      const response = await axios.get(
        "https://jsonplaceholder.typicode.com/posts"
      );

      setPosts(response.data);
    } catch (error) {
      console.log(error);
    }
  }, []);

  

  useEffect(() => {
    setTimeout(() => {
      fetchData();
      setLoading(false);
    }, 1000);
  }, [fetchData]);




  return (
    <div className="home">
      {loading ? (
        <div className="loading">
          <img src={loadingGif} alt="not available" />
        </div>
      ) : (
        <div className="posts animate__animated animate__backInLeft ">
          {posts.map((post) => (
            <div key={post.id} className="post">
              <h2>{post.title}</h2>
              <p>{post.body}</p>
            </div>
          ))}
        </div>
      )}

      {/* Alternative way to render posts */}
    </div>
  );
};

export default Home;
