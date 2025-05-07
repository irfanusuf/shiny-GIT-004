import ReactDOM from "react-dom/client"; // node modules se import
import App from "./App";
import {store} from "./redux/store"
import {Provider} from "react-redux"




const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(

<Provider store = {store}>

<App/>

</Provider>


);
