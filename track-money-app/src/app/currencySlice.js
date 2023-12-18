// currencySlice.js

import { createSlice } from '@reduxjs/toolkit';

const currencySlice = createSlice({
  name: 'currency',
  initialState: 'UAH', // Default currency
  reducers: {
    setCurrency: (state, action) => action.payload,
  },
});

export const { setCurrency } = currencySlice.actions;
export default currencySlice.reducer;
