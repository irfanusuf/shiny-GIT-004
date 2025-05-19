
import React, { useContext} from "react";
import loadingGif from "../assets/loading.gif";
import { Context } from "../context/Store";

const Home = () => {


const {posts , loading } = useContext(Context)





  return (
    <div className="home">
      {loading ? (
        <div className="loading">
          <img src={loadingGif} alt="not available" />
        </div>
      ) : (
        <div className="posts animate__animated animate__backInLeft ">

          {posts &&   posts.map((post) => (
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
