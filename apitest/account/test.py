import pytest
import requests

@pytest.fixture
def module_url(url):
  return "{}/Account".format(url)

def test_account(module_url, login_url, admin_username, password, team_id):
    """
    """
    with requests.Session() as s:
        payload = {
            "email": admin_username,
            "password": password
        }
        r = s.post(login_url, json=payload)
        assert(r.status_code==200)
        s.headers.update({
            "Authorization": "Bearer {}".format(r.json().get('userToken', ''))
        })


        # Creating user
        data = {
            "teamId": team_id,
            "firstName": "test",
            "lastName": "test",
            "position": "string",
            "phoneNumber": "123",
            "email": "string3@hscr.site",
            "userRole": 1
        }

        url = '{}/{}'.format(module_url, 'create')
        r = s.post(url, json=data)

        user_id = r.json()
        print('response:',r.json())

        assert(r.status_code==201)


        # Editing user
        data = {
            "userId": user_id,
            # "active": True,
            "firstName": "string",
            "lastName": "string",
            "position": "string",
            "phoneNumber": "string",
            "email": "asdf@hscr.site"
        }
        url = '{}/{}'.format(module_url, 'update')
        r = s.post(url, json=data)
        print('editing:', r.json())


        # Deactivating of user
        data = {"userId": user_id}
        url = '{}/{}'.format(module_url, 'delete')
        r = s.post(url, json=data)
        print('deletion:',r.json())

