// GetEmployeeByIdCacheEndpoint.js
import http from 'k6/http';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';
import { runChecks } from '../share/k6GetEmployeeByIdChecks.js';


export { options };

export default function () {
    const id = 1; // Example ID, replace with a valid ID
    const url = `http://localhost:12082/api/cartelAPI/GetEmployeeById/${id}`;

    const response = http.get(url);
    
    runChecks(response);

    sleep(1);
}