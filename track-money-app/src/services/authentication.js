import { userAuthenticated } from '../app/authenticationSlice';
import * as axios from 'axios';

const axiosInstance = axios.create({
    baseURL: `${process.env.REACT_APP_BASE_URL}/Authentication`,
});

export const SignUp = async (dispatch, credentials) => {
    try {
        // api call
        const { data } = await axiosInstance.post('/sign-up', credentials);
        dispatch(userAuthenticated(data));
    } catch {
        console.log('Error!');
    }
}

export const SignIn = async (dispatch, credentials) => {
    try {
        // api call
        const { data } = await axiosInstance.post('/sign-in', credentials);
        dispatch(userAuthenticated(data));
    } catch {
        console.log('Error!');
    }
}

export const ThirdPartySignIn = async (dispatch, token) => {
    try {
        // api call        
        const { data } = await axiosInstance.post(`/google-auth?token=${token}`);
        dispatch(userAuthenticated(data));
    } catch {
        console.log('Error!')
    }
}