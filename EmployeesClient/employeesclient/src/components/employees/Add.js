import { Component } from 'react';
import axios from 'axios';

import {
    Container,
    FormGroup,
    Button,
    Input,
    Form,
    Label,
    Col,
    Alert
} from 'reactstrap';

class AddEmployee extends Component {

    constructor (props) {
        super (props);

        var token = localStorage.getItem("ACCESS_TOKEN");

        this.state = {
            empNo: '',
            firstName: '',
            lastName: '',
            birthDate: '',
            hireDate: '',
            token: token,
            isSubmitted: '',
            error: ''
        }

        this.handleChange = this.handleChange.bind(this);
        this.add = this.add.bind(this);
        this.cancel = this.cancel.bind(this);
    }

    handleChange (e) {
        this.setState (
            {
                [e.target.name]: e.target.value
            }
        )
    }

    add (e) {
        axios.post ("http://localhost:5000/api/employees", 
            {
                empNo: this.state.empNo,
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                birthDate: this.state.birthDate,
                hireDate: this.state.hireDate,
                gender: 'F'
            }, 
            {
                headers: {
                    'Content-type': "application/json",
                    'Authorization': `Bearer ${this.state.token}`
                }
            }
        ).then (
            (response) => {
                if (response.status === 200) {
                    this.cancel ();
                    this.setState (
                        {
                            isSubmitted: true,
                            error: false
                        }
                    );
                }
            },
            (error) => {
                this.setState (
                    {
                        isSubmitted: true,
                        error: true
                    }
                );
                console.log(error);
            }
        );
    }

    cancel (e) {
        this.setState (
            {
                empNo: '',
                firstName: '',
                lastName: '',
                birthDate: '',
                hireDate: ''
            }
        );
    }

    render () {
        return (
            <Container className="App">
                <h4 className="PageHeading">Enter employee infomation</h4>
                <Alert
                    isOpen={this.state.isSubmitted}
                    color={!this.state.error ? "success" : "warning"}
                    toggle={() => this.setState ({isSubmitted: false})}
                >
                    {!this.state.error ? "Information was saved!" : "An error occurs while trying to save information"}
                </Alert>
                <Form className="form">
                    <Col>
                        <FormGroup row>
                            <Label for="name" sm={2}>No. Employee</Label>
                            <Col sm={2}>
                                <Input type="text" name="empNo" onChange={this.handleChange} value={this.state.empNo} />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="name" sm={2}>First Name</Label>
                            <Col sm={2}>
                                <Input type="text" name="firstName" onChange={this.handleChange} value={this.state.firstName} />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="name" sm={2}>Last Name</Label>
                            <Col sm={2}>
                                <Input type="text" name="lastName" onChange={this.handleChange} value={this.state.lastName} />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="name" sm={2}>Birth Date</Label>
                            <Col sm={2}>
                                <Input bsSize="md" type="date" name="birthDate" value={this.state.birthDate} onChange={this.handleChange} />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="name" sm={2}>Hire Date</Label>
                            <Col sm={2}>
                                <Input bsSize="md" type="date" name="hireDate" onChange={this.handleChange} value={this.state.hireDate} />
                            </Col>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup row>
                            <Col sm={5}>
                            </Col>
                            <Col sm={1}>
                                <Button color="primary" onClick={this.add}>Submit</Button>
                            </Col>
                            <Col sm={1}>
                                <Button color="secondary" onClick={this.cancel}>Cancel</Button>{' '}
                            </Col>
                            <Col sm={5}>
                            </Col>
                        </FormGroup>
                    </Col>
                </Form>
            </Container>
        );
    }

}

export default AddEmployee;