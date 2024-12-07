import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';


export { options };

export default function () {
    const url = 'http://localhost:12085/api/graphql'; 
    const query = JSON.stringify({
        query: `
            query {
                getAllEmployees {
                    id
                    firstName
                    lastName
                    age
                }
            }
        `
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = http.post(url, query, params);

    if (response.body.trim().length === 0) {
        console.error("Error: Empty response body");
    }

    check(response, {
        'is status 200': (r) => r.status === 200,
        'response time < 75ms': (r) => r.timings.duration < 75,
        'content type is GraphQL JSON': (r) => r.headers['Content-Type'] && r.headers['Content-Type'].includes('application/graphql-response+json'),
        'response body contains employees': (r) => {
            if (r.body.trim().length === 0) {
                return false;
            }
            const data = JSON.parse(r.body);
            return data.data && data.data.getAllEmployees && data.data.getAllEmployees.length > 0;
        },
        'has expected fields(id, firstName, lastName, age)': (r) => {
            if (r.body.trim().length === 0) {
                return false;
            }
            const data = JSON.parse(r.body);
            return data.data.getAllEmployees.every(e => e.hasOwnProperty('id')
                && e.hasOwnProperty('firstName')
                && e.hasOwnProperty('lastName')
                && e.hasOwnProperty('age'));
        },
    });

    sleep(1); 
}
