import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };

export default function () {
    const id = 1; // Example ID, replace with a valid ID or loop through different IDs
    const url = 'http://localhost:12085/api/graphql';

    // Define the GraphQL query
    const query = JSON.stringify({
        query: `
            query GetEmployeeById($id: Int!) {
                employeeById(id: $id) {
                    id
                    firstName
                    lastName
                    age
                }
            }
        `,
        variables: {
            id: id, // Pass the dynamic ID here
        },
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = http.post(url, query, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
        'response time <= 200ms': (r) => r.timings.duration <= 200,
        'response body is not null': (r) => r.body !== null,
        'response contains id': (r) => {
            const data = JSON.parse(r.body);
            return data.data && data.data.employeeById && data.data.employeeById.id !== undefined;
        },
        'response contains firstName': (r) => {
            const data = JSON.parse(r.body);
            return data.data && data.data.employeeById && data.data.employeeById.firstName !== undefined;
        },
        'response contains lastName': (r) => {
            const data = JSON.parse(r.body);
            return data.data && data.data.employeeById && data.data.employeeById.lastName !== undefined;
        },
        'response contains age': (r) => {
            const data = JSON.parse(r.body);
            return data.data && data.data.employeeById && data.data.employeeById.age !== undefined;
        },
    });

    sleep(1);
}
