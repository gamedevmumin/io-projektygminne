export const routePaths = {
    HOME: '/',
    RESULTS: '/results',
    EDITION: '/edition/:id',
    PROJECT: '/project/:id',
    ADMIN_PANEL: '/admin_panel',
    ADD_PROJECT: '/add_project',
    ADD_PHOTOS: '/add_photos/:id',
    ADD_USER: '/add_user',
    ADD_EDITION: '/add_edition',
    EDITIONS_ADMIN: '/editions_admin',
    NEWSLETTER: '/newsletter'
}

export const getEditionURL = (id: string) => {
    return routePaths.EDITION.replace(':id', id.toString())
}

export const getProjectURL = (id: number) => {
    return routePaths.PROJECT.replace(':id', id.toString())
}
export const getAddPhotosURL = (id: number) => {
    return routePaths.ADD_PHOTOS.replace(':id', id.toString())
}


