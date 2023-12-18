import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { GetTransactions } from '../services/transactions';
import { Button, Row, Col, Pagination } from 'react-bootstrap';
import TransactionForm from './TransactionForm';

const TransactionList = () => {
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const dispatch = useDispatch();
    const transactions = useSelector((state) => state.transactionSlice.transactions);
    const selectedCurrency = useSelector((state) => state.currency);

    useEffect(() => {
        GetTransactions(dispatch, { pageNumber, pageSize, selectedCurrency });
    }, [pageNumber, pageSize, selectedCurrency]); 


    return (
        <div>
            {transactions.map((e) => (
                <div key={e.id} style={{ marginBottom: '1rem' }}>
                    <ListRow transaction={e} selectedCurrency={selectedCurrency} />
                </div>
            ))}
            <ListControls pageNumber={pageNumber} pageSize={pageSize} setPageNumber={setPageNumber} setPageSize={setPageSize} />
        </div>
    );
};
const ListControls = ({ pageNumber, pageSize, setPageNumber, setPageSize }) => {
    return (
        <Pagination>
            <Pagination.Prev onClick={() => setPageNumber(pageNumber - 1)} disabled={pageNumber === 1} />
            <Pagination.Item>{pageNumber}</Pagination.Item>
            <Pagination.Next onClick={() => setPageNumber(pageNumber + 1)} />
        </Pagination>
    );
};
const ListRow = ({ transaction, selectedCurrency }) => {
    const [isEditing, setIsEditing] = useState(false);

    return isEditing ? (
        <TransactionForm transaction={transaction} setIsEditing={setIsEditing} />
    ) : (
        <div>
            <Row>
                <Col>{transaction.description}</Col>
                <Col>{`${transaction.amount} ${selectedCurrency}`}</Col>
                <Col>{Math.round(transaction.amount * 10) / 10 }</Col>
                <Col>{transaction.transactionType}</Col>
                <Col>{new Date(transaction.date).toLocaleDateString()}</Col>
                <Col>{transaction.updatedAt ? new Date(transaction.updatedAt).toLocaleDateString() : '-'}</Col>
                <Button variant="warning" onClick={() => setIsEditing(!isEditing)}>
                    Edit
                </Button>
            </Row>
            <hr />
        </div>
    );
};

export default TransactionList;
