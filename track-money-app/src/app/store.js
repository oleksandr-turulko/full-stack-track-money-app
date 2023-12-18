import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import authenticationSlice from './authenticationSlice';
import transactionSlice from './transactionSlice';
import currencyReducer from './currencySlice';
import ToastMiddleware from '../middlewares/ToastMiddleware';

export default configureStore({
  reducer: {
    authenticationSlice: authenticationSlice,
    transactionSlice: transactionSlice,
    currency: currencyReducer, 
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(ToastMiddleware)
});
