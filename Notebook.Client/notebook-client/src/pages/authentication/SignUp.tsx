import { Card, Col, Divider, Input, Row } from "antd"
import { useState } from "react"

const SignUp: React.FC = () => {

    const [step, move] = useState(0);

    return (
        <div>
            <Row style={{height: '100vh'}} align={'middle'} justify={'center'}>
                <Col>
                    <Card>
                        <div style={{display: step === 0 ? 'block' : 'none'}}>
                            <Input type="text" placeholder="First name" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Last name" style={{marginBottom: '10px'}} />
                        </div>

                        <div style={{display: step === 1 ? 'block' : 'none'}}>
                            <Input type="text" placeholder="Email" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Login" style={{marginBottom: '10px'}} />
                        </div>

                        <div style={{display: step === 2 ? 'block' : 'none'}}>
                            <Input type="text" placeholder="Password" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Confirm password" style={{marginBottom: '10px'}} />
                        </div>

                        <Divider />
                        <Row>
                            <Col span={6} style={{display: step === 0 ? 'none' : 'block'}}>
                                <Input type="button" value={'Previous'} style={{cursor: 'pointer'}} onClick={() => move(step - 1)} />
                            </Col>
                            
                            <Col span={6} push={step === 0 ? 18 : 12} style={{display: step === 2 ? 'none' : 'block'}}>
                                <Input type="button" value={'Next'} style={{cursor: 'pointer'}} onClick={() => move(step + 1)} />
                            </Col>
                            
                            <Col span={6} push={12} style={{display: step === 2 ? 'block' : 'none'}}>
                                <Input type="button" value={'Complete'} style={{cursor: "pointer" }} />
                            </Col>
                        </Row>
                    </Card>
                </Col>
            </Row>
        </div>
    )
}

export default SignUp