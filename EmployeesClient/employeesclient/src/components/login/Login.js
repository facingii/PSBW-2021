import React from "react";
import axios from "axios";

import { 
	Form, 
	FormGroup,
	Label, 
	Input, 
	Button 
} from "reactstrap";

import "./Login.css";

class Login extends React.Component {

	constructor (props) {
		super(props);

		this.state = {
			User: {},
			UserName: '',
			Password: '',
			Prev: props.location.state.from.pathname
		}
		this.doLogin = this.doLogin.bind(this);
	}

	doLogin () {
		axios.post ('http://localhost:5000/api/users/authenticate',
		{
			"firstName": "",
			"lastName": "",
			"userName": this.state.UserName,
			"password": this.state.Password,
			"token": ""
		},
		{
			headers: {
				'Content-type': 'application/json'
			}
		}).then (
			(response) => {
				if (response.status === 200) {
					const json = response.data;
					localStorage.setItem("ACCESS_TOKEN", json.token);
					console.log(json.token);
					this.props.history.push(this.state.Prev);
				}
			},
			(error) => {
				console.log("Exception " + error);
			}
		);
	}

	handleChange = (e) => {
		this.setState (
			{
				[e.target.name]: e.target.value
			}
		);
	}

	render () {
		return (
			<div className="Login">
				<Form>
					<FormGroup>
						<Label>Usuario</Label>
						<Input name="UserName" type="text" onChange={this.handleChange} value={this.state.UserName} />
					</FormGroup>
					<FormGroup>
						<Label>Contraseña</Label>
						<Input type="password" name="Password" onChange={this.handleChange} value={this.state.Password} />
					</FormGroup>
					<Button block type="button" onClick={this.doLogin}>
						login
					</Button>
				</Form>
			</div>
		);
	}

}

export default Login;
