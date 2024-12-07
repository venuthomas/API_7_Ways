import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';
import { options } from '../share/k6Options.js';

const client = new grpc.Client();
client.load(['../share/protos'], 'employee.proto');


export { options };

export default function() {
    let duration = 0;
    try {
        client.connect('localhost:12081', { plaintext: true });
        const startTime = Date.now();
        // Use full package path
        const response = client.invoke('employees.EmployeeService/GetAllEmployees', {});
        duration = Date.now() - startTime;
      
            check(response, {
            'status is OK': (r) => r.status === grpc.StatusOK,
            'response time < 75ms': () => duration < 75, 
            'content type is gRPC': (r) => r.headers['content-type'][0].includes('application/grpc'),
            'response body contains employees': (r) => r.message.Employees.length > 0,
            'has expected fields(Id, FirstName, LastName & Age)': (r) => {
                    const employees = r.message.Employees;
                    return employees.every(e =>
                        e.hasOwnProperty('Id') &&
                        e.hasOwnProperty('FirstName') &&
                        e.hasOwnProperty('LastName') &&
                        e.hasOwnProperty('Age')
                    );
                }
        });
    } catch (error) {
        console.error(`gRPC call failed: ${error}`);
    } finally {
        client.close();
    }

    sleep(1);
}