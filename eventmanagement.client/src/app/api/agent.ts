import axios, { AxiosResponse } from 'axios';
import { IActivitiesEnvelope, IActivity } from '../models/activity';
import { toast } from 'react-toastify';
import { history } from '../..';
import { IUser, IUserFormValues } from '../models/user';
import { request } from 'https';
import { IPhoto, IProfile } from '../models/profile';

axios.defaults.baseURL = 'https://localhost:44361/api';

axios.interceptors.request.use((config) => {
    const token = window.localStorage.getItem('jwt');
    if (token) config.headers!.Authorization = `Bearer ${token}`
    return config;
}, error => {
        return Promise.reject(error);
})

axios.interceptors.response.use(undefined, error => {
    if (error.message === 'Network Error' && !error.response) {
        toast.error('Network error - make sure API is running!');
    }
    const { status, data, config } = error.response;

    if (status === 404) {
        history.push('/notfound')
    }

    if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')) {
        history.push('/notfound');
    }

    if (status === 500) {
        toast.error('Server error - check the terminal for more info!');
    }
    throw error.response;
})

const responseBody = (response: AxiosResponse) => response.data;

const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(response), ms));

const requests = {
    get: (url: string) => axios.get(url).then(sleep(1000)).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(sleep(1000)).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(sleep(1000)).then(responseBody),
    del: (url: string) => axios.delete(url).then(sleep(1000)).then(responseBody),
    postForm: (url: string, file: Blob) => {
        let formData = new FormData();
        formData.append('File', file);
        return axios.post(url, formData, {
            headers: { 'Content-type': 'multipart/form-data' }
        }).then(responseBody)
    }
};

const Activities = {
    list: (params: URLSearchParams): Promise<IActivitiesEnvelope> =>
        axios.get('/activities', { params: params }).then(sleep(1000)).then(responseBody),
    details: (id: string) => requests.get(`/activities/${id}`),
    create: (activity: IActivity) => requests.post('/activities', activity),
    update: (activity: IActivity) => requests.put(`/activities/${activity.id}`, activity),
    delete: (id: string) => requests.del(`/activities/${id}`),
    attend: (id: string) => requests.post(`/activities/${id}/attend`, {}),
    unattend: (id: string) => requests.del(`/activities/${id}/attend`)
}

const User = {
    current: (): Promise<IUser> => requests.get('/user'),
    login: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/login`, user),
    register: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/register`, user),
    fbLogin: (accessToken: string) => requests.post(`/user/facebook`, { accessToken }),
}

const Profiles = {
    get: (username: string): Promise<IProfile> => requests.get(`/profiles/${username}`),
    uploadPhoto: (photo: Blob): Promise<IPhoto> => requests.postForm(`/photos`, photo),
    setMainPhoto: (id: string) => requests.post(`/photos/${id}/setMain`, {}),
    deletePhoto: (id: string) => requests.del(`/photos/${id}`),
    updateProfile: (profile: Partial<IProfile>) => requests.put(`/profiles`, profile),
    follow: (username: string) => requests.post(`/profiles/${username}/follow`, {}),
    unfollow: (username: string) => requests.del(`/profiles/${username}/follow`),
    listFollowings: (username: string, predicate: string) =>
        requests.get(`/profiles/${username}/follow?predicate=${predicate}`),
    listActivities: (username: string, predicate: string) =>
        requests.get(`/profiles/${username}/activities?predicate=${predicate}`)
}

export default {
    Activities,
    User,
    Profiles
}