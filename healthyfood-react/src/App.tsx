import About from "./components/About";
import Home from "./components/Home";
import Privacy from "./components/Privacy";
import Header from "./components/shared/Header";

import React from "react";

// ARROW FUNCTION COMPONENT STYLE

const getPage = () => {
    const route = window.location.pathname;
    if (route === "/About") return <About/>;
    if (route === "/Privacy") return <Privacy/>
    return <Home />;
}

const App = () => (
    <>
        <Header />
        <div className="container">
            <main role="main" className="pb-3">
                { getPage() }
            </main>
        </div>
    </>
);

export default App;