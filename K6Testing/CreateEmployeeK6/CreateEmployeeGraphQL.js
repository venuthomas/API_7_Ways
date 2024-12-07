import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };
export default function () {
    const url = 'http://localhost:12085/api/graphql';
    const query = `
        mutation {
            createEmployee(input: {
                firstName: "John",
                lastName: "Doe",
                age: 30
            }) {
                id
                firstName
                lastName
                age
            }
        }
    `;

    const payload = JSON.stringify({ query });
    const headers = { 'Content-Type': 'application/json' };

    const response = http.post(url, payload, { headers });

    check(response, {
        'is status 200': (r) => r.status === 200,
        'no GraphQL errors': (r) => !r.json().errors,
        'response contains new id': (r) => !isNaN(r.json().data.createEmployee.id),
    });

    sleep(1);
}