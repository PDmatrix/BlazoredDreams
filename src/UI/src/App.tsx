import React from "react";
import Main from "./Pages/Main";
import NotFound from "./Pages/NotFound";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

const App = () => {
  return (
    <Router>
      <Switch>
        <Route path="/" exact component={Main} />
        <Route component={NotFound} />
      </Switch>
    </Router>
  );
};

export default App;
