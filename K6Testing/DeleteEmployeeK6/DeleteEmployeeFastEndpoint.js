// DeleteEmployeeEndpoint.js
import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };

export default function () {
    const id = Math.floor(Math.random() * 250) + 1;
    const url = `http://localhost:12084/api/fastEndpointAPI/DeleteEmployeeById/${id}`;

    const response = http.del(url);
    check(response, {
        'is status 200': (r) => r.status === 200,
        'deleted successfully': (r) => !isNaN(JSON.parse(r.body).deletedSuccessfully) === true,
    });

    sleep(1);
}