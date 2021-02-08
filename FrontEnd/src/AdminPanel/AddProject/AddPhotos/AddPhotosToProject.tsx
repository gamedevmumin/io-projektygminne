import React, {
    FunctionComponent,
    useContext,
    useState,
    useEffect,
    useMemo,
} from 'react'
import { Formik } from 'formik'
import {
    Input as AntdInput,
    Col,
    Row,
    Typography,
    Upload,
    message,
    Descriptions,
} from 'antd'
import { Form, Input, SubmitButton, InputNumber, Select } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import Dragger, { DraggerProps } from 'antd/lib/upload/Dragger'
import { getDistricts, getProject } from '../../../Endpoints/Endpoints'
import { InboxOutlined } from '@ant-design/icons'
import { useAuthContext } from '../../../Context/Context'
import { RouteComponentProps, withRouter } from 'react-router-dom'

export const AddPhotosToProject: FunctionComponent<RouteComponentProps<{
    id: string
}>> = (props) => {
    const [token] = useAuthContext()
    const [project, setProject] = useState<any>()

    useEffect(() => {
        const fetchProject = async () => {
            const tempProject = await getProject(props.match.params.id)
            setProject(tempProject)
        }

        fetchProject()
    }, [props.match.params.id])

    const props2 = useMemo<DraggerProps>(
        () => ({
            name: 'files',
            multiple: true,
            action: `https://localhost:44385/projects/${project?.id}/images`,
            method: 'put',
            headers: {
                Authorization: `Bearer ${token}`,
            },
            style: { width: 500 },
            onChange(info: any) {
                const { status } = info.file
                if (status !== 'uploading') {
                    console.log(info.file, info.fileList)
                }
                if (status === 'done') {
                    message.success(
                        `${info.file.name} file uploaded successfully.`
                    )
                } else if (status === 'error') {
                    message.error(`${info.file.name} file upload failed.`)
                }
            },
        }),
        [props.match.params.id, project]
    )

    return (
        <>
            <Typography.Title>Dodaj zdjęcia do projektu</Typography.Title>
            {project && (
                <>
                    <Descriptions
                        layout="vertical"
                        column={1}
                        bordered={true}
                        style={{
                            width: 500,
                            marginBottom: 50,
                        }}
                        size="small"
                    >
                        <Descriptions.Item label={<h3>"Dzielnica"</h3>}>
                            {project.district}
                        </Descriptions.Item>
                        <Descriptions.Item label={<h3>Nazwa</h3>}>
                            {project.name}
                        </Descriptions.Item>
                        <Descriptions.Item label={<h3>Opis</h3>}>
                            {project.description}
                        </Descriptions.Item>
                        <Descriptions.Item label={<h3>Kwota</h3>}>
                            {project.pricePln} zł
                        </Descriptions.Item>
                    </Descriptions>
                    <Dragger {...props2}>
                        <p className="ant-upload-drag-icon">
                            <InboxOutlined />
                        </p>
                        <p className="ant-upload-text">
                            Click or drag file to this area to upload
                        </p>
                        <p className="ant-upload-hint">
                            Support for a single or bulk upload. Strictly
                            prohibit from uploading company data or other band
                            files
                        </p>
                    </Dragger>
                </>
            )}
        </>
    )
}

export default withRouter(AddPhotosToProject)
