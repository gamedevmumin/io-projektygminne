import React, {
    FunctionComponent,
    useContext,
    useState,
    useEffect,
} from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row, Typography, message } from 'antd'
import { Form, Input, SubmitButton, InputNumber, Select } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { Context } from '../../Context/Context'
import { httpClient } from '../../core/ApiClient'
import { getDistricts } from '../../Endpoints/Endpoints'
import { useHistory } from 'react-router-dom'
import { getAddPhotosURL, routePaths } from '../../Routing/routePaths'

const { Option } = Select

export const AddProjectPage: FunctionComponent = (props) => {
    const [token] = useContext(Context)
    const [districts, setDistricts] = useState([])
    const history = useHistory()

    useEffect(() => {
        const fetchDistricts = async () => {
            const tempDistricts = await getDistricts()
            setDistricts(tempDistricts)
        }
        fetchDistricts()
        console.log(districts)
    }, [JSON.stringify(districts)])

    return (
        <>
            <Typography.Title>Dodaj Projekt</Typography.Title>
            <Formik
                onSubmit={(
                    {
                        name,
                        description,
                        district,
                        amount,
                        estimatedTimeInDays,
                    },
                    actions
                ) => {
                    httpClient
                        .post(
                            'https://localhost:44385/projects',
                            {
                                name,
                                description,
                                districtId: district,
                                pricePln: amount,
                                estimatedTimeInDays,
                            },
                            {
                                Authorization: `Bearer ${token}`,
                            }
                        )
                        .then((proj) => {
                            actions.setSubmitting(false)
                            history.push(getAddPhotosURL(proj.id))
                        })
                        .catch(() => {
                            message.error('Nie udało sie dodac sprawdz dane')
                        })
                }}
                initialValues={{
                    name: '',
                    description: '',
                    district: 'Dzielnica',
                    amount: 0,
                    estimatedTimeInDays: 0,
                }}
            >
                <Form {...{ labelCol: { span: 8 }, wrapperCol: { span: 16 } }}>
                    <AntdInput.Group>
                        <Form.Item label="Nazwa Projektu" name="name">
                            <Input
                                name="name"
                                bordered
                                style={{ width: 300 }}
                                placeholder="Nazwa projektu"
                            />
                        </Form.Item>
                        <Form.Item label="Dzielnica" name="district">
                            <Select
                                name="district"
                                bordered
                                style={{ width: 300 }}
                            >
                                {districts?.map(({ id, name }) => (
                                    <Option key={id} value={id}>
                                        {name}
                                    </Option>
                                ))}
                            </Select>
                        </Form.Item>
                        <Form.Item label="Koszt" name="ammount">
                            <InputNumber
                                name="amount"
                                style={{ width: 300 }}
                                formatter={(
                                    value: string | number | undefined
                                ) => `${value} zł`}
                                parser={(value: string | undefined) =>
                                    value?.replace('zł', '').replace(' ', '') ||
                                    ''
                                }
                                placeholder="Koszt projektu"
                            />
                        </Form.Item>
                        <Form.Item label="Opis" name="description">
                            <Input
                                name="description"
                                bordered
                                style={{ width: 300 }}
                                placeholder="Opis"
                            />
                        </Form.Item>
                        <Form.Item
                            label="Przewidywany czas ukończenia"
                            name="estimatedTimeInDays"
                        >
                            <Input
                                name="estimatedTimeInDays"
                                bordered
                                addonAfter="dni"
                                style={{ width: 300 }}
                                placeholder="Przewidywany czas ukończenia"
                            />
                        </Form.Item>
                        <Form.Item name="submitButton">
                            <SubmitButton style={{}}>
                                Dodaj projekt
                            </SubmitButton>
                        </Form.Item>
                    </AntdInput.Group>
                </Form>
            </Formik>
        </>
    )
}

export default AddProjectPage
