import React, { FunctionComponent, ReactNode, useEffect, useState } from 'react'
import { withRouter, Link } from 'react-router-dom'
import { Table, Button } from 'antd'
import { columns } from './EditionColumns'
import { getCurrentEdition } from '../Endpoints/Endpoints'
import { getDateString } from '../core/DateHelpers'
import 'antd/dist/antd.css'
import { getEditionURL } from '../Routing/routePaths'
import { getYear } from 'date-fns'

interface CurrentEditionColumnData {
    key: number
    name: string
    endDate: string
    browseButton: ReactNode
}

export const EditionsListPage: FunctionComponent = () => {
    const [currentEdition, setCurrentEdition] = useState<
        CurrentEditionColumnData | undefined
    >()

    useEffect(() => {
        ;(async () => {
            try {
                const { id, district, endDate } = await getCurrentEdition()
                setCurrentEdition({
                    key: id,
                    name: `${district} ${getYear(endDate)}`,
                    endDate: getDateString(endDate),
                    browseButton: (
                        <Link to={getEditionURL('current')}>
                            <Button type="primary"> Przeglądaj </Button>
                        </Link>
                    ),
                })
                console.log('fetched edition')
            } catch (err) {
                console.log(err)
            }
        })()
    }, [])

    return (
        <>
            <h1>Aktualna edycja</h1>
            <Table
                dataSource={currentEdition ? [currentEdition] : []}
                columns={columns}
                size="middle"
                pagination={false}
                locale={{
                    emptyText: 'Aktualnie żadna edycja nie jest w toku',
                }}
            />
        </>
    )
}

export default withRouter(EditionsListPage)
