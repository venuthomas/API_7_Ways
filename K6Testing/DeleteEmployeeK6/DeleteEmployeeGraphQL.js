import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };


export default function() {
    const url = 'http://localhost:12085/api/graphql';
    const mutation = `
        mutation DeleteEmployee($id: Int!) {
            deleteEmployee(id: $id) {
                deletedSuccessfully
            }
        }
    `;

    const variables = {
        id: Math.floor(Math.random() * 250) + 1, // Use a valid employee ID
    };

    const headers = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
    };

    const payload = JSON.stringify({
        query: mutation,
        variables: variables,
    });

    const response = http.post(url, payload, { headers });

    console.log(`Response: ${response.body}`);

    check(response, {
        'is status 200': (r) => r.status === 200,
        'deleted successfully': (r) => {
            const body = JSON.parse(r.body);
            return body.data && body.data.deleteEmployee && body.data.deleteEmployee.deletedSuccessfully === true;
        },
    });

    sleep(1);
};