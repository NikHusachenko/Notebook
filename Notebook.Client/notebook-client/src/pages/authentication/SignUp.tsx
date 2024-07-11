import { Card, Col, Divider, Input, Row } from "antd"
import React, { useState } from "react"

const SignUp : React.FC = () => {

    const [step, move] = useState(0);

    const GetDisplayNone = (expiredStep : number) : React.CSSProperties => {
        return GetDisplay(expiredStep, step, 'none', 'block')
    }
    
    const GetDisplayBlock = (expiredStep : number) : React.CSSProperties => {
        return GetDisplay(expiredStep, step, 'block', 'none')
    }
    
    const GetDisplay = (expiredStep : number, realStep : number, propValue : string, alternativeValue : string) : React.CSSProperties => {
        return { display: expiredStep === realStep ? propValue : alternativeValue }
    }

    return (
        <div>
            <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
                <Col>
                    <Card title="Sign Up">
                        <div style={GetDisplayBlock(0)}>
                            <Input type="text" placeholder="First name" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Last name" style={{marginBottom: '10px'}} />
                        </div>

                        <div style={GetDisplayBlock(1)}>
                            <Input type="text" placeholder="Email" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Login" style={{marginBottom: '10px'}} />
                        </div>

                        <div style={GetDisplayBlock(2)}>
                            <Input type="text" placeholder="Password" style={{marginBottom: '10px'}} />
                            <Input type="text" placeholder="Confirm password" style={{marginBottom: '10px'}} />
                        </div>

                        <Divider />
                        <Row>
                            <Col span={6} style={GetDisplayNone(0)}>
                                <Input type="button" value={'Previous'} style={{cursor: 'pointer'}} onClick={() => move(step - 1)} />
                            </Col>
                            
                            <Col span={6} push={step === 0 ? 18 : 12} style={GetDisplayNone(2)}>
                                <Input type="button" value={'Next'} style={{cursor: 'pointer'}} onClick={() => move(step + 1)} />
                            </Col>
                            
                            <Col span={6} push={12} style={ GetDisplayBlock(2) }>
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