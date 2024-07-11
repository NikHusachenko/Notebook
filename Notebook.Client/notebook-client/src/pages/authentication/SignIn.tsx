import { Col, Row, Card, Input, Flex } from "antd"

const SignIn : React.FC = () => {
    return (
        <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
            <Col>
                <Card title="Sign in">
                    <Input type="text" placeholder="Login" style={{marginBottom: '10px'}} />
                    <Input type="password" placeholder="Password" style={{marginBottom: '10px'}} />
                    <Input type="button" value={'Login'} style={{cursor: 'pointer'}} />
                </Card>
            </Col>
        </Row>
    )
}

export default SignIn