import React from 'react';
import { Nav, Button, Form } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { logout } from '../app/authenticationSlice';
import { GetTransactions } from '../services/transactions';
import { setCurrency } from '../app/currencySlice'; // Add this line

const Navbar = () => {
  const { isLoggedIn } = useSelector((state) => state.authenticationSlice);
  const dispatch = useDispatch();
  const selectedCurrency = useSelector((state) => state.currency);

  const handleCurrencyChange = (e) => {
    const newCurrency = e.target.value;
    localStorage.setItem('currency', newCurrency);
    dispatch(setCurrency(newCurrency));

    // Fetch transactions when the currency changes
    GetTransactions(dispatch, { pageNumber: 1, pageSize: 10, selectedCurrency: newCurrency });
  };

  const currencies = ['UAH', 'USD', 'EUR', 'GBP', 'JPY'];
  let mainCurrency = localStorage.getItem('currency');
  if (mainCurrency == undefined) {
    mainCurrency = currencies[0];
  }

  return (
    <Nav className="navbar" style={{ backgroundColor: '#e4fff2' }}>
      <h1 style={{ fontFamily: 'Brush Script MT, cursive' }}>My Expenses</h1>
      {isLoggedIn ? (
        <div style={{ display: 'flex', alignItems: 'center' }}>
            
          <NavLink style={{ marginLeft: '1rem' }} variant="link" to="/">
            Home
          </NavLink>
          
          
          <Form.Control as="select" style={{ marginLeft: '1rem' }} value={selectedCurrency} onChange={handleCurrencyChange}>
        {currencies.map((currency) => (
          <option key={currency} value={currency}>
            {currency}
          </option>
        ))}
      </Form.Control>
          
          
          <NavLink style={{ marginLeft: '1rem' }} variant="link" to="/statistics">
              Statistics
            </NavLink>
          <Button
            variant="link"
            href="/signin"
            onClick={() => {
              dispatch(logout());
            }}
          >
            Log out
          </Button>
        </div>
      ) : (
        <div style={{ display: 'flex' }}>
          <NavLink to="/signup">Sign up</NavLink>
          <NavLink to="/signin" style={{ marginLeft: '1rem' }}>
            Sign in
          </NavLink>
        </div>
      )}
    </Nav>
  );
};

export default Navbar;