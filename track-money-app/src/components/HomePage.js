
import { ToastContainer } from 'react-toastify';
import TransactionForm from './TransactionForm';
import TransactionList from './TransactionList';
const HomePage = () => (
    <div style={{ width: '80%', margin: 'auto' }}>
        <ToastContainer />
        <h4>New Expense</h4>
        <TransactionForm />
        <hr style={{ border: '1px solid grey' }} />
        <h4>Your Expenses</h4>
        <TransactionList />
    </div>
);

export default HomePage;