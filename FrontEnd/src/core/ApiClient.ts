const get = async (url: string, headers?: object) => {
    return await (await fetch(url)).json()
}

const post = async (url: string, body: object, headers?: object) => {
    return await (
        await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                ...headers,
            },
            body: JSON.stringify(body),
        })
    ).json()
}

const put = async (
    url: string,
    body: object,
    headers?: object,
    shouldStringify?: boolean
) => {
    const promise = await await fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            ...headers,
        },
        body: JSON.stringify(body),
    })
    return shouldStringify ? promise.json() : promise.text()
}

const remove = async (
    url: string,
    body?: object,
    headers?: object,
    shouldStringify?: boolean
) => {
    const promise = await await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            ...headers,
        },
        body: JSON.stringify(body),
    })
    return shouldStringify ? promise.json() : promise.text()
}

export const httpClient = {
    get,
    post,
    put,
    remove,
}
