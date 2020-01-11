import csv
import requests

API_URL = "https://hsourcerapp.azurewebsites.net/api"
API_VERSION = "v1.0"
ENDPOINT = 'Account/update' # /create
USERNAME = "admin@hscr.site"
PASSWORD = "Test12#$"

URL = '{}/{}/{}'.format(API_URL, API_VERSION, ENDPOINT)
LOGIN_URL = '{}/{}/Auth/login'.format(API_URL, API_VERSION)

login_data = {"email": USERNAME, "password": PASSWORD}

with open('../csv_load/ludzie.csv', 'r') as f:
 reader = csv.reader(f, delimiter=',', quotechar='"')
 next(iter(reader))

 with requests.Session() as s:
  r = s.post(LOGIN_URL, json=login_data)
  token = r.json().get('userToken', '')

 s.headers.update({
  "Authorization": "Bearer {}".format(token)
 })

 for row in reader:
  print(row)

  data = {
   "userId": int(row[0]),
   "teamId": int(row[1]),
   "firstName": row[2],
   "lastName": row[3],
   "position": row[4],
   "phoneNumber": row[5],
   "email": row[6],
   "userRole": int(row[7]),
   "photoPath": row[8],
   "active": int(row[9])
  }

  r = s.post(URL, json=data)

  print(r.status_code)


