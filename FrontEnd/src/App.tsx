import React, { useState } from 'react'
import './App.css'
import { Link, Route, Switch, BrowserRouter as Router } from 'react-router-dom'
import { Results } from './Results/Results'
import { routePaths, getEditionURL } from './Routing/routePaths'
import EditionMainPage from './Edition/EditionMainPage'
import EditionsListPage from './EditionsList/EditionsListPage'
import { Layout, Menu } from 'antd'
import 'antd/dist/antd.css'
import { AdminPanel } from './AdminPanel/AdminPanel'
import ProjectMainPage from './Project/ProjectMainPage'
import { ContentWrapper } from './core/components/ContentWrapper'
import AddProjectPage from './AdminPanel/AddProject/AddProjectPage'
import { Context } from './Context/Context'
import AuthedWrapper from './AuthedWrapper/AuthedWrapper'
import { AddUser } from './AdminPanel/AddUser'
import { AddEdition } from './AdminPanel/AddEdition'
import { Editions } from './AdminPanel/Editions'
import AddPhotosToProject from './AdminPanel/AddProject/AddPhotos/AddPhotosToProject'

const { Header, Content } = Layout

function App() {
    const [token, setToken] = useState<string>()
    const [role, setRole] = useState<string>()

    return (
        <Context.Provider value={[token, setToken, role, setRole]}>
            <Router>
                <Layout className="layout App">
                    <Header>
                        <Menu
                            theme="dark"
                            mode="horizontal"
                            defaultSelectedKeys={['1']}
                        >
                            <Menu.Item key="1">
                                <Link to={routePaths.HOME}>Strona główna</Link>
                            </Menu.Item>
                            <Menu.Item key="2">
                                <Link to={routePaths.RESULTS}>Wyniki</Link>
                            </Menu.Item>
                            <Menu.Item key="3">
                                <Link to={getEditionURL('current')}>
                                    Aktualna edycja
                                </Link>
                            </Menu.Item>
                            <Menu.Item key="4">
                                <Link to={routePaths.ADMIN_PANEL}>
                                    {token ? 'Panel admina' : 'Zaloguj'}
                                </Link>
                            </Menu.Item>
                            {token && (
                                <Menu.Item key="5">
                                    <Link
                                        to={routePaths.HOME}
                                        onClick={() => setToken(undefined)}
                                    >
                                        Wyloguj
                                    </Link>
                                </Menu.Item>
                            )}
                        </Menu>
                    </Header>
                    <ContentWrapper>
                        <Content>
                            <Switch>
                                <Route path={routePaths.ADD_PROJECT}>
                                    <AuthedWrapper>
                                        <AddProjectPage />
                                    </AuthedWrapper>
                                </Route>
                                <Route path={routePaths.ADD_PHOTOS}>
                                    <AuthedWrapper>
                                        <AddPhotosToProject />
                                    </AuthedWrapper>
                                </Route>
                                <Route path={routePaths.RESULTS}>
                                    <Results />
                                </Route>
                                <Route path={routePaths.EDITION}>
                                    <EditionMainPage />
                                </Route>
                                <Route exact path={routePaths.HOME}>
                                    <EditionsListPage />
                                </Route>
                                <Route path={routePaths.ADMIN_PANEL}>
                                    <AdminPanel />
                                </Route>
                                <Route path={routePaths.PROJECT}>
                                    <ProjectMainPage />
                                </Route>
                                <Route path={routePaths.ADD_USER}>
                                    <AuthedWrapper>
                                        <AddUser />
                                    </AuthedWrapper>
                                </Route>
                                <Route path={routePaths.ADD_EDITION}>
                                    <AuthedWrapper>
                                        <AddEdition />
                                    </AuthedWrapper>
                                </Route>
                                <Route path={routePaths.EDITIONS_ADMIN}>
                                    <AuthedWrapper>
                                        <Editions />
                                    </AuthedWrapper>
                                </Route>
                            </Switch>
                        </Content>
                    </ContentWrapper>
                </Layout>
            </Router>
        </Context.Provider>
    )
}

export default App
