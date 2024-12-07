import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

const client = new grpc.Client();
client.load(['../share/protos'], 'employee.proto');
export { options };
export default function () {
    let duration = 0;
    try {
        client.connect('localhost:12081', { plaintext: true });
        const startTime = Date.now();
        
        const response = client.invoke('employees.EmployeeService/GetEmployeeById', { "Id": 1 });
        duration = Date.now() - startTime;

        check(response, {
            'status is OK': (r) => r.status === grpc.StatusOK,
            'response time < 75ms': () => duration < 75,
            'content type is gRPC': (r) => r.headers['content-type'][0].includes('application/grpc'),
            'response body is not null': (r) => r.message.Employees !== null,
            'response contains Id': (r) => r.message.Employee.Id !== undefined,
            'response contains firstName': (r) => r.message.Employee.FirstName !== undefined,
            'response contains lastName': (r) => r.message.Employee.LastName !== undefined,
            'response contains age': (r) => r.message.Employee.Age !== undefined,

        });
    } catch (error) {
        console.error(`gRPC call failed: ${error}`);
    } finally {
        client.close();
    }

    sleep(1);
}