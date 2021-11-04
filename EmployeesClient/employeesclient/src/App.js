import React from 'react';
import Login from './components/login/Login';
import ListEmployees from './components/employees/List';
import AddEmployee from './components/employees/Add';
import EditEmployee from './components/employees/Edit';

import {
	BrowserRouter as Router,
	Link,
	Switch,
	Route
} from 'react-router-dom'; 

import './App.css';
import { Container, Navbar, NavItem } from 'reactstrap';

function App (props) {
    return (
        <Router>
            <Container>
                <Navbar expand="lg" className="navheader">
                    <div className="collapse navbar-collapse">
                        <ul className="navbar-nav mr-auto">
                        <NavItem>
                            <Link to={'/addEmployee'} className="nav-link">Add Employee</Link>
                        </NavItem>
                        <NavItem>
                            <Link to={'/ListEmployee'} className="nav-link">Employee List</Link>
                        </NavItem>
                        </ul>
                    </div>
                </Navbar>
            </Container>
            <br />
            <Switch>
                <Route exact path='/ListEmployee' component={ListEmployees} />
                <Route exact path='/edit/:id' component={EditEmployee} />
                <Route exact path='/AddEmployee' component={AddEmployee} />
                <Route exact path='/login' component={Login} />
            </Switch>
        </Router>
    );
}

export default App;
