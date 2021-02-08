import { httpClient } from '../core/ApiClient'

const apiUrl = 'https://localhost:44385'

export interface ProjectData {
    id: number
    name: string
    description: string
}

export interface PlaceVoteBody {
    giveVote: boolean
    VoterPesel: string
}

export interface CurrentEditionResponseDto {
    district: string
    endDate: Date
    id: number
    participants: CurrentEditionParticipantResponseDto[]
}

export interface CurrentEditionParticipantResponseDto {
    project: CurrentEditionParticipantProjectDataResponseDto
    voteCount: 0
}

export interface CurrentEditionParticipantProjectDataResponseDto {
    accepted: boolean
    description: string
    district: string
    estimatedTimeInDays: number
    id: number
    name: string
    pricePln: number
}

export const getCurrentEdition: () => Promise<
    CurrentEditionResponseDto
> = async () => {
    const result = await httpClient.get(`${apiUrl}/editions/current/`)
    return { ...result, endDate: new Date(result.endDate) }
}
export const getConcludedEditions: () => Promise<
    CurrentEditionResponseDto[]
> = async () => {
    const results: CurrentEditionResponseDto[] = await httpClient.get(
        `${apiUrl}/editions/concluded`
    )
    return results.map((result) => ({
        ...result,
        endDate: new Date(result.endDate),
    }))
}

export const getCurrentEditionProjects = (): Promise<
    CurrentEditionResponseDto
> => httpClient.get(`${apiUrl}/editions/current/`)

export const getAcceptedProjects = () =>
    httpClient.get(`${apiUrl}/projects/accepted`)

export const placeVote = (
    id: string,
    body: PlaceVoteBody,
    shouldStringify: boolean
) =>
    httpClient.put(
        `${apiUrl}/editions/current/projects/${id}/votes`,
        body,
        {},
        shouldStringify
    )

export const subscribeToNewsletter = (
    email: string,
    shouldStringify: boolean
) => {
    httpClient.put(`${apiUrl}/subscribers`, { email }, {}, shouldStringify)
}

export const getDistricts = () => httpClient.get(`${apiUrl}/districts`)

export const postEdition = (
    description: string,
    endDate: Date,
    districtId: number,
    projectIds: number[],
    token: string | undefined
) =>
    httpClient.post(
        `${apiUrl}/editions/drafts`,
        { description, endDate, districtId, projectIds },
        {
            Authorization: `Bearer ${token}`,
        }
    )

export const getProject = (id: string | undefined) =>
    httpClient.get(`${apiUrl}/projects/${id}`)

export const getImages = (id: number | undefined) =>
    httpClient.get(`${apiUrl}/projects/${id}/images`)

export const getProjects = () => httpClient.get(`${apiUrl}/projects`)

export const getPendingProjects = () =>
    httpClient.get(`${apiUrl}/projects/pending`)

export const startEdition = (id: number, token: string | undefined) =>
    httpClient.put(
        `${apiUrl}/editions/current`,
        { draftId: id },
        {
            Authorization: `Bearer ${token}`,
        }
    )

export const getEditionDrafts = () =>
    httpClient.get(`${apiUrl}/editions/drafts`)

export const removeEditionDraft = (id: number, token: string | undefined) =>
    httpClient.remove(
        `${apiUrl}/editions/drafts/${id}`,
        {},
        {
            Authorization: `Bearer ${token}`,
        }
    )

export const addUser = (
    username: string,
    password: string,
    roleId: string,
    token: string | undefined
) =>
    httpClient.post(
        `${apiUrl}/users`,
        { username, password, roleId },
        {
            Authorization: `Bearer ${token}`,
        }
    )
