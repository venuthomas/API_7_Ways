import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };

const mutation = `
  mutation UpdateEmployee($input: UpdateEmployeeCommandInput!) {
    updateEmployee(input: $input) {
      updatedSuccessfully
    }
  }
`;

export default function () {
    const url = 'http://localhost:12085/api/graphql';
    const variables = {
        input: {
            id: 1,
            firstName: "Jane",
            lastName: "Doe",
            age: 34,
        }
    };

    const headers = { 'Content-Type': 'application/json' };

    const payload = JSON.stringify({
        query: mutation,
        variables: variables
    });

    const response = http.post(url, payload, { headers });
console.log(`Response: ${response.body}`);
    check(response, {
        'is status 200': (r) => r.status === 200,
        'deleted successfully': (r) => {
            const body = JSON.parse(r.body);
            return body.data && body.data.updateEmployee && body.data.updateEmployee.updatedSuccessfully === true;
        },
    });

    sleep(1);
}