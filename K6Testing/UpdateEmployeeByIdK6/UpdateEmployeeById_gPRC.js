import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

export { options };

const client = new grpc.Client();
client.load(['../share/protos'], 'employee.proto');


export default function () {
    client.connect('localhost:12081', { plaintext: true });

    const requestData = {
        Id: 1,
        FirstName: 'John',
        LastName: 'Doe',
        Age: 30
    };

    const response = client.invoke('employees.EmployeeService/UpdateEmployee', requestData);

    check(response, {
        'status is OK': (r) => r && r.status === grpc.StatusOK,
        'updatedSuccessfully successfully': (r) => r && r.message.updatedSuccessfully === true,
    });

    client.close();

    sleep(1);
}