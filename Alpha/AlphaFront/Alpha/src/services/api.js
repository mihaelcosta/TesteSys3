import axios from 'axios';

const httpClient = axios.create({
  baseURL: 'https://localhost:5080',
  headers: {
    'Content-type': 'application/json',
  },
});

export default httpClient;
