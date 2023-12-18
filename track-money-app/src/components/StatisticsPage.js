import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';

const StatisticsPage = () => {

    


  const [data, setData] = useState({
    labels: [],
    datasets: [
      {
        label: 'Transaction Amounts',
        data: [],
        backgroundColor: [
          '#007bff', // blue
          '#FF0000', // red
          '#FFD700', // yellow
          '#28a745', // green
          '#FF00FF', // violet
          '#ff9900', // orange
          '#00FFFF', // aqua marine
          '#d69ae5', // red violet
          '#FF8F66', // orange red
          '#00FF00', // lime
        ],
      },
    ],
  });

  useEffect(() => {
    // Mock data for testing
    const result = [
      { Key: 'Category 1', Value: 100 },
      { Key: 'Category 2', Value: 200 },
      { Key: 'Category 3', Value: 150 },
    ];

    setData({
      labels: result.map(item => item.Key),
      datasets: [
        {
          label: 'Transaction Amounts',
          data: result.map(item => item.Value),
          backgroundColor: data.datasets[0].backgroundColor,
        },
      ],
    });
  }, []); // Make an API call here instead of using mock data

  return (
    <div style={{ maxWidth: '35rem', maxHeight: '35rem', margin: 'auto', textAlign: 'center' }}>
      <h4 style={{ marginTop: '10px' }}>Transaction Amounts per Category</h4>
      <Bar data={data} />
    </div>
  );
};

export default StatisticsPage;
