import {
    setTransactions, newTransaction, editTransaction, deleteTransaction,
    setTransactionsError, editTransactionError, newTransactionError, deleteTransactionError
} from '../app/transactionSlice';
import axios from 'axios';

const axiosInstance = axios.create({    
    baseURL: `${process.env.REACT_APP_BASE_URL}/Transactions`,
})

axiosInstance.interceptors.request.use((config) => {
    config.headers = { authorization: 'Bearer ' + localStorage.getItem('token') };
    return config;
});

export const GetTransactions = async (dispatch, params) => {
    try {
        // api call
        const { data } = await axiosInstance.get( `?pageNumber=${params.pageNumber}&pageSize=${params.pageSize}&selectedCurrency=${params.selectedCurrency}`);
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
        await axiosInstance.patch('', transaction);
        dispatch(editTransaction(transaction));
    } catch {
        dispatch(editTransactionError());
    }
}

export const DeleteTransaction = async (dispatch, transaction) => {
    try {
        // api call
        await axiosInstance.delete(`/${transaction.id}`);

        dispatch(deleteTransaction(transaction));
    } catch {
        dispatch(deleteTransactionError());
    }
}