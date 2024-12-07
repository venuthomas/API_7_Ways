// UpdateEmployeeByIdEndpoint.js
import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };

export default function () {
    const id = 1; 
    const url = `http://localhost:12086/api/minimalAPI/UpdateEmployeeById/${id}`;
    const payload = JSON.stringify({
        id: id, 
        firstName: 'Jane77',
        lastName: 'Doe77',
        age: 35
    });

    const headers = { 'Content-Type': 'application/json' };

    const response = http.put(url, payload, { headers });

    check(response, {
        'is status 200': (r) => r.status === 200,
        'updatedSuccessfully successfully': (r) => !isNaN(JSON.parse(r.body).updatedSuccessfully) === true,
    });

    sleep(1);
}