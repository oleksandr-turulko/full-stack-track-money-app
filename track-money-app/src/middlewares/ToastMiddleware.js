import { newTransaction, editTransaction, deleteTransaction, 
    setTransactionsError, newTransactionError, editTransactionError, deleteTransactionError } from '../app/transactionSlice';
import { toast } from 'react-toastify';

const ToastMiddleware = () => next => action => {
    switch(action.type) {
        case newTransaction.type:
            toast.success('New transaction added successfully');
            break;
        case editTransaction.type:
            toast.success('Transaction edited successfully');
            break;
        case deleteTransaction.type:
            toast.success('Transaction deleted successfully');
            break;
        case setTransactionsError.type:
            toast.error('Error loading transactions');
            break;
        case newTransactionError.type:
            toast.error('Error adding new transaction');
            break;
        case editTransactionError.type:
            toast.error('Error editing transaction');
            break;
        case deleteTransactionError.type:
            toast.error('Error deleting transaction');
            break;
        default:
            break;
    }
    return next(action);
}

export default ToastMiddleware;