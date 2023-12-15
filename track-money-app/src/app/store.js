import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import authenticationSlice from './authenticationSlice';
import ToastMiddleware from '../middlewares/ToastMiddleware';

export default configureStore({
  reducer: {
    authenticationSlice: authenticationSlice
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(ToastMiddleware)
});
