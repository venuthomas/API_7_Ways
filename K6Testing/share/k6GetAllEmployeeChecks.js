import { check } from 'k6';

export const runChecks = (response) => {
    check(response, {
        'is status 200': (r) => r.status === 200,
        'response time < 75ms': (r) => r.timings.duration < 75,
        'content type is JSON': (r) => r.headers['Content-Type'].includes('application/json'),
        'response body contains employees': (r) => JSON.parse(r.body).length > 0,
        'has expected fields(id, firstName, lastName & age)': (r) => {
            const employees = JSON.parse(r.body);
            return employees.every(e => e.hasOwnProperty('id')
                && e.hasOwnProperty('firstName')
                && e.hasOwnProperty('lastName')
                && e.hasOwnProperty('age'));
        },
    });
}