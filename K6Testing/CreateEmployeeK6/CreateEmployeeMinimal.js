// CreateEmployeeEndpoint.js
import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };
export default function () {
    const url = 'http://localhost:12086/api/minimalAPI/CreateEmployee'; 
    const payload = JSON.stringify({
        firstName: 'John',
        lastName: 'Doe',
        age: 30
    });

    const headers = { 'Content-Type': 'application/json' };

    const response = http.post(url, payload, { headers });
    check(response, {
        'is status 200': (r) => r.status === 200,
        'response time < 500ms': (r) => r.timings.duration < 500,
        'response contains new id': (r) => !isNaN(JSON.parse(r.body).id),
    });

    sleep(1);
}