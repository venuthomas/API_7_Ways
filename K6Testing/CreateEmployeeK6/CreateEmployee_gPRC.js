// Replace the import and HTTP calls with grpc module calls
import { check, sleep } from 'k6';
import grpc from 'k6/net/grpc';
import { options } from '../share/k6Options.js';

export { options };

const client = new grpc.Client();
client.load(['../share/protos'], 'employee.proto');

export default function () {
    let duration = 0;
    client.connect('localhost:12081', {
        plaintext: true
    });
    const startTime = Date.now();

    const payload = {
        FirstName: 'John',
        LastName: 'Doe',
        Age: 30
    };

    const response = client.invoke('employees.EmployeeService/CreateEmployee', payload);
    duration = Date.now() - startTime;
    
    check(response, {
        'is status OK': (r) => r && r.status === grpc.StatusOK,
        'response time < 500ms': () => duration < 500,
        'response contains new id': (r) => r && r.message && !isNaN(r.message.Employee.Id),
    });

    client.close();
    sleep(1);
}