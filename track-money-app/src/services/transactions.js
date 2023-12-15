import {
    setTransactions, newTransaction, editTransaction, deleteTransaction,
    setTransactionsError, editTransactionError, newTransactionError, deleteTransactionError
} from '../app/transactionSlice';
import * as axios from 'axios';

const axiosInstance = axios.create({    
    baseURL: `${process.env.REACT_APP_BASE_URL}/transactions`,
})

axiosInstance.interceptors.request.use((config) => {
    config.headers = { authorization: 'Bearer ' + sessionStorage.getItem('token') };
    return config;
});

export const GetTransactions = async (dispatch) => {
    try {
        // api call
        const { data } = await axiosInstance.get();
        dispatch(setTransactions(data));
    } catch {
        dispatch(setTransactionsError());
    }
}

export const NewTransaction = async (dispatch, transaction) => {
    try {
        // api call
        const { data } = await axiosInstance.post('', transaction);
        dispatch(newTransaction(data));
    } catch {
        dispatch(newTransactionError());
    }
}

export const EditTransaction = async (dispatch, transaction) => {
    try {
        // api call
        await axiosInstance.put('', transaction);
        dispatch(editTransaction(transaction));
    } catch {
        dispatch(editTransactionError());
    }
}

export const DeleteTransaction = async (dispatch, transaction) => {
    try {
        // api call
        await axiosInstance.delete('', { data: { ...transaction } });
        dispatch(deleteTransaction(transaction));
    } catch {
        dispatch(deleteTransactionError());
    }
}