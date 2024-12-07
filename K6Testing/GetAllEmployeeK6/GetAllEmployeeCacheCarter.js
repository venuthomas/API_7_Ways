import http from 'k6/http';
import { check, sleep } from 'k6';

import { options } from '../share/k6Options.js';
import { runChecks } from '../share/k6GetAllEmployeeChecks.js';


export { options };
export default function () {
    const url = 'http://localhost:12082/api/CacheCartelAPI/GetAllEmployees';
    const response = http.get(url);
    
    runChecks(response);

    sleep(1);
}