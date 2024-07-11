import { MinusOutlined, PlusOutlined } from "@ant-design/icons"
import { Card, Col, Divider, Flex, FloatButton, Input, Row } from "antd"
import TextArea from "antd/es/input/TextArea";
import { useState } from "react"

const NewNote: React.FC = () => {

    const [buttonIcon, changeIcon] = useState(<PlusOutlined />);
    const [isExpand, setExpand] = useState(false)
    const toggleExpand = () => {

        console.log(isExpand)

        if (isExpand) {
            setExpand(false)
            changeIcon(<PlusOutlined />)
        } else {
            setExpand(true)
            changeIcon(<MinusOutlined />)
        }
    }

    return (
        <>
            <FloatButton icon={buttonIcon} onClick={() => toggleExpand()} />
            <Card style={{ marginTop: '15px', marginBottom: '15px', display: isExpand ? 'block' : 'none' }}>
                <Row>
                    <Col style={{ width: '100%' }}>
                        <TextArea rows={5} placeholder="How are you today?" />
                    </Col>
                    <Col style={{width: '100%'}}>
                        <Flex justify="right">
                            <Input type="button" style={{width: 'auto'}} value={'Smth'} />
                        </Flex>
                    </Col>
                </Row>
            </Card>
        </>
    )
}

export default NewNote