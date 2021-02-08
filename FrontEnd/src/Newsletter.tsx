import { Typography } from 'antd'
import React, { FunctionComponent } from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row } from 'antd'
import { Form, Input, SubmitButton, Select } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { subscribeToNewsletter } from './Endpoints/Endpoints'

export const Newsletter: FunctionComponent = (props) => {
    return (
        <>
            <Typography.Text>
                Zapisz się do newslettera, by dowiedzieć się o nowych edycjach
                jako pierwszy
            </Typography.Text>
            <Formik
                onSubmit={async ({ email }, actions) => {
                    subscribeToNewsletter(email, false)
                }}
                initialValues={{ email: '' }}
            >
                <Form>
                    <AntdInput.Group
                        style={{ display: 'flex', justifyContent: 'center' }}
                    >
                        <Col>
                            <Row>
                                <Input
                                    name="email"
                                    bordered
                                    prefix={<UserOutlined />}
                                    style={{ width: 300, margin: 10 }}
                                    placeholder="Email"
                                />
                            </Row>
                            <Row justify="center">
                                <SubmitButton style={{ margin: 10 }}>
                                    Zapisz się na newsletter
                                </SubmitButton>
                            </Row>
                        </Col>
                    </AntdInput.Group>
                </Form>
            </Formik>
        </>
    )
}
