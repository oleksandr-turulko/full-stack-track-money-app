import { createSlice } from '@reduxjs/toolkit';

export const authenticationSlice = createSlice({
    name: 'authentication',
    initialState: {
        token: '',
        isLoggedIn: false,
    },
    reducers: {
        userAuthenticated: (state, action) => {
            const storedValue = localStorage.getItem('token');
            let token ='';
            if (storedValue !== null) {
            // Use the value
                token = storedValue;
            } else {
            // Handle the case where the item is not present
                localStorage.setItem('token', action.payload.jwt);
                token = localStorage.getItem('token');
            }
            
            return {
                ...state, ...{
                    token: token,
                    isLoggedIn: true,
                }
            }
        },
        logout: () => {
            localStorage.clear();
        }
    }
});

export const { userAuthenticated, logout } = authenticationSlice.actions;

export default authenticationSlice.reducer;