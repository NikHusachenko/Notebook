import { Col, Row, Card, Input, Flex } from "antd"

const SignIn : React.FC = () => {
    return (
        <Row style={{height: '100vh'}} align={'middle'} justify={'center'}>
            <Col>
                <Card>
                    <Input type="text" placeholder="Login" style={{marginBottom: '10px'}} />
                    <Input type="password" placeholder="Password" style={{marginBottom: '10px'}} />
                    <Input type="button" value={'Login'} style={{cursor: 'pointer'}} />
                </Card>
            </Col>
        </Row>
    )
}

export default SignIn