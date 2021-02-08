import Descriptions from 'antd/lib/descriptions'
import { FormikValues } from 'formik'
import { Form, Input } from 'formik-antd'
import { Formik } from 'formik'
import React, { FunctionComponent, useCallback } from 'react'
import { httpClient } from '../../core/ApiClient'
import { IProjectData } from '../IProjectData'
import { Button, notification } from 'antd'
import { CloseOutlined, CheckOutlined } from '@ant-design/icons'
import { ErrorMessage } from './ProjectVoteForm.components'
import { placeVote } from '../../Endpoints/Endpoints'
import { Newsletter } from '../../Newsletter'
export interface IVoteFormProps {
    id: number
}

export const ProjectVoteForm: FunctionComponent<IVoteFormProps> = ({ id }) => {
    const sendVote = useCallback((pesel: string) => {
        //httpClient.post()
        console.log(id)
    }, [])

    const isPeselValid = useCallback((pesel: string) => {
        const reg = /^[0-9]{11}$/
        if (reg.test(pesel) === false) return false
        else {
            var digits = ('' + pesel).split('')
            if (
                parseInt(pesel.substring(4, 6)) > 31 ||
                parseInt(pesel.substring(2, 4)) > 12
            )
                return false

            var checksum =
                (1 * parseInt(digits[0]) +
                    3 * parseInt(digits[1]) +
                    7 * parseInt(digits[2]) +
                    9 * parseInt(digits[3]) +
                    1 * parseInt(digits[4]) +
                    3 * parseInt(digits[5]) +
                    7 * parseInt(digits[6]) +
                    9 * parseInt(digits[7]) +
                    1 * parseInt(digits[8]) +
                    3 * parseInt(digits[9])) %
                10
            if (checksum === 0) checksum = 10
            checksum = 10 - checksum

            return parseInt(digits[10]) === checksum
        }
    }, [])

    const validate = useCallback(
        (values) => {
            const pesel: string | undefined = values.pesel

            if (!pesel) {
                return {
                    pesel: 'Musisz podać pesel',
                }
            }

            if (!isPeselValid(pesel)) {
                return {
                    pesel: 'To nie jest poprawny numer pesel',
                }
            }

            return {}
        },

        []
    )

    const handlePlaceVote = async (pesel: string, giveVote: boolean) => {
        // try {
        placeVote(
            id.toString(),
            {
                VoterPesel: pesel,
                giveVote,
            },
            true
        )
            .then((response) => {
                if (
                    response?.error ===
                        "User with that PESEL hasn't casted a vote yet" ||
                    response?.error ===
                        'User with that PESEL has casted a vote already.'
                ) {
                    throw new Error('Something went wrong')
                }
                notification.open({
                    message: giveVote ? 'Oddałeś głos' : 'Cofnięto głos',
                    description: <Newsletter />,
                    icon: <CheckOutlined style={{ color: 'green' }} />,
                })
            })
            .catch((err) =>
                notification.open({
                    message: 'Coś poszło nie tak!',
                    icon: <CloseOutlined style={{ color: 'red' }} />,
                })
            )
    }

    return (
        <Formik
            initialValues={{ pesel: '' }}
            validate={validate}
            onSubmit={(values: FormikValues) => {}}
            render={({ errors, touched, values }) => (
                <Form
                    style={{
                        width: 500,
                    }}
                >
                    {errors.pesel && touched.pesel && (
                        <ErrorMessage>{errors.pesel}</ErrorMessage>
                    )}
                    <Input
                        name="pesel"
                        placeholder="Pesel"
                        style={{ marginBottom: 15 }}
                    />
                    <Button
                        onClick={() => handlePlaceVote(values.pesel, true)}
                        type="primary"
                        style={{ margin: 10 }}
                    >
                        Oddaj głos
                    </Button>
                    <Button
                        onClick={() => {
                            handlePlaceVote(values.pesel, false)
                        }}
                        danger
                        type="primary"
                        style={{ margin: 10 }}
                    >
                        Cofnij głos
                    </Button>
                </Form>
            )}
        />
    )
}

export default ProjectVoteForm
