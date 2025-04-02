import http from 'k6/http';
import { check, sleep } from 'k6';


const BASE_URL = 'http://141.147.1.249:5153'; 

export const options = {
  stages: [
    { duration: '10s', target: 50 },  
    { duration: '30s', target: 50 },  
    { duration: '10s', target: 200 },  
    { duration: '30s', target: 200 },  
    { duration: '10s', target: 50 },   
    { duration: '10s', target: 0 },    
  ],
};

export default function () {
  // Batch hitting "simple" and "cached" endpoints
  let responses = http.batch([
    ['GET', `${BASE_URL}/api/simple/add?a=10&b=5`],
    ['GET', `${BASE_URL}/api/simple/subtract?a=10&b=5`],
    ['GET', `${BASE_URL}/api/simple/multiply?a=10&b=5`],
    ['GET', `${BASE_URL}/api/simple/divide?a=10&b=5`],
    ['GET', `${BASE_URL}/api/simple/factorial?a=10`],
    ['GET', `${BASE_URL}/api/simple/prime?a=7`],

    ['GET', `${BASE_URL}/api/cached/add?a=10&b=5`],
    ['GET', `${BASE_URL}/api/cached/subtract?a=10&b=5`],
    ['GET', `${BASE_URL}/api/cached/multiply?a=10&b=5`],
    ['GET', `${BASE_URL}/api/cached/divide?a=10&b=5`],
    ['GET', `${BASE_URL}/api/cached/factorial?a=10`],
    ['GET', `${BASE_URL}/api/cached/prime?a=11`],
  ]);

  // Check for HTTP 200
  responses.forEach((res) => {
    check(res, { 'status is 200': (r) => r.status === 200 });
  });

  sleep(1);
}
