import { Col, Row, Card, Divider, Flex } from "antd";
import { useState } from "react";

type NoteProps = {
    author: string,
    releaseDate: Date,
    content: string
}

const Note: React.FC<NoteProps> = (props: NoteProps) => {

    const [isExpand, setExpand] = useState(false)
    const toggleExpand = () => { setExpand(!isExpand) }

    return (
        <Card>
            <Row style={{marginTop: '5px', marginBottom: '5px'}}>
                <Col span={12}>
                    <Flex justify="left">
                        {props.author}
                    </Flex>
                </Col>
                <Col span={12}>
                    <Flex justify="right">
                        {props.releaseDate.toDateString()}
                    </Flex>
                </Col>
            </Row>

            <hr />

            <Row style={{marginTop: '5px', marginBottom: '5px'}}>
                <Col span={24}>{props.content}</Col>
            </Row>

            <hr />

            <Row style={{marginTop: '5px', marginBottom: '5px'}}>
                <Col span={12}>
                    <Flex justify="left">
                        <a href="#" onClick={() => toggleExpand()}>Comment</a>
                    </Flex>
                </Col>
                <Col span={12}>
                    <Flex justify="right">
                        <a href="#">Like</a>
                    </Flex>
                </Col>
            </Row>

            <Row style={{display: isExpand ? 'block' : 'none'}}>
                <Col span={24}>
                    This is Comment
                </Col>
            </Row>
        </Card>
    )
}

export default Note;