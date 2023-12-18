import { userAuthenticated } from '../app/authenticationSlice';
import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: `${process.env.REACT_APP_BASE_URL}/Statistics`,
});

axiosInstance.interceptors.request.use((config) => {
    config.headers = { authorization: 'Bearer ' + localStorage.getItem('token') };
    return config;
});

export const GetStatsByDate = async (dispatch, params) => {
    try {
        // api call
        const { data } = await axiosInstance.get( `${params}`);
        dispatch(setTransactions(data));
    } catch {
        dispatch(setTransactionsError());
    }
}