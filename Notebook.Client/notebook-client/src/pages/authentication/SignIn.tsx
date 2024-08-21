import { Col, Row, Card, Input } from "antd"
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const SignIn : React.FC = () => {

    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const navigate = useNavigate();

    async function signIn() {
        let data = {
            login: login,
            password: password
        }
        let jsonData : string = JSON.stringify(data)

        let response = await fetch('https://localhost:7262/api/authentication/sign-in', {
            method: 'post',
            body: jsonData,
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            mode: 'cors'
        })

        if (response.status !== 204) {
            let responseData = await response.json()
            console.log(responseData)
        } else {
            console.log(204)
        }
    }

    return (
        <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
            <Col>
                <Card title="Sign in">
                    <Input type="text" placeholder="Login" style={{marginBottom: '10px'}} onChange={(e) => setLogin(e.target.value)} />
                    <Input type="password" placeholder="Password" style={{marginBottom: '10px'}} onChange={(e) => setPassword(e.target.value)} />
                    <Input type="button" value={'Login'} style={{cursor: 'pointer'}} onClick={async () => await signIn()} />
                </Card>
            </Col>
        </Row>
    )
}

export default SignIn