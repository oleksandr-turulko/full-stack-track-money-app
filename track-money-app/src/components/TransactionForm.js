import { React, useState, useEffect } from 'react';
import { Form, Row, Col, Button } from "react-bootstrap";
import { EditTransaction, NewTransaction, DeleteTransaction } from '../services/transactions';
import { useDispatch } from 'react-redux';

const TransactionForm = ({ transaction, setIsEditing }) => {
    const transactionTypes = ['Expence', 'Inclome'];
    const [amount, setAmount] = useState(0);
    const [description, setDescription] = useState('');
    const [transactionType, setTransactionType] = useState(transactionTypes[0]);
    const [isNewTransaction, setIsNewTransaction] = useState(true);
    const dispatch = useDispatch();

    useEffect(() => {
        if (transaction !== undefined) {
            setIsNewTransaction(false);
            setAmount(transaction.amount);
        }
        else {
            setIsNewTransaction(true);
        }
    }, [transaction]);

    return <Form
        onSubmit={event => {
            event.preventDefault();
            if (isNewTransaction) {
                NewTransaction(dispatch, { description: description, amount: Number(amount) });
            }
            else {
                EditTransaction(dispatch, { id: transaction.id, description: description, amount: Number(amount) });
                setIsEditing(false);
            }
        }}
    >
        <Row>
            <Col>
                <Form.Label>Transaction type</Form.Label>
                <Form.Control as='select'
                    onChange={event => setTransactionType(event.target.value)}>
                    {transactionTypes.map((t, idx) =>  <option key={idx}>{t}</option> )}
                </Form.Control>
            </Col>
            <Col>
                <Form.Label>Description</Form.Label>
                <Form.Control onChange={event => setDescription(event.target.value)}/>
            </Col>
            <Col>
                <Form.Label>Amount</Form.Label>
                <Form.Control type='number' step='0.01'
                    placeholder={amount}
                    onChange={event => setAmount(event.target.value)} />
            </Col>
            <Col>
                <Form.Label>Transaction</Form.Label>
                <Form.Control type='number' step='0.01'
                    placeholder={amount}
                    onChange={event => setAmount(event.target.value)} />
            </Col>
            <div style={{ marginTop: 'auto' }}>
                {isNewTransaction
                    ? <Button variant='primary' type='submit'>Add</Button>
                    : <div>
                        <Button style={{ marginRight: '2px' }} variant='danger'
                            onClick={() => DeleteTransaction(dispatch, transaction)}>Delete</Button>
                        <Button style={{ marginRight: '2px' }} variant='success' type='submit'>Save</Button>
                        <Button style={{ marginRight: '2px' }} variant='default' onClick={() => setIsEditing(false)}>Cancel</Button>
                    </div>}
            </div>
        </Row>
    </Form>
}

export default TransactionForm;