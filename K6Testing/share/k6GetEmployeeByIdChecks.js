

import { check } from 'k6';

export const runChecks = (response) => {
    check(response, {
        'is status 200': (r) => r.status === 200,
        'response time <= 200ms': (r) => r.timings.duration <= 200,
        'response body is not null': (r) => r.body !== null,
        'response contains id': (r) => JSON.parse(r.body).id !== undefined,
        'response contains firstname': (r) => JSON.parse(r.body).firstName !== undefined,
        'response contains lastname': (r) => JSON.parse(r.body).lastName !== undefined,
        'response contains age': (r) => JSON.parse(r.body).age !== undefined,
    });
}