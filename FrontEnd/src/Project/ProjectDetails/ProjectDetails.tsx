import Descriptions from 'antd/lib/descriptions'
import React, { FunctionComponent, useEffect, useState } from 'react'
import { IProjectData } from '../IProjectData'
import { useAuthContext } from '../../Context/Context'
import { getImages } from '../../Endpoints/Endpoints'
import { Carousel } from 'antd'
export interface IProjectDetailsProps {
    project: IProjectData
}

export const ProjectDetails: FunctionComponent<IProjectDetailsProps> = ({
    project,
}) => {
    const [images, setImages] = useState([])

    useEffect(() => {
        const fetchImages = async () => await getImages(project.id)
        fetchImages().then((response) => setImages(response.links))
    }, [])

    return (
        <div
            style={{
                display: 'flex',
            }}
        >
            <Descriptions
                layout="vertical"
                column={1}
                bordered={true}
                style={{
                    width: 500,
                }}
            >
                <Descriptions.Item label={<h3>Dzielnica</h3>}>
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
                <Descriptions.Item
                    style={{ display: 'flex', flexDirection: 'column' }}
                    label={<h3>Zdjęcia</h3>}
                >
                    {images &&
                        images.map((image) => (
                            <img
                                src={`https://localhost:44385/${image}`}
                                alt={'nie załadowano zdjęcia'}
                                style={{
                                    height: '200px',
                                    width: '300px',
                                    margin: '10px 0px',
                                }}
                            />
                        ))}
                </Descriptions.Item>
            </Descriptions>
        </div>
    )
}

export default ProjectDetails
