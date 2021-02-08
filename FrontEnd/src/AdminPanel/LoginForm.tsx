import React, { FunctionComponent, useContext } from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row } from 'antd'
import { Form, Input, SubmitButton } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { RouteComponentProps, withRouter } from 'react-router-dom'
import { routePaths } from '../Routing/routePaths'
import { httpClient } from '../core/ApiClient'
import { Context } from '../Context/Context'

export const LoginForm: FunctionComponent<RouteComponentProps> = ({
    history,
}) => {
    const [, setToken, , setRole] = useContext(Context)
    return (
        <>
            <h1> Zaloguj się do panelu admina </h1>
            <Formik
                onSubmit={async ({ userName, password }, actions) => {
                    const {
                        accessToken,
                        role,
                    } = await httpClient.post(
                        'https://localhost:44385/users/login',
                        { userName, password }
                    )
                    if (accessToken) {
                        setToken(accessToken)
                        setRole(role)
                        history.push(routePaths.ADMIN_PANEL)
                    } else {
                        actions.setSubmitting(false)
                    }
                }}
                initialValues={{ userName: '', password: '' }}
                >
                <Form>
                    <AntdInput.Group
                        style={{ display: 'flex', justifyContent: 'center' }}
                    >
                        <Col>
                            <Row>
                                <Input
                                    name="userName"
                                    bordered
                                    prefix={<UserOutlined />}
                                    style={{ width: 300, margin: 10 }}
                                    placeholder="Nazwa użytkownika"
                                />
                            </Row>
                            <Row>
                                <Input.Password
                                    name="password"
                                    placeholder="Hasło"
                                    style={{ width: 300, margin: 10 }}
                                />
                            </Row>
                            <Row justify="center">
                                <SubmitButton style={{ margin: 10 }}>
                                    {' '}
                                    Zaloguj się{' '}
                                </SubmitButton>
                            </Row>
                        </Col>
                    </AntdInput.Group>
                </Form>
            </Formik>
        </>
    )
}

export default withRouter(LoginForm)
