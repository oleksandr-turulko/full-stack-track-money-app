import React, { useState, useEffect } from 'react';
import { Form, Row, Col, Button } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { EditTransaction, NewTransaction, DeleteTransaction } from '../services/transactions';

const TransactionForm = ({ transaction, setIsEditing }) => {
    const transactionTypes = ['Expense', 'Income']; // Fixed typo in transaction type
    const [value, setValue] = useState(0);
    const [description, setDescription] = useState('');
    const [currencyCode, setCurrencyCode] = useState(''); // Added currencyCode state
    const [transactionType, setTransactionType] = useState(transactionTypes[0]);
    const [isNewTransaction, setIsNewTransaction] = useState(true);
    const dispatch = useDispatch();

    useEffect(() => {
        if (transaction !== undefined) {
            setIsNewTransaction(false);
            setValue(transaction.value);
            setDescription(transaction.description);
            setCurrencyCode(transaction.currencyCode);
            setTransactionType(transaction.transactionType);
        } else {
            setIsNewTransaction(true);
        }
    }, [transaction]);

    return (
        <Form
            onSubmit={(event) => {
                event.preventDefault();
                const formData = {
                    description,
                    value: Number(value),
                    currencyCode,
                    transactionType,
                };

                if (isNewTransaction) {
                    NewTransaction(dispatch, formData);
                } else {
                    EditTransaction(dispatch, { transactionId: transaction.id, ...formData });
                    setIsEditing(false);
                }
            }}
        >
            <Row>
                <Col>
                    <Form.Label>Transaction type</Form.Label>
                    <Form.Control as="select" onChange={(event) => setTransactionType(event.target.value)}>
                        {transactionTypes.map((t, idx) => (
                            <option key={idx}>{t}</option>
                        ))}
                    </Form.Control>
                </Col>
                <Col>
                    <Form.Label>Description</Form.Label>
                    <Form.Control onChange={(event) => setDescription(event.target.value)} value={description} />
                </Col>
                <Col>
                    <Form.Label>Amount</Form.Label>
                    <Form.Control
                        type="number"
                        step="0.01"
                        placeholder={value}
                        onChange={(event) => setValue(event.target.value)}
                        value={value}
                    />
                </Col>
                <Col>
                    <Form.Label>Currency Code</Form.Label>
                    <Form.Control onChange={(event) => setCurrencyCode(event.target.value)} value={currencyCode} />
                </Col>
            </Row>
            <div style={{ marginTop: 'auto' }}>
                {isNewTransaction ? (
                    <Button variant="primary" type="submit">
                        Add
                    </Button>
                ) : (
                    <div>
                        <Button style={{ marginRight: '2px' }} variant="danger" onClick={() => DeleteTransaction(dispatch, transaction)}>
                            Delete
                        </Button>
                        <Button style={{ marginRight: '2px' }} variant="success" type="submit">
                            Save
                        </Button>
                        <Button style={{ marginRight: '2px' }} variant="default" onClick={() => setIsEditing(false)}>
                            Cancel
                        </Button>
                    </div>
                )}
            </div>
        </Form>
    );
};

export default TransactionForm;
