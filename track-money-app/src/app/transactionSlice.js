import { createSlice, createAction } from '@reduxjs/toolkit';

export const setTransactionsError = createAction('setTransactionsError');
export const newTransactionError = createAction('newTransactionError');
export const editTransactionError = createAction('editTransactionError');
export const deleteTransactionError = createAction('deleteTransactionError');

export const transactionsSlice = createSlice({
    name: 'transactions',
    initialState: {
        transactions: [],
    },
    reducers: {
        setTransactions: (state, action) => {
            return { ...state, transactions: [...action.payload] };
        },
        newTransaction: (state, action) => {
            return { ...state, transactions: [action.payload, ...state.transactions] }
        },
        editTransaction: (state, action) => {
            const transactions = state.transactions.map(transaction => {
                if (transaction.id === action.payload.id) {
                    transaction = action.payload;
                }
                return transaction;
            });
            return { ...state, transactions: [...transactions] };
        },
        deleteTransaction: (state, action) => {
            const transactions = state.transactions.filter(transaction =>
                transaction.id !== action.payload.id);
            return { ...state, transactions: [...transactions] };
        }
    }
});

export const { setTransactions, newTransaction, editTransaction, deleteTransaction } = transactionsSlice.actions;

export default transactionsSlice.reducer;