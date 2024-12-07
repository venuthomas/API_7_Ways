import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';

// Initialize the client
const client = new grpc.Client();
client.load(['../share/protos'], 'employee.proto');

export default () => {
    client.connect('localhost:12081', {
        plaintext: true,
    });

    const payload = {
        Id: Math.floor(Math.random() * 250) + 1, 
    };

    const response = client.invoke('employees.EmployeeService/DeleteEmployee', payload);


    // Check the response
    check(response, {
        'is OK': (r) => r && r.status === grpc.StatusOK,
        'deleted successfully': (r) => r && r.message.deletedSuccessfully === true,
    });

    client.close();
    sleep(1);
};